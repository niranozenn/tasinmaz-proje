
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using tasinmaz_proje.Business.Abstract;
using tasinmaz_proje.Entities.Concrete;
using tasinmaz_proje.Entities.Dtos;
using tasinmaz_proje.Entity.Concrete;

namespace tasinmaz_proje.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly ICustomLogger<AuthController> _logger;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration, ICustomLogger<AuthController> logger)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            try
            {
                var userToCreate = new User
                {
                    UserName = userForRegisterDto.UserName,
                    Role = userForRegisterDto.Role
                };

                var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);

                await _logger.LogAsync(true, "Kayıt", "Kullanıcı kaydedildi.", HttpContext.Connection.RemoteIpAddress?.ToString(), userForRegisterDto.UserId);

                return StatusCode(201, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Kayıt işlemi sırasında bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            try
            {
                var user = await _authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);
                if (user == null)
                {
                    await _logger.LogAsync(false, "Giriş", "Kullanıcı bulunamadı.", HttpContext.Connection.RemoteIpAddress?.ToString(), userForLoginDto.UserId);

                    return Unauthorized();
                }
               

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName)
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                await _logger.LogAsync(true, "Giriş", "Kullanıcı giriş yaptı.", HttpContext.Connection.RemoteIpAddress?.ToString(), userForLoginDto.UserId);

                return Ok(new { token = tokenString });
            }
            catch (Exception ex)
            {
                await _logger.LogAsync(false, "Giriş", $"Giriş işlemi sırasında bir hata oluştu: {ex.Message}", HttpContext.Connection.RemoteIpAddress?.ToString(), userForLoginDto.UserId);

                return StatusCode(500, $"Giriş işlemi sırasında bir hata oluştu: {ex.Message}");
            }
        }
    }
}


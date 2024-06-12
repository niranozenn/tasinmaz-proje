using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using tasinmaz_proje.Business.Abstract;
using tasinmaz_proje.Entities.Concrete;

namespace tasinmaz_proje.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public UserController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _authRepository.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Kullanıcıları alırken bir hata oluştu: {ex.Message}");
            }
        }
    }
}

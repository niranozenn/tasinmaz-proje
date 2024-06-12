using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using tasinmaz_proje.DataAccess;
using Microsoft.EntityFrameworkCore;
using tasinmaz_proje.Entity.Concrete;
using tasinmaz_proje.Business.Abstract;

namespace tasinmaz_proje.Controllers
{
    [Route("api/il")]
    [ApiController]
    public class IlController : ControllerBase
    {
        private DataContext _context;
        private IAuthRepository _authRepository;
        public IlController(DataContext context, IAuthRepository authRepository)
        {
            _context = context;
            _authRepository = authRepository;
        }
        //GET api values
        [HttpGet("getAll")]
        public async Task<ActionResult> GetValues()
        {

            try
            {
                
                var iller = await _context.Sehir.ToListAsync();
                return Ok(iller);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Veri alınırken bir hata oluştu: {ex.Message}");
            }
        }
        



    }
}


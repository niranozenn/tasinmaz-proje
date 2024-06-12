using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using tasinmaz_proje.DataAccess;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace tasinmaz_proje.Controllers
{
    [Route("api/ilce")]
    [ApiController]
    public class IlceController : ControllerBase
    {
        private DataContext _context;
        public IlceController(DataContext context)
        {
            _context = context;
        }
        //GET api values
        [HttpGet("getAll")]
        public async Task<ActionResult> GetValues()
        {

        
            try
            {
                var ilceler = await _context.Ilce.Include(k => k.Sehir).ToListAsync();
                return Ok(ilceler);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Veri alınırken bir hata oluştu: {ex.Message}");
            }
        }
        [HttpGet("getBySehirId/{id}")]
        public async Task<ActionResult> GetIlcebyId(int id)
        {
            try
            {
                var ilcelerbyId = await _context.Ilce.Include(k => k.Sehir).Where(i => i.SehirId == id).ToListAsync();
                if (ilcelerbyId == null) return NotFound();
                return Ok(ilcelerbyId);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

    }
}



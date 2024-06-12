using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using tasinmaz_proje.DataAccess;
using System.Linq;

namespace tasinmaz_proje.Controllers
{
    [Route("api/mahalle")]
    [ApiController]
   
    public class MahalleController : ControllerBase
    {
        private DataContext _context;
        public MahalleController(DataContext context)
        {
            _context = context;
        }
        //GET api values
        [HttpGet("getAll")]
        public async Task<ActionResult> GetValues()
        {
            try
            {
                var mahalleler = await _context.Mahalle.Include(n=>n.Ilce).ThenInclude(k=>k.Sehir).ToListAsync();
                return Ok(mahalleler);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Veri alınırken bir hata oluştu: {ex.Message}");
            }
        }

        //GET api values
        [HttpGet("getBySehirId/{id}")]
        public async Task<ActionResult> GetMahallebyIlceId(int id)
        {
            try
            {
                var mahallelerByIlceId = await _context.Mahalle.Include(n => n.Ilce).Where(i => i.IlceId==id).ToListAsync();
                if (mahallelerByIlceId == null) return NotFound();
                return Ok(mahallelerByIlceId);

      
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Veri alınırken bir hata oluştu: {ex.Message}");
            }
        }

    }
}

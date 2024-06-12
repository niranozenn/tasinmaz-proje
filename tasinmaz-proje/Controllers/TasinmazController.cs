using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using tasinmaz_proje.DataAccess;
using tasinmaz_proje.Entity.Concrete;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq;
using tasinmaz_proje.Business.Abstract;
using Microsoft.Extensions.Logging;

namespace tasinmaz_proje.Controllers
{
    [Route("api/tasinmaz")]
    [ApiController]
    public class TasinmazController : ControllerBase
    {
        private readonly ICustomLogger<AuthController> _logger;
        private DataContext _context;
        public TasinmazController(DataContext context, ICustomLogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }
        //GET api values
        [HttpGet("getValues")]
        public async Task<ActionResult> GetValues()
        {
            try
            {
                var tasinmazlar = await _context.Tasinmaz.Include(x=>x.Mahalle).ThenInclude(x=>x.Ilce).
                     ThenInclude(x=>x.Sehir).ToListAsync();
                return Ok(tasinmazlar);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Veri alınırken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("getTasinmazById/{id}")]
        public async Task<ActionResult> GetTasinmazById(int id)
        {
            try
            {
                var tasinmazById = await _context.Tasinmaz
                    .Include(t => t.Mahalle)
                        .ThenInclude(m => m.Ilce)
                            .ThenInclude(i => i.Sehir)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (tasinmazById == null)
                    return NotFound();

                return Ok(tasinmazById);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Veri alınırken bir hata oluştu: {ex.Message}");
            }
        }
        [HttpGet("getByUserId")]
        public async Task<ActionResult> GetByUserId(int userId)
        {
            try
            {
                var tasinmazById = await _context.Tasinmaz
                    .Include(t => t.Mahalle)
                        .ThenInclude(m => m.Ilce)
                            .ThenInclude(i => i.Sehir).Where(i => i.UserId == userId).ToListAsync();

                if (tasinmazById == null)
                    return NotFound();

                return Ok(tasinmazById);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Veri alınırken bir hata oluştu: {ex.Message}");
            }
        }

        // POST api/tasinmaz

        [HttpPost("add")]
        public async Task<ActionResult> AddTasinmazAsync(Tasinmaz tasinmaz)
        {
            try
            {
                await this._context.Tasinmaz.AddAsync(tasinmaz);
                await this._context.SaveChangesAsync();

                return Ok(tasinmaz);
            }
            catch (Exception e)
            {
                
                await Console.Out.WriteLineAsync("Hata: " + e.Message);
        
                return BadRequest(new { Message = "Tasinmaz eklenirken bir hata oluştu. " });
            }

        }

        
        // Put api/tasinmaz
        [HttpPut("put")]
        public async Task<ActionResult> PutTasinmazAsync(int id, [FromBody]Tasinmaz yeniTasinmaz)
        {
            try
            {
                var tasinmaz = await _context.Tasinmaz.FirstOrDefaultAsync(x => x.Id == id);

                if (tasinmaz == null)
                {
                    return NotFound("Tasinmaz bulunamadı");
                }

                tasinmaz.MahalleId = yeniTasinmaz.MahalleId;
                tasinmaz.Ada = yeniTasinmaz.Ada;
                tasinmaz.Parsel = yeniTasinmaz.Parsel;
                tasinmaz.Nitelik = yeniTasinmaz.Nitelik;
                tasinmaz.Adres = yeniTasinmaz.Adres;
                tasinmaz.KoordinatX = yeniTasinmaz.KoordinatX;
                tasinmaz.KoordinatY = yeniTasinmaz.KoordinatY;

                _context.Tasinmaz.Update(tasinmaz);
                await _context.SaveChangesAsync();

                return Ok(tasinmaz);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("Hata: " + e.Message);
                return BadRequest();
            }
        }
        

        // Delete api/tasinmaz
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteTasinmazAsync(int id)
        {
            try
            {
                var tasinmaz = await _context.Tasinmaz.FindAsync(id);
                if (tasinmaz == null)
                {
                    return BadRequest("Tasinmaz bulunamadı");
                }

                this._context.Tasinmaz.Remove(tasinmaz);
                await this._context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("Hata: " + e.Message);
                return BadRequest();
            }
        }
    }
}

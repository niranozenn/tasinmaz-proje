using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tasinmaz_proje.DataAccess;
using tasinmaz_proje.Entities.Concrete;

namespace tasinmaz_proje.Controllers
{
    [Route("api/Log")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly DataContext _context;

        public LogController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("getLogs")]
        public async Task<ActionResult> GetLogs()
        {
            try
            {
                var logs = await _context.Log.ToListAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Logları alırken bir hata oluştu: {ex.Message}");
            }
        }

        // POST api/log/add
        [HttpPost("add")]
        public async Task<IActionResult> AddLog([FromBody] Log log)
        {
            try
            {
                // Log'u veritabanına ekleyin
                await _context.Log.AddAsync(log);
                await _context.SaveChangesAsync();
                return Ok("Log başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, $"Log kaydedilirken bir hata oluştu1: {ex.InnerException.Message}");
                }
                else
                {
                    return StatusCode(500, $"Log kaydedilirken bir hata oluştu2: {ex.Message}");
                }
            }

        }
    }
}

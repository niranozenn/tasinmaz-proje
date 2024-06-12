using System;
using System.Threading.Tasks;
using tasinmaz_proje.Business.Abstract;
using tasinmaz_proje.DataAccess;
using tasinmaz_proje.Entities.Concrete;

namespace tasinmaz_proje.Business.Concrete
{
    public class LogManager<T> : ICustomLogger<T>
    {
        private readonly DataContext _context;

        public LogManager(DataContext context)
        {
            _context = context;
        }

        public async Task LogAsync(bool durum, string islemTipi, string aciklama, string logIp, int userId)
        {
            var log = new Log
            {
                durum = durum,
                islemTipi = islemTipi,
                aciklama = aciklama,
                logIp = logIp,
                userId = userId, // Burada kullanıcıya ait userId'yi log'a ekliyoruz
                tarih = DateTime.Now
            };

            await _context.Log.AddAsync(log);
            await _context.SaveChangesAsync();
        }

    }
}

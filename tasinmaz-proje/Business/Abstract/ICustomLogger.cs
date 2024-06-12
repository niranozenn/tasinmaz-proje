using tasinmaz_proje.Entities.Concrete;
using System.Threading.Tasks;

namespace tasinmaz_proje.Business.Abstract
{
    public interface ICustomLogger<T>
    {
        Task LogAsync(bool durum, string islemTipi, string aciklama, string logIp, int userId);
    }
}

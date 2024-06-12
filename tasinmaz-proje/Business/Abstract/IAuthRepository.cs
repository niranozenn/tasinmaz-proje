using System.Collections.Generic;
using System.Threading.Tasks;
using tasinmaz_proje.Entity.Concrete;

namespace tasinmaz_proje.Business.Abstract
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login( string username, string password);
        Task<bool> UserExists(string userName);
        Task<IEnumerable<User>> GetUsers();
    }
}

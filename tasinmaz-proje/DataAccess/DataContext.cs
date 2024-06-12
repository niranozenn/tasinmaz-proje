using Microsoft.EntityFrameworkCore;
using tasinmaz_proje.Entities.Concrete;
using tasinmaz_proje.Entity.Concrete;

namespace tasinmaz_proje.DataAccess
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
       

        public DbSet<User> Users { get; set; }
        public DbSet<Ilce> Ilce { get; set; }

        public DbSet<Sehir> Sehir { get; set; }

        public DbSet<Mahalle> Mahalle { get; set; }

        public DbSet<Tasinmaz> Tasinmaz { get; set; }

        public DbSet<Log> Log { get; set; }







    }
}

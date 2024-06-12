using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tasinmaz_proje.Entity.Concrete
{
    [Table("User")]
    public class User
    {
       [Key, Column("UserId")]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int UserId { get; set; }
       public string UserName { get; set; }
       public byte[] PasswordHash { get; set; }

       public byte[] PasswordSalt { get; set; }

        public string Role { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tasinmaz_proje.Entity.Concrete
{
    [Table("Sehir")]
    public class Sehir
    {
       [Key, Column("Id")]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       public string Ad { get; set; }
       
    }
}

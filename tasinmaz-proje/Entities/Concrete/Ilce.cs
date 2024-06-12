

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tasinmaz_proje.Entity.Concrete
{
    [Table("Ilce")]
    public class Ilce
    {
        [Key, Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Ad { get; set; }
        public int SehirId { get; set; }

        [ForeignKey("SehirId")]
        public virtual Sehir Sehir { get; set; }
    }
}

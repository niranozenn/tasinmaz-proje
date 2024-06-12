using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tasinmaz_proje.Entity.Concrete
{
    public class Mahalle
    {
        [Key, Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Ad { get; set; }
        public int IlceId { get; set; } // Ilce tablosundaki ilce_id'ye referans

        [ForeignKey("IlceId")]
        public virtual Ilce Ilce { get; set; } // Ilce tablosu ile ilişkilendirme
        
    }
}

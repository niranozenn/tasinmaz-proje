using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tasinmaz_proje.Entity.Concrete
{
    public class Tasinmaz
    {
        [Key, Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Ada { get; set; }
        public string Parsel { get; set; }
        public string Nitelik { get; set; }
        public string KoordinatX { get; set; }
        public string Adres { get; set; }
        public string KoordinatY { get; set; }
        public int MahalleId { get; set; } // Mahalle tablosundaki mahalle_id'ye referans
        
        [ForeignKey("MahalleId")]
        public virtual Mahalle Mahalle { get; set; } // Mahalle tablosu ile ilişkilendirme

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}

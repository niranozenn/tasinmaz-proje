using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tasinmaz_proje.Entity.Concrete;

namespace tasinmaz_proje.Entities.Concrete
{

    [Table("Log")]
    public class Log
    {
        [Key, Column("logId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int logId { get; set; }
        public bool durum { get; set; }
        public string islemTipi { get; set; }
        public string aciklama { get; set; }
        public DateTime tarih { get; set; }
        public string logIp { get; set; }
        public int userId { get; set; }

        [ForeignKey("userId")]
        public virtual User User { get; set; }
    }
}

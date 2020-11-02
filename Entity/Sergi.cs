using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ahe.Models
{
    [Table("Sergi")]
    public class Sergi
    {
        public int ID { get; set; }

        [MaxLength(4)]
        public string Tarih { get; set; }

        public int KategoriID { get; set; }

        [MaxLength(50)]
        public string Adi { get; set; }

        [MaxLength(50)]
        public string Baslik { get; set; }

        [MaxLength(50)]
        public string GorselA { get; set; }

        [MaxLength(50)]
        public string GorselB { get; set; }

        public Nullable<bool> Aktif { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        [MaxLength(50)]
        public string Technique { get; set; }

        [MaxLength(50)]
        public string Material { get; set; }

        [MaxLength(50)]
        public string Dimensions { get; set; }

        [MaxLength(50)]
        public string Gallery { get; set; }

        public int durum { get; set; }

    }
}














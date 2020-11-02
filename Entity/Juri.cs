using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ahe.Models
{
    [Table("Juri")]
    public class Juri
    {
        public int ID { get; set; }

        [MaxLength(4)]
        public string Tarih { get; set; }

        [MaxLength(50)]
        public string AdSoyad { get; set; }

        [MaxLength(250)]
        public string Gorsel { get; set; }

        [MaxLength(50)]
        public string Unvan { get; set; }

        [MaxLength]
        public string Bilgi { get; set; }

        public int durum { get; set; }


    }
}
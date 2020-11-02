using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ahe.Models
{
    [Table("Kullanicilar")]
    public class Kullanicilar
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string KullaniciAdi { get; set; }

        [MaxLength(50)]
        public string Sifre { get; set; }
    }
}
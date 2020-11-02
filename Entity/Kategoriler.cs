using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ahe.Models
{
    [Table("Kategoriler")]
    public class Kategoriler
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string Kategori { get; set; }

        public int durum { get; set; }
    }
}
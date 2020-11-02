using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Ahe.Models
{
    public class AheSergiDB : DbContext
    {
        public DbSet<Juri> Juri { get; set; }
        public DbSet<Kategoriler> Kategoriler { get; set; }
        public DbSet<Kullanicilar> Kullanicilar { get; set; }
        public DbSet<Sergi> Sergi { get; set; }

        public AheSergiDB()
         : base("name=AheSergiDB")
        {
        }

    }
}
using Ahe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ahe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            string tarih ="2020";
            if (id == "")
            {
                int tmpID;
                if (int.TryParse(id, out tmpID))
                    tarih = id;
            }

            AheSergiDB myDB = new AheSergiDB();
            var jurilist = myDB.Juri.Where(x => x.durum == 0 && x.Tarih == tarih).OrderBy(y => y.AdSoyad).ToList();
            var sergilist1 = (from s in myDB.Sergi
                          join k in myDB.Kategoriler
                          on s.KategoriID equals k.ID
                          select new
                          {
                              s.ID,
                              s.Tarih,
                              s.KategoriID,
                              s.Adi,
                              s.Baslik,
                              s.GorselA,
                              s.GorselB,
                              s.Aktif,
                              s.Genre,
                              s.Technique,
                              s.Material,
                              s.Dimensions,
                              s.Gallery,
                              k.Kategori,
                              s.durum
                          }).Where(x =>x.Tarih==tarih && x.durum == 0 && x.Aktif==true).OrderBy(x=>x.KategoriID).AsEnumerable().Select(x => x.ToExpando());

            ViewBag.sergiler = sergilist1;
            ViewBag.juriler = jurilist;

            int n1 = myDB.Sergi.Where(x => x.KategoriID == 1 && x.Tarih == tarih && x.durum == 0 && x.Aktif == true).Count();
            int n2 = myDB.Sergi.Where(x => x.KategoriID == 2 && x.Tarih == tarih && x.durum == 0 && x.Aktif == true).Count();
            int n3 = myDB.Sergi.Where(x => x.KategoriID == 3 && x.Tarih == tarih && x.durum == 0 && x.Aktif == true).Count();
            int n4 = myDB.Sergi.Where(x => x.KategoriID == 4 && x.Tarih == tarih && x.durum == 0 && x.Aktif == true).Count();
            int n5 = myDB.Sergi.Where(x => x.KategoriID == 5 && x.Tarih == tarih && x.durum == 0 && x.Aktif == true).Count();

            string strLayout = "";
            if (n1 > 0)
                strLayout = n1.ToString()+",";
            if(n2>0)
                strLayout += n2.ToString() + ",";
            if (n3 > 0)
                strLayout += n3.ToString() + ",";
            if (n4 > 0)
                strLayout += n4.ToString() + ",";
            if (n5 > 0)
                strLayout += n5.ToString() + ",";

            ViewBag.layout = strLayout.Remove(strLayout.Length-1, 1);
            ViewBag.tarih = tarih;

            return View();
        }

    }
}
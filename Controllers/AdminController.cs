using Ahe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Ahe.Controllers
{
    [UserAuthorize]
    public class AdminController : Controller
    {
        [HttpPost]
        public ActionResult sergiKaydet(Sergi sergi, HttpPostedFileBase GorselA, HttpPostedFileBase GorselB)
        {

            string strNewNameA = "";
            if (GorselA != null)
            {
                int fileSizeInBytes = GorselA.ContentLength;
                string fileName = GorselA.FileName.ToLower();
                string fileExtension = "";

                if (!string.IsNullOrEmpty(fileName))
                    fileExtension = Path.GetExtension(fileName);

                strNewNameA = setURL(fileName.Replace(fileExtension, "")) + "-" + Guid.NewGuid().ToString() + fileExtension;

                string savedFileName = Path.Combine(@Server.MapPath("~/uploads/sergi/"), strNewNameA);
                GorselA.SaveAs(savedFileName);
            }


            string strNewNameB = "";
            if (GorselB != null)
            {
                int fileSizeInBytes = GorselB.ContentLength;
                string fileName = GorselB.FileName.ToLower();
                string fileExtension = "";

                if (!string.IsNullOrEmpty(fileName))
                    fileExtension = Path.GetExtension(fileName);

                strNewNameB = setURL(fileName.Replace(fileExtension, "")) + "-" + Guid.NewGuid().ToString() + fileExtension;

                string savedFileName = Path.Combine(@Server.MapPath("~/uploads/sergi/"), strNewNameB);
                GorselB.SaveAs(savedFileName);
            }

            if (strNewNameA != "")
                sergi.GorselA = strNewNameA;

            if (strNewNameB != "")
                sergi.GorselB = strNewNameB;

            AheSergiDB myDB = new AheSergiDB();
            var dbSergi = new Sergi();
            dbSergi.Tarih = sergi.Tarih;
            dbSergi.KategoriID = sergi.KategoriID;
            dbSergi.Adi = sergi.Adi;
            dbSergi.Baslik = sergi.Baslik;
            dbSergi.GorselA = sergi.GorselA;
            dbSergi.GorselB = sergi.GorselB;

            if (sergi.Aktif.ToString().ToLower() == "true")
                dbSergi.Aktif = true;
            else
                dbSergi.Aktif = false;

            dbSergi.Genre = sergi.Genre;
            dbSergi.Technique = sergi.Technique;
            dbSergi.Material = sergi.Material;
            dbSergi.Dimensions = sergi.Dimensions;
            dbSergi.Gallery = sergi.Gallery;
            myDB.Sergi.Add(dbSergi);
            myDB.SaveChanges();

            TempData["Alert"] = new Alert { message = "Yeni Sergi Eklendi", color = "green" };

            return RedirectToAction("sergiler", "Admin");
        }

        public ActionResult sergiEkle()
        {
            AheSergiDB myDB = new AheSergiDB();
            dynamic mymodel = new ExpandoObject();
            mymodel.kategoriler = myDB.Kategoriler.Where(x => x.durum == 0).ToList();
            
           var yearList = new List<SelectListItem>();
            yearList.Add(new SelectListItem() { Value = "", Text = "Seç" });
            for (var i = DateTime.Now.Year; i >= 2007; i--)
            {
                yearList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }

            mymodel.Tarih = yearList;

            return View(mymodel);
        }


        [HttpPost]
        public ActionResult sergiGuncelle(Sergi sergi, HttpPostedFileBase GorselA, HttpPostedFileBase GorselB)
        {

            string strNewNameA = "";
            if (GorselA != null)
            {
                int fileSizeInBytes = GorselA.ContentLength;
                string fileName = GorselA.FileName.ToLower();
                string fileExtension = "";

                if (!string.IsNullOrEmpty(fileName))
                    fileExtension = Path.GetExtension(fileName);

                strNewNameA = setURL(fileName.Replace(fileExtension, "")) + "-" + Guid.NewGuid().ToString() + fileExtension;

                string savedFileName = Path.Combine(@Server.MapPath("~/uploads/sergi/"), strNewNameA);
                GorselA.SaveAs(savedFileName);
            }


            string strNewNameB = "";
            if (GorselB != null)
            {
                int fileSizeInBytes = GorselB.ContentLength;
                string fileName = GorselB.FileName.ToLower();
                string fileExtension = "";

                if (!string.IsNullOrEmpty(fileName))
                    fileExtension = Path.GetExtension(fileName);

                strNewNameB = setURL(fileName.Replace(fileExtension, "")) + "-" + Guid.NewGuid().ToString() + fileExtension;

                string savedFileName = Path.Combine(@Server.MapPath("~/uploads/sergi/"), strNewNameB);
                GorselB.SaveAs(savedFileName);
            }

            if (strNewNameA != "")
                sergi.GorselA = strNewNameA;

            if (strNewNameB != "")
                sergi.GorselB = strNewNameB;

            AheSergiDB myDB = new AheSergiDB();
            var dbSergi = myDB.Sergi.Where(x => x.ID == sergi.ID).FirstOrDefault();
            dbSergi.Tarih = sergi.Tarih;
            dbSergi.KategoriID = sergi.KategoriID;
            dbSergi.Baslik = sergi.Baslik;
            dbSergi.GorselA = sergi.GorselA;
            dbSergi.GorselB = sergi.GorselB;

            if (sergi.Aktif.ToString().ToLower() == "true")
                dbSergi.Aktif = true;
            else
                dbSergi.Aktif = false;

            dbSergi.Genre = sergi.Genre;
            dbSergi.Technique = sergi.Technique;
            dbSergi.Material = sergi.Material;
            dbSergi.Dimensions = sergi.Dimensions;
            dbSergi.Gallery = sergi.Gallery;

            myDB.Sergi.Add(dbSergi);

            TempData["Alert"] = new Alert { message = "Sergi Güncellendi", color = "green" };

            return RedirectToAction("sergiler", "Admin");
        }

        public ActionResult Sergiler()
        {
            AheSergiDB myDB = new AheSergiDB();
            var result = (from s in myDB.Sergi
                          join k in myDB.Kategoriler
                          on s.KategoriID equals k.ID
                          select new
                          {
                              s.ID,
                              s.Tarih,
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
                          }).Where(x => x.durum == 0).AsEnumerable().Select(x => x.ToExpando());

            return View(result);
        }

        public ActionResult sergiDuzenle(string id)
        {
            int tmpID = 0;
            if (!int.TryParse(id, out tmpID))
                Response.Redirect("/Admin/sergiler");

            AheSergiDB myDB = new AheSergiDB();
            dynamic mymodel = new ExpandoObject();
            mymodel.sergi = myDB.Sergi.Where(x => x.ID == tmpID && x.durum == 0).FirstOrDefault();
            mymodel.kategoriler = myDB.Kategoriler.Where(x => x.durum == 0).ToList();

            var yearList = new List<SelectListItem>();
            yearList.Add(new SelectListItem() { Value = "", Text = "Seç" });
            for (var i = DateTime.Now.Year; i >= 2007; i--)
            {
                yearList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }

            mymodel.Tarih = yearList;

            return View(mymodel);
        }

        public ActionResult sergiSil(string id)
        {
            int tmpID = 0;
            if (!int.TryParse(id, out tmpID))
                Response.Redirect("/Admin/sergiler");

            AheSergiDB myDB = new AheSergiDB();
            var sergi = myDB.Sergi.Where(x => x.ID == tmpID).FirstOrDefault();
            sergi.durum = 1;
            myDB.SaveChanges();

            TempData["Alert"] = new Alert { message = "Sergi Silindi", color = "green" };
            return RedirectToAction("sergiler", "Admin");
        }

        public ActionResult kategoriler()
        {
            AheSergiDB myDB = new AheSergiDB();
            var kategorilist = myDB.Kategoriler.Where(x => x.durum == 0).OrderBy(y => y.Kategori).ToList();

            return View(kategorilist);
        }


        public ActionResult kategoriSil(string id)
        {
            int tmpID = 0;
            if (!int.TryParse(id, out tmpID))
                Response.Redirect("/Admin/kategoriler");

            AheSergiDB myDB = new AheSergiDB();
            var kategori = myDB.Kategoriler.Where(x => x.ID == tmpID).FirstOrDefault();
            kategori.durum = 1;
            myDB.SaveChanges();

            TempData["Alert"] = new Alert { message = "Kategori Silindi", color = "green" };
            return RedirectToAction("kategoriler", "Admin");
        }


        public ActionResult kategoriDuzenle(string id)
        {
            int tmpID = 0;
            if (!int.TryParse(id, out tmpID))
                Response.Redirect("/Admin/kategoriler");

            AheSergiDB myDB = new AheSergiDB();
            Kategoriler kategori = myDB.Kategoriler.Where(x => x.ID == tmpID && x.durum == 0).FirstOrDefault();

            return View(kategori);
        }

        [HttpPost]
        public ActionResult kategoriGuncelle(Kategoriler model)
        {
            AheSergiDB myDB = new AheSergiDB();
            var dbKategori = myDB.Kategoriler.Where(x => x.ID == model.ID).FirstOrDefault();
            dbKategori.Kategori = model.Kategori;
            myDB.SaveChanges();

            TempData["Alert"] = new Alert { message = "Kategori Güncellendi", color = "green" };
            return RedirectToAction("kategoriler", "Admin");
        }

        [HttpPost]
        public ActionResult kategoriKaydet(Kategoriler model)
        {
            //Kategoriler kategori = new Kategoriler { Kategori = frm["Kategori"].ToString(), durum = 0 };

            AheSergiDB myDB = new AheSergiDB();
            myDB.Kategoriler.Add(model);
            myDB.SaveChanges();

            TempData["Alert"] = new Alert { message = "Yeni Kategori Eklendi", color = "green" };
            return RedirectToAction("kategoriler", "Admin");
        }

        public ActionResult kategoriEkle()
        {
            return View();
        }

        public ActionResult sifreGuncelle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult sifreGuncelle(FormCollection frm)
        {
            if (frm["Sifre"].ToString() == frm["YeniSifre"].ToString())
            {
                int nID = int.Parse(Session["ID"].ToString());
                string eskiSifre = frm["EskiSifre"].ToString();

                AheSergiDB myDB = new AheSergiDB();
                Kullanicilar kullanici = myDB.Kullanicilar.Where(x => x.ID == nID && x.Sifre == eskiSifre).FirstOrDefault();

                if (kullanici != null)
                {
                    kullanici.Sifre = frm["YeniSifre"].ToString();
                    myDB.SaveChanges();
                    TempData["Alert"] = new Alert { message = "Kullanıcı bilgileri güncellendi", color = "green" };
                }
                else
                {
                    TempData["Alert"] = new Alert { message = "Eski şifreniz yanlış", color = "red" };
                    ModelState.Clear();
                    return RedirectToAction("sifreGuncelle", "Admin");
                }

            }
            else
            {
                TempData["Alert"] = new Alert { message = "Yeni Şifre Uyuşmuyor, aynı olmalı", color = "red" };
                ModelState.Clear();
                return RedirectToAction("sifreGuncelle", "Admin");
            }

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult juriler()
        {
            AheSergiDB myDB = new AheSergiDB();
            var jurilist = myDB.Juri.Where(x => x.durum == 0).OrderByDescending(y => y.Tarih).ThenBy(y => y.AdSoyad).ToList();

            return View(jurilist);
        }

        [HttpPost]
        public ActionResult juriKaydet(Juri juri, HttpPostedFileBase file)
        {
            string strNewName = "";
            if (file != null)
            {
                int fileSizeInBytes = file.ContentLength;
                string fileName = file.FileName.ToLower();
                string fileExtension = "";

                if (!string.IsNullOrEmpty(fileName))
                    fileExtension = Path.GetExtension(fileName);

                strNewName = setURL(fileName.Replace(fileExtension, "")) + "-" + Guid.NewGuid().ToString() + fileExtension;

                string savedFileName = Path.Combine(@Server.MapPath("~/uploads/Juri/"), strNewName);
                file.SaveAs(savedFileName);
            }

            if (strNewName != "")
                juri.Gorsel = strNewName;

            AheSergiDB myDB = new AheSergiDB();
            myDB.Juri.Add(juri);
            myDB.SaveChanges();

            TempData["Alert"] = new Alert { message = "Yeni Jüri Eklendi", color = "green" };
            return RedirectToAction("juriler", "Admin");
        }

        [HttpPost]
        public ActionResult juriGuncelle(Juri juri, HttpPostedFileBase file)
        {
            string strNewName = "";
            if (file != null)
            {
                int fileSizeInBytes = file.ContentLength;
                string fileName = file.FileName.ToLower();
                string fileExtension = "";

                if (!string.IsNullOrEmpty(fileName))
                    fileExtension = Path.GetExtension(fileName);

                strNewName = setURL(fileName.Replace(fileExtension, "")) + "-" + Guid.NewGuid().ToString() + fileExtension;

                string savedFileName = Path.Combine(@Server.MapPath("~/uploads/Juri/"), strNewName);
                file.SaveAs(savedFileName);
            }

            if (strNewName != "")
                juri.Gorsel = strNewName;

            AheSergiDB myDB = new AheSergiDB();
            var dbJuri = myDB.Juri.Where(x => x.ID == juri.ID).FirstOrDefault();
            dbJuri.Tarih = juri.Tarih;
            dbJuri.AdSoyad = juri.AdSoyad;
            dbJuri.Gorsel = juri.Gorsel;
            dbJuri.Unvan = juri.Unvan;
            dbJuri.Bilgi = juri.Bilgi;

            myDB.SaveChanges();

            TempData["Alert"] = new Alert { message = "Juri Güncellendi", color = "green" };
            return RedirectToAction("juriler", "Admin");
        }

        public ActionResult juriDuzenle(string id)
        {
            int tmpID = 0;
            if (!int.TryParse(id, out tmpID))
                Response.Redirect("/Admin/juriler");

            AheSergiDB myDB = new AheSergiDB();

            dynamic mymodel = new ExpandoObject();
            mymodel.Kategoriler = myDB.Kategoriler.ToList();
            mymodel.Juri = myDB.Juri.Where(x => x.ID == tmpID).FirstOrDefault();

            var yearList = new List<SelectListItem>();
            yearList.Add(new SelectListItem() { Value = "", Text = "Seç" });
            for (var i = DateTime.Now.Year; i >= 2007; i--)
            {
                yearList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }

            mymodel.Tarih = yearList;

            return View(mymodel);
        }

        public ActionResult juriSil(string id)
        {
            int tmpID = 0;
            if (!int.TryParse(id, out tmpID))
                Response.Redirect("/Admin/juriler");

            AheSergiDB myDB = new AheSergiDB();
            var juri = myDB.Juri.Where(x => x.ID == tmpID).FirstOrDefault();
            juri.durum = 1;
            myDB.SaveChanges();

            TempData["Alert"] = new Alert { message = "Juri Silindi", color = "green" };
            return RedirectToAction("juriler", "Admin");
        }

        public ActionResult juriEkle()
        {
            var yearList = new List<SelectListItem>();
            yearList.Add(new SelectListItem() { Value = "", Text = "Seç" });
            for (var i = DateTime.Now.Year; i >= 2007; i--)
            {
                yearList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }

            dynamic mymodel = new ExpandoObject();
            mymodel.Tarih = yearList;

            return View(mymodel);
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult cikis()
        {
            Session["ID"] = null;
            Session["kullaniciAdi"] = null;
            Response.Redirect("/");

            return View();
        }

        public ActionResult giris()
        {
            return View();
        }



        [HttpPost]
        public ActionResult giris(Kullanicilar kullanicilar)
        {
            string strKul = kullanicilar.KullaniciAdi;
            string strSifre = kullanicilar.Sifre;

            if (strKul != "" && strSifre != "")
            {
                AheSergiDB myDB = new AheSergiDB();
                var kullanici = myDB.Kullanicilar.Where(x => x.KullaniciAdi == strKul && x.Sifre == strSifre).FirstOrDefault();

                if (kullanici != null)
                {
                    Session["ID"] = kullanici.ID.ToString();
                    Session["kullaniciAdi"] = strKul;
                    Response.Redirect("/Admin/Index");
                }
                else
                    ViewBag.Hata = "Kullanıcı adı veya şifre hatalı";
            }

            ModelState.Clear();
            return View();
        }

        public string setURL(string baslik)
        {
            Regex r = new Regex(@"\s+");
            baslik = baslik.Trim().ToLower();
            string url = r.Replace(baslik, @" ").Replace("%", "").Replace(" ", "").Replace("'", "").Replace("\"", "").Replace("ç", "c").Replace("ş", "s").Replace("ğ", "g").Replace("ü", "u").Replace("ö", "o").Replace("ı", "i").Replace("+", "").Replace("#", "").Replace(".", "").Replace(",", "").Replace(";", "").Replace("?", "").Replace("!", "").Replace(":", "").Replace("/", "").Replace(@"\", "").Replace("<", "").Replace(">", "").Replace("&", "-").Replace("é", "e");
            url = Regex.Replace(url, "[^a-zA-Z0-9_]+", "-", RegexOptions.Compiled);

            return url;


        }

    }

    public class UserAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.RawUrl.ToLower().IndexOf("admin/giris") >= 0)
                return true;

            if (httpContext.Session["ID"] != null || httpContext.Session["kullaniciAdi"] != null)
            {
                return true;
            }
            else
            {
                httpContext.Response.Redirect("/admin/giris");
                return false;
            }

        }
    }
}

public class Alert
{
    public Alert()
    {
    }

    public string message { get; set; }
    public string color { get; set; }
}

public static class impFunctions
{
    public static ExpandoObject ToExpando(this object anonymousObject)
    {
        IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
        IDictionary<string, object> expando = new ExpandoObject();
        foreach (var item in anonymousDictionary)
            expando.Add(item);
        return (ExpandoObject)expando;
    }
}
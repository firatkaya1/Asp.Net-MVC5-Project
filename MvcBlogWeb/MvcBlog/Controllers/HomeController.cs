using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Models;
using PagedList;
using PagedList.Mvc;
namespace MvcBlog.Controllers
{
    public class HomeController : Controller
    {
        mvcblogDB db = new mvcblogDB();
        // GET: Home
        public ActionResult Index(int  Page=1)
        {
            var makale = db.Makales.OrderByDescending(m=>m.Makaleid).ToPagedList(Page,5);
            return View(makale);
        }
        public ActionResult BlogAra(string Ara=null)
        {
            var aranan = db.Makales.Where(m => m.Baslik.Contains(Ara)).ToList();
            return View(aranan.OrderByDescending(m=>m.Tarih));
        }
        public ActionResult SonYorumlar()
        {
            
            return View(db.Yorums.OrderByDescending(y=>y.Yorumid).Take(5));
        }
        public ActionResult PopulerMakaleler()
        {

            return View(db.Makales.OrderByDescending(m => m.Okuma).Take(5));
        }
        public ActionResult KategoriMakale(int id)
        {
            var makaleler = db.Makales.Where(m => m.Kategori.Kategoriid == id).ToList();



            return View(makaleler);
        }
        public ActionResult MakaleDetayı(int id)
        {
            var makale = db.Makales.Where(m => m.Makaleid == id).SingleOrDefault();
            if (makale==null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }
        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        public ActionResult KategoriPartial()
        {
            return View(db.Kategoris.ToList());
        }
        public JsonResult YorumYap(string yorum,int Makaleid)
        {
            var uyeid = Session["uyeid"];
            if (yorum == null)
            {
                return Json(true,JsonRequestBehavior.AllowGet);
                
            }
            db.Yorums.Add(new Yorum { Uyeid = Convert.ToInt32(uyeid), Makaleid = Makaleid, Icerik = yorum, Tarih = DateTime.Now });
            db.SaveChanges();

            return Json(false,JsonRequestBehavior.AllowGet);//yorum yapmasına izin ver
        }
        public ActionResult YorumSil(int id)
        {
            var uyeid = Session["uyeid"];
            var yorum = db.Yorums.Where(y => y.Yorumid == id).SingleOrDefault();
            var makale = db.Makales.Where(m => m.Makaleid == yorum.Makaleid).SingleOrDefault();
            if (yorum.Uyeid==Convert.ToInt32(uyeid))
            {
                db.Yorums.Remove(yorum);
                db.SaveChanges();
                return RedirectToAction("MakaleDetayı", "Home", new { id = makale.Makaleid });
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult OkunmaArttir(int Makaleid)
        {
            var makale = db.Makales.Where(m => m.Makaleid == Makaleid).SingleOrDefault();
            makale.Okuma += 1;
            db.SaveChanges();
            return View();
        }
    }
}
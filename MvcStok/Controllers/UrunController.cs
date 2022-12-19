using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models;
using MvcStok.Models.Entity;



namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        
        //MvcDbStokEntities db = new MvcDbStokEntities();    
        MvcDbStokEntities4 db = new MvcDbStokEntities4();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
           
        }

        [HttpGet]






        public ActionResult UrunEkle()
        {

            MvcDbStokEntities4 db = new MvcDbStokEntities4();
            Class1 cs = new Class1();

            // cs.Urunler = new SelectList(db.TBLURUNLER, "URUNID", "URUNAD", "MARKA", "URUNKATEGORI", "FIYAT", "STOK");

            cs.Kategoriler = new SelectList(db.TBLKATEGORILER, "KATEGORIID", "KATEGORIAD");

            cs.AltKategoriler = new SelectList(db.TBLALTKETEGORILER, "ALTKATEGORIID", "ALTKATEGORIADI");

            cs.Markalar = new SelectList(db.TBLMARKALAR, "MARKAID", "MARKAADI");

            return View(cs);






            //List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
            //                                 select new SelectListItem
            //                                 {
            //                                     Text = i.KATEGORIAD,
            //                                     Value = i.KATEGORIID.ToString()
            //                                 }).ToList();
            //ViewBag.dgr = degerler;





            //List<SelectListItem> degerler2 = (from a in db.TBLALTKETEGORILER.ToList()
            //                                  select new SelectListItem
            //                                  {
            //                                      Text = a.ALTKATEGORIADI,
            //                                      Value = a.ALTKATEGORIID.ToString()
            //                                  }).ToList();




            //ViewBag.dgr2 = degerler2;

            //return View();



        }

        public JsonResult altkategorigetir(int p)
        {

            var altkategoriler = (from x in db.TBLALTKETEGORILER
                                  join y in db.TBLKATEGORILER on x.TBLKATEGORILER.KATEGORIID equals y.KATEGORIID
                                  where x.TBLKATEGORILER.KATEGORIID == p
                                  select new
                                  {
                                      Text = x.ALTKATEGORIADI,
                                      value = x.ALTKATEGORIID.ToString()
                                  }).ToList();



            return Json(altkategoriler, JsonRequestBehavior.AllowGet);

        }


        public JsonResult markagetir(int p)
        {

            var markalar = (from x in db.TBLMARKALAR
                            join y in db.TBLALTKETEGORILER on x.TBLALTKETEGORILER.ALTKATEGORIID equals y.ALTKATEGORIID
                            where x.TBLALTKETEGORILER.ALTKATEGORIID == p
                            select new
                            {
                                Text = x.MARKAADI,
                                value = x.MARKAID.ToString()
                            }).ToList();

            return Json(markalar, JsonRequestBehavior.AllowGet);

        }




        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p)
        {
            //var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            //p.TBLKATEGORILER = ktg;

            //var altktg = db.TBLALTKETEGORILER.Where(m => m.ALTKATEGORIID == p.TBLALTKETEGORILER.ALTKATEGORIID).FirstOrDefault();
            //p.TBLALTKETEGORILER = altktg;  

           


            db.TBLURUNLER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urun = db.TBLURUNLER.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;

           
         
           urun.MARKA = p.MARKA;

            urun.STOK = p.STOK;
            urun.FIYAT = p.FIYAT;

            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
           

          
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
using ProdavnicaMVC.DBProdavnica;
using ProdavnicaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProdavnicaMVC.Controllers
{
    public class ArtiklController : Controller
    {
        // GET: Artikl
        [Authorize(Roles ="Administrator, Moderator")]
        public ActionResult Index()
        {
            using (var context = new ProdavnicaContext())
            {
                var artikli = context.Artikls.Select(a => new ArtiklViewModel()
                {
                    ArtiklId = a.ArtiklId,
                    Naziv = a.Naziv,
                    Cijena = a.Cijena,
                    Kolicina = a.Kolicina,
                    Opis = a.Opis,
                    DobavljacId = a.DobavljacId,
                    DobavljacNaziv = a.Dobavljac.Naziv,
                   
                    Slika = a.Slika

                }).ToList();
                return View(artikli);
            }              
        }
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Create()
        {
            using (var context = new ProdavnicaContext())
            {
                ViewBag.Dobavljaci = context.Dobavljacs.Select(d => new DobavljacViewModel()
                {
                    DobavljacId = d.DobavljacId,
                    Naziv = d.Naziv

                }).ToList();

               
                ViewBag.Dobavljaci = context.Dobavljacs.Select(d => new SelectListItem()
                {
                    Text = d.Naziv,
                    Value = "" + d.DobavljacId
                }).ToList();
                
          
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Create(ArtiklViewModel artikl)
        {
            using (var context = new ProdavnicaContext())
            {
                context.Artikls.Add(new Artikl()
                {
                    ArtiklId = artikl.ArtiklId,
                    Naziv = artikl.Naziv,
                    Cijena = artikl.Cijena,
                    Kolicina = artikl.Kolicina,
                    Opis = artikl.Opis,
                    DobavljacId = artikl.DobavljacId,
                    Slika = artikl.Slika
                });
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit(string id)
        {
            
            int artiklId = Convert.ToInt32(id);
            using (var context = new ProdavnicaContext())
            {
                Artikl artikl = context.Artikls.Where(a => a.ArtiklId == artiklId).FirstOrDefault();
                ArtiklViewModel artiklVm = new ArtiklViewModel()
                {
                    ArtiklId = artikl.ArtiklId,
                    Naziv = artikl.Naziv,
                    Cijena = artikl.Cijena,
                    Kolicina = artikl.Kolicina,
                    Opis = artikl.Opis,
                    DobavljacId = artikl.DobavljacId,
                    Slika = artikl.Slika
                };

                ViewBag.Dobavljaci = context.Dobavljacs.Select(d => new SelectListItem() //
                {
                    Text = d.Naziv,
                    Value = "" + d.DobavljacId
                }).ToList();

               return View(artiklVm);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit(ArtiklViewModel artikl)
        {
            using (var context = new ProdavnicaContext())
            {
                Artikl art = context.Artikls.Find(artikl.ArtiklId);

                art.ArtiklId = artikl.ArtiklId;
                art.Naziv = artikl.Naziv;
                art.Cijena = artikl.Cijena;
                art.Kolicina = artikl.Kolicina;
                art.Opis = artikl.Opis;
                art.DobavljacId = artikl.DobavljacId;
                art.Slika = artikl.Slika;

                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(string id)
        {
            int artiklId = Convert.ToInt32(id);

            using (var context = new ProdavnicaContext())
            {
                Artikl artikl = context.Artikls.Where(a => a.ArtiklId == artiklId).FirstOrDefault();
                ArtiklViewModel artiklVm = new ArtiklViewModel()
                {
                    ArtiklId = artikl.ArtiklId,
                    Naziv = artikl.Naziv,
                    Cijena = artikl.Cijena,
                    Kolicina = artikl.Kolicina,
                    Opis = artikl.Opis,
                    DobavljacId = artikl.DobavljacId,
                    Slika = artikl.Slika
                };
                return View(artiklVm);
            }

        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int id)
        {
            using (var context = new ProdavnicaContext())
            {

                context.Artikls.Remove(context.Artikls.Find(id));
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        /*public ActionResult Jezik(string leng)
        {

            HttpCookie myCookie = new HttpCookie("Jezik");
            DateTime now = DateTime.Now;

            // Set the cookie value.
            myCookie.Value = leng;
            // Set the cookie expiration date.
            myCookie.Expires = now.AddMonths(10);
            // Add the cookie.
            Response.Cookies.Add(myCookie);
            return RedirectToAction("Index", "Artikl", null);
        }*/

       [AllowAnonymous]     //bilo ko moze pristupiti
        public JsonResult PromijeniJezik(string lang)
        {
            HttpCookie myCookie = new HttpCookie("Jezik");    //trazi cookie Jezik
            DateTime now = DateTime.Now;

            // Set the cookie value.
            myCookie.Value = lang;
            // Set the cookie expiration date.
            myCookie.Expires = now.AddMonths(10);
            // Add the cookie.
            Response.Cookies.Add(myCookie);

            return Json(new { Success = true });

        }



    }
}
using ProdavnicaMVC.DBProdavnica;
using ProdavnicaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProdavnicaMVC.Controllers
{
    public class RacunController : Controller
    {
        // GET: Racun
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator, Promoter, User")]
        public ActionResult Create()
        {
            using (var context = new ProdavnicaContext())
            {
                var artikliNaStanju = context.Artikls.Where(a => a.Kolicina > 0).Select(a => new ArtiklNaStanjuViewModel()
                {
                    ArtiklId = a.ArtiklId,
                    Naziv = a.Naziv,
                    Cijena = a.Cijena,
                    Kolicina = a.Kolicina

                }).ToList();

                return View(artikliNaStanju);

            }              
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Promoter, User")]
        public ActionResult Create(List<KupljeniArtikliViewModel> listaKupljenihStavki)  //isti naziv kao naziv niza u js - listaKupljenihStavki
        {
            using (var context = new ProdavnicaContext())
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Success = false, Message = "Greska" });
                }
                if (listaKupljenihStavki == null)
                {
                    return Json(new { Success = false, Message = "Greska" });
                }
                if (listaKupljenihStavki.Any(k => k.Kolicina == 0 || k.ArtiklId == 0 || k.Kolicina < 0))
                {
                    return Json(new { Success = false, Message = "Greska" });
                }
                if (listaKupljenihStavki.Any(k => k.Kolicina > context.Artikls.Find(k.ArtiklId).Kolicina))
                {
                    return Json(new { Success = false, Message = "Greska" });
                }

                Racun racun = new Racun()
                {
                    DatumIzdavanja = DateTime.Now                  
                };

                var korisnik = context.Korisniks.FirstOrDefault(k => k.Username == User.Identity.Name);  //trazim ulogovanog
                racun.KorisnikId = korisnik.KorisnikId;
                var zadnjiRacun = context.Racuns.Where(r => r.DatumIzdavanja == racun.DatumIzdavanja).Count();
                racun.BrojRacuna = zadnjiRacun + 1;
                var ukupanIznos = listaKupljenihStavki.Sum(k => k.Kolicina * context.Artikls.Find(k.ArtiklId).Cijena);
                racun.UkupanIznos = ukupanIznos;

                foreach (var item in listaKupljenihStavki)
                {
                    Stavka stavka = new Stavka();
                    stavka.ArtiklId = item.ArtiklId;
                    stavka.Kolicina = item.Kolicina;
                    stavka.Cijena = context.Artikls.Find(item.ArtiklId).Cijena;  //nema u VM
                    racun.Stavkas.Add(stavka);  //dodajem stavke na racun

                    context.Artikls.Find(item.ArtiklId).Kolicina -= (int)stavka.Kolicina;
                }
                context.Racuns.Add(racun);
                context.SaveChanges();

                return Json(new { Success = true });
            }
                
        }
    }
}
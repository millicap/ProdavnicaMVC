using ProdavnicaMVC.DBProdavnica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using ProdavnicaMVC.Models;
using ProdavnicaMVC.Helpers;

namespace ProdavnicaMVC.Controllers
{
    public class KorisnikController : Controller
    {
        // GET: Korisnik
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListKorisnik(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var context = new ProdavnicaContext())
                {
                    var korisnici = context.Korisniks.Select(k => new
                    {
                        k.KorisnikId,
                        k.Ime,
                        k.Prezime,
                        k.Adresa,
                        k.Pol,
                        k.DatumRodjenja,
                        k.Username,
                        k.Password
                        
                    }).ToList();

                    var count = korisnici.Count();
                    var records = korisnici.OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize).ToList();

                    //Return result to jTable
                    return Json(new { Result = "OK", Records = records, TotalRecordCount = count });
                }

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateKorisnik(KorisnikViewModel korisnikVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                using (var context = new ProdavnicaContext())
                {
                    Korisnik korisnik = new Korisnik()
                    {
                        KorisnikId = korisnikVM.KorisnikId,
                        Ime = korisnikVM.Ime,
                        Prezime = korisnikVM.Prezime,
                        Adresa = korisnikVM.Adresa,
                        Pol = korisnikVM.Pol,
                        DatumRodjenja = korisnikVM.DatumRodjenja,
                        Username = korisnikVM.Username,
                        Password =Encryptor.MD5Hash(korisnikVM.Password)
                    };

                    context.Korisniks.Add(korisnik);
                    context.SaveChanges();

                    korisnikVM.KorisnikId = korisnik.KorisnikId;
              
                    return Json(new { Result = "OK", Record = korisnikVM });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateKorisnik(KorisnikViewModel korisnikVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }
                using (var context = new ProdavnicaContext())
                {
                    Korisnik korisnikUpdate = context.Korisniks.Find(korisnikVM.KorisnikId);

                    korisnikUpdate.KorisnikId = korisnikVM.KorisnikId;
                    korisnikUpdate.Ime = korisnikVM.Ime;
                    korisnikUpdate.Prezime = korisnikVM.Prezime;
                    korisnikUpdate.Adresa = korisnikVM.Adresa;
                    korisnikUpdate.Pol = korisnikVM.Pol;
                    korisnikUpdate.DatumRodjenja = korisnikVM.DatumRodjenja;
                    korisnikUpdate.Username = korisnikVM.Username;
                    korisnikUpdate.Password = korisnikVM.Password;

                    context.SaveChanges();
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteKorisnik(int korisnikId)
        {
            try
            {
                using (var context = new ProdavnicaContext())
                {

                    context.Korisniks.Remove(context.Korisniks.Find(korisnikId));
                    context.SaveChanges();
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


    }
}
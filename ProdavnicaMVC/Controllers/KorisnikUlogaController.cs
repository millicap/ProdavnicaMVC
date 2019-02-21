using ProdavnicaMVC.DBProdavnica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using ProdavnicaMVC.Models;

namespace ProdavnicaMVC.Controllers
{
    public class KorisnikUlogaController : Controller
    {
        // GET: KorisnikUloga
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListKorisnikUloga(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var context = new ProdavnicaContext())
                {
                    var kUloge = context.KorisnikUlogas.Select(ku => new
                    {
                        ku.KorisnikUlogaId,
                        ku.KorisnikId,
                        ku.UlogaId
                       
                    }).ToList();

                    var count = kUloge.Count();
                    var records = kUloge.OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize).ToList();



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
        public JsonResult CreateKorisnikUloga(KorisnikUlogaViewModel kUlogaVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                using (var context = new ProdavnicaContext())
                {
                    //onemoguciti dodavanje vise uloga za istog korisnika
                    var korisnik = context.Korisniks.Find(kUlogaVM.KorisnikId);   //u korisnik trazim kuloga.korisnikId
                    var ima = korisnik.KorisnikUlogas.Any(k => k.UlogaId == kUlogaVM.UlogaId); //ako postoji ta uloga u ulogama
                    if (ima)
                    {
                        return Json(new { Result = "ERROR", Message = "Uloga vec postoji za izabranog korisnika!" });
                    }


                    KorisnikUloga korisnikUloga = new KorisnikUloga()
                    {
                        KorisnikId = kUlogaVM.KorisnikId,
                        UlogaId = kUlogaVM.UlogaId,
                        KorisnikUlogaId = kUlogaVM.KorisnikUlogaId
                    };
                    context.KorisnikUlogas.Add(korisnikUloga);
                    context.SaveChanges();

                    kUlogaVM.KorisnikUlogaId = korisnikUloga.KorisnikUlogaId;

                    return Json(new { Result = "OK", Record = kUlogaVM });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateKorisnikUloga(KorisnikUloga kUloga)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                using (var context = new ProdavnicaContext())
                {
                    var korisnik = context.Korisniks.Find(kUloga.KorisnikId);   //u korisnik trazim kuloga.korisnikId
                    var ima = korisnik.KorisnikUlogas.Any(k => k.UlogaId == kUloga.UlogaId && k.KorisnikUlogaId!=k.KorisnikUlogaId); //ako postoji ta uloga u ulogama
                    if (ima)
                    {
                        return Json(new { Result = "ERROR", Message = "Uloga vec postoji za izabranog korisnika!" });
                    }

                    KorisnikUloga kUlogaUpdate = context.KorisnikUlogas.Find(kUloga.KorisnikUlogaId);

                    kUlogaUpdate.KorisnikUlogaId = kUloga.KorisnikUlogaId;
                    kUlogaUpdate.KorisnikId = kUloga.KorisnikId;
                    kUlogaUpdate.UlogaId = kUloga.UlogaId;

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
        public JsonResult DeleteKorisnikUloga(int KorisnikUlogaId)
        {
            try
            {
                using (var context = new ProdavnicaContext())
                {
                    context.KorisnikUlogas.Remove(context.KorisnikUlogas.Find(KorisnikUlogaId));
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
        public JsonResult VratiKorisnika()
        {
            try
            {
                using (var context = new ProdavnicaContext())
                {
                    var korisnici = context.Korisniks.Select(k => new
                    {
                        Value = k.KorisnikId,
                        DisplayText = k.Ime + "" + k.Prezime
 
                    }).ToList();


                    //Return result to jTable
                    return Json(new { Result = "OK", Options = korisnici });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult VratiUlogu()
        {
            try
            {
                using (var context = new ProdavnicaContext())
                {
                    var uloge = context.Ulogas.Select(u => new
                    {
                        Value = u.UlogaId,
                        DisplayText = u.Naziv

                    }).ToList();
                    //Return result to jTable
                    return Json(new { Result = "OK", Options = uloge });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
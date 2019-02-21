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
    public class UlogaController : Controller
    {
        // GET: Uloga
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListUloga(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var context = new ProdavnicaContext())
                {
                    var uloge = context.Ulogas.Select(u => new
                    {
                        u.UlogaId,
                        u.Naziv
                    }).ToList();

                    var count = uloge.Count();
                    var records = uloge.OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize).ToList();



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
        public JsonResult CreateUloga(UlogaViewModel ulogaVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                using (var context = new ProdavnicaContext())
                {
                    Uloga uloga = new Uloga()
                    {
                        UlogaId = ulogaVM.UlogaId,
                        Naziv = ulogaVM.Naziv
                    };
                    context.Ulogas.Add(uloga);
                    context.SaveChanges();

                    ulogaVM.UlogaId = uloga.UlogaId;

                    return Json(new { Result = "OK", Record = ulogaVM });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateUloga(UlogaViewModel ulogaVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                using (var context = new ProdavnicaContext())
                {
                    Uloga ulogaUpdate = context.Ulogas.Find(ulogaVM.UlogaId);

                    ulogaUpdate.UlogaId = ulogaVM.UlogaId;
                    ulogaUpdate.Naziv = ulogaVM.Naziv;
                    
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
        public JsonResult DeleteUloga(int ulogaId)
        {
            try
            {
                using (var context = new ProdavnicaContext())
                {

                    context.Ulogas.Remove(context.Ulogas.Find(ulogaId));
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
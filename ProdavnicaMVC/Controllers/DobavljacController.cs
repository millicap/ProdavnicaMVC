using ProdavnicaMVC.DBProdavnica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace ProdavnicaMVC.Controllers
{
    public class DobavljacController : Controller
    {
        // GET: Dobavljac
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var context = new ProdavnicaContext())
                {
                    var dobavljaci = context.Dobavljacs.Select(d => new
                    {
                        d.DobavljacId,
                        d.Naziv
                    }).ToList();

                    var count = dobavljaci.Count();
                    var records = dobavljaci.OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize).ToList();

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
        [Authorize(Roles = "Administrator")]
        public JsonResult Create(Dobavljac dobavljac)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                using (var context = new ProdavnicaContext())
                {
                    context.Dobavljacs.Add(dobavljac);
                    context.SaveChanges();
                    return Json(new { Result = "OK", Record = dobavljac });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult Update(Dobavljac dobavljac)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                using (var context = new ProdavnicaContext())
                {
                    Dobavljac dobavljacUpdate = context.Dobavljacs.Find(dobavljac.DobavljacId);

                    dobavljacUpdate.DobavljacId = dobavljac.DobavljacId;
                    dobavljacUpdate.Naziv = dobavljac.Naziv;

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
        [Authorize(Roles = "Administrator")]
        public JsonResult Delete(int dobavljacId)
        {
            try
            {
                using (var context = new ProdavnicaContext())
                {

                    context.Dobavljacs.Remove(context.Dobavljacs.Find(dobavljacId));
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
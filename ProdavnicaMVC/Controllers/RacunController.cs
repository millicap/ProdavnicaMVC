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


    }
}
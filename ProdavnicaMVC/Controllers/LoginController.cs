using ProdavnicaMVC.DBProdavnica;
using ProdavnicaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProdavnicaMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult UlogujSe()
        {
            return View();
        }

        // POST: Account/Create
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UlogujSe(LoginViewModel viewModel, string returnUrl)
        {
            using (var context = new ProdavnicaContext())
            {
                var korisnik = context.Korisniks.FirstOrDefault(k => k.Username == viewModel.Username && k.Password == viewModel.Password);

                if (korisnik != null)
                {
                    var authTicket = new FormsAuthenticationTicket(
                                                         1,
                                                         viewModel.Username,
                                                         DateTime.Now,
                                                         DateTime.Now.AddMinutes(30),
                                                         false,
                                                         //"User",     ovaj ne moze jer ne mogu 2 user-a u 2 stringa
                                                         string.Join(",", korisnik.KorisnikUlogas.Select(u => u.Uloga.Naziv).Distinct())  //user uloga

                                                         //Primjer dodavanja uloga za korisnika
                                                         //string.Join(",", korisnik.KorisnikUlogas.Select(o => o.Uloga.Naziv).Distinct())
                                                         );

                    var encTicket = FormsAuthentication.Encrypt(authTicket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Artikl");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Pogresno korisnicko ime ili lozinka");
                    return View();
                }
            }
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("UlogujSe", "Login");
        }

        [Authorize(Roles = "Administrator, Moderator, Promoter, User")]
        public ActionResult PromijeniSifru()
        {
            using (var context = new ProdavnicaContext())
            {
                var korisnik = context.Korisniks.FirstOrDefault(k => k.Username == User.Identity.Name);
                var izmijeniSifruVM = new PromijeniSifruViewModel()
                {
                    KorisnikId = korisnik.KorisnikId,
                    Username = korisnik.Username,
                    Password = korisnik.Password

                };
                return View(izmijeniSifruVM);
            }

        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Promoter, User")]
        public ActionResult PromijeniSifru(PromijeniSifruViewModel promijeniSifruVM)
        {
            using (var context = new ProdavnicaContext())
            {
                if (ModelState.IsValid)
                {
                    //return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });


                    var korisnik = context.Korisniks.Find(promijeniSifruVM.KorisnikId);
                    if (korisnik.Password != promijeniSifruVM.NewPassword)
                    {
                        korisnik.Password = promijeniSifruVM.NewPassword;
                        context.SaveChanges();

                        return RedirectToAction("UlogujSe","Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Greska!");
                        return View(promijeniSifruVM);
                    }
                    
                }
                else
                {
                    //ModelState.AddModelError("", "Greska");
                    return View(promijeniSifruVM);
                }

               
            }


        }
    }
}


   


















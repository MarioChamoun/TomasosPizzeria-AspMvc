using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TomasosPizzeria.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: /<controller>/
            public IActionResult Index()
            {
                return View();
            }
            public IActionResult UpdateProfile()
            {
            if (HttpContext.Session.Get("UserID") == null)
            {
                RedirectToAction("login");
            }

            return View();
             }
            
            [HttpPost]
            public IActionResult UpdateProfile(Kund kund)
        {
            if (ModelState.IsValid)
            {
                using (TomasosContext context = new TomasosContext())
                {
                    int userid = int.Parse(HttpContext.Session.GetString("UserID"));
                    kund.KundId = userid;
                    context.Kund.Update(kund);
                    context.SaveChanges();

                    var user = context.Kund.Where(x => x.KundId == userid).FirstOrDefault(); 
                    //uppdaterar session värdet när användaren uppdaterar profilen
                    HttpContext.Session.SetString("Username", user.AnvandarNamn);
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("Gatuadress", user.Gatuadress);
                    HttpContext.Session.SetString("Namn", user.Namn);
                    HttpContext.Session.SetString("Postnr", user.Postnr);
                    HttpContext.Session.SetString("Postort", user.Postort);
                    HttpContext.Session.SetString("Telefon", user.Telefon);
                    ViewBag.Updated = user.AnvandarNamn + " har uppdaterats";
                }
                ModelState.Clear();
            }
            return View("UpdateProfile");
        }
            public IActionResult register()
            {
                return View();
            }
            [HttpPost]
            public IActionResult register(Kund kund)
            {
                if (ModelState.IsValid)
                {
                    using (TomasosContext context = new TomasosContext())
                    {
                        context.Kund.Add(kund);
                        context.SaveChanges();
                    }
                    ModelState.Clear();
                    ViewBag.Message = kund.AnvandarNamn + " har registrerats.";
                }
                return View("register");
            }

            public IActionResult login()
            {
                return View();
            }
            [HttpPost]
            public IActionResult login(Kund kund)
            {
                using (TomasosContext context = new TomasosContext())
                {
                    var user = context.Kund.Where(x => x.AnvandarNamn == kund.AnvandarNamn && x.Losenord == kund.Losenord).FirstOrDefault();
                    if (user != null)
                    {
                        HttpContext.Session.SetString("UserID", user.KundId.ToString());
                        HttpContext.Session.SetString("Username", user.AnvandarNamn);
                        HttpContext.Session.SetString("Email", user.Email);
                        HttpContext.Session.SetString("Gatuadress", user.Gatuadress);
                        HttpContext.Session.SetString("Namn", user.Namn);
                        HttpContext.Session.SetString("Postnr", user.Postnr);
                        HttpContext.Session.SetString("Postort", user.Postort);
                        HttpContext.Session.SetString("Telefon", user.Telefon);
                    return RedirectToAction("Loggedin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Användarnamn eller lösenord är fel");
                    }
                }
                return View();
            }

            public IActionResult Loggedin()
            {
                if (HttpContext.Session.Get("UserID") != null)
                {
                    return RedirectToAction("../Meny/Korg");
                }
                else
                {
                    return RedirectToAction("login");
                }
            }
            
            public IActionResult logout()
            {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
            }
        }
    }
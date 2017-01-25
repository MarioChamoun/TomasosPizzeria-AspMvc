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
    
    public class MenyController : Controller
    {
        public IActionResult Index()
        {
            TomasosContext context = new TomasosContext();
            var pizzor = context.Matratt.ToList();

            return View(pizzor);
        }

        [HttpGet]
        public IActionResult addkorg(string matnamn,int pris,int matid)
        {
            if (HttpContext.Session.Get("UserID") == null)
            {
                return RedirectToAction("login", "UserAccount", new { area = "" });
            }
            else
            {
                TomasosContext context = new TomasosContext();

                context.Korg.Add(new Korg() { Matrattid = matid, Matrattnamn = matnamn, Pris = pris,KundId = int.Parse(HttpContext.Session.GetString("UserID")) });
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult bestall(int totalbelopp)
        {
            if (HttpContext.Session.Get("UserID") == null)
            {
                return RedirectToAction("login", "UserAccount", new { area = "" });
            }
            using (TomasosContext context = new TomasosContext())
            {
                Bestallning bestall = new Bestallning();
                var userid = int.Parse(HttpContext.Session.GetString("UserID"));
                var korg = context.Korg.Where(x => x.KundId == userid).ToList();
                //beställning
                bestall.Totalbelopp = totalbelopp;
                bestall.BestallningDatum = DateTime.Now;
                bestall.KundId = userid;
                context.Bestallning.Add(bestall);
                context.SaveChanges();
                int bestallid = bestall.BestallningId;
                //beställningmaträtt
                foreach (var item in korg)
                {
                        context.BestallningMatratt.Add(new BestallningMatratt() {MatrattId = item.Matrattid.Value,BestallningId = bestallid});
                        context.SaveChanges();
                }

                var k = context.Korg.Where(x => x.KundId == userid).ToList();
                foreach (var i in k)
                {
                    context.Korg.Remove(i);
                }
                context.SaveChanges();
            }
                return View();
        }
        public IActionResult tabortkorg()
        {
            if (HttpContext.Session.Get("UserID") == null)
            {
                return RedirectToAction("login", "UserAccount", new { area = "" });
            }
            else
            {
                var userid = int.Parse(HttpContext.Session.GetString("UserID"));
                TomasosContext context = new TomasosContext();
                var k = context.Korg.Where(x => x.KundId == userid).ToList();

                foreach (var i in k)
                {
                    context.Korg.Remove(i);
                }
                context.SaveChanges();
            }
            return View("Korg");
        }
        public IActionResult Korg()
        {
            if (HttpContext.Session.Get("UserID") == null)
            {
                return RedirectToAction("login", "UserAccount", new { area = "" });
            }
                TomasosContext context = new TomasosContext();
                var userid = int.Parse(HttpContext.Session.GetString("UserID"));
                var minkorg = context.Korg.Where(x => x.KundId == userid).ToList();
            return View(minkorg);
        }
    }
}

using Domaci_MVC_1.Models;
using Domaci_MVC_1.ViewModels;
using Domaci_MVC_1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Domaci_MVC_1.Controllers
{
    public class PretragaController : Controller
    {
        private static IRepository<Genre> genreRepo = new GenreRepository();
        private static IRepository<Book> bookRepo = new BookRepository();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pretraga(string Name, int tipPretrage)
        {
            List<Book> listaRezultata;
            if (tipPretrage == 1)
            {
               listaRezultata =
                    (from b in bookRepo.GetAll()
                    where b.Name.ToLower().Contains(Name.ToLower())
                    select b).ToList();

            }
            else
            {
                listaRezultata =
                    (from b in bookRepo.GetAll()
                     where b.Chapters.ContainsKey(Name)
                     select b).ToList();
            }

            if (listaRezultata != null && listaRezultata.Count > 0)
            {

                return View(listaRezultata);
            }
            else
            {
                listaRezultata = null;
                return View(listaRezultata);
            }
           

            //string tip = (tipPretrage == 1) ? "nazivima knjiga" : "nazivima poglavlja";
            //return Content(string.Format("Tražim {0} među {1}", Name, tip));
        }
    }
}
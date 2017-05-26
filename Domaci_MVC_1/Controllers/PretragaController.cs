using Domaci_MVC_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Domaci_MVC_1.Controllers
{
    public class PretragaController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pretraga(string Name, int tipPretrage)
        {
            IEnumerable<Book> listaRezultata;
            if (tipPretrage == 1)
            {
               listaRezultata =
                    (from b in BookstoreController.listaKnjiga
                    where b.Name.ToLower().Contains(Name.ToLower())
                    select b).ToList();

            }
            else
            {
                listaRezultata =
                    (from b in BookstoreController.listaKnjiga
                     where b.Chapters.ContainsKey(Name)
                     select b).ToList();
            }

            return View(listaRezultata);

            //string tip = (tipPretrage == 1) ? "nazivima knjiga" : "nazivima poglavlja";
            //return Content(string.Format("Tražim {0} među {1}", Name, tip));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domaci_MVC_1.Models;
namespace Domaci_MVC_1.Controllers
{
    [RoutePrefix("zanr")]
    public class GenreController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            List<Genre> genres = BookstoreController.listaZanrova;
            return View(genres);
        }

        [Route("detalji")]
        public ActionResult Details(int id)
        {
            Genre genre = BookstoreController.listaZanrova.Where(g => g.Id == id).SingleOrDefault();
            if (genre != null)
            {
                return View(genre);
            }
            else
            {
                return Content("nema bre");
            }
        }

        [Route("novi")]
        public ActionResult Create()
        {
            return View();
        }

        [Route("novi")]
        [HttpPost]
        public ActionResult Create(string Name)
        {
            try
            {
                Genre genre = new Genre(Name);
                if (!BookstoreController.listaZanrova.Contains(genre))
                {
                    BookstoreController.listaZanrova.Add(genre);
                }
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Route("izmeni")]
        public ActionResult Edit(int id)
        {
            Genre genre = BookstoreController.listaZanrova.Where(g => g.Id == id).SingleOrDefault();
            if (genre != null)
            {
                return View(genre);
            }
            else
            {
                return Content("nema bre");
            }
        }

        [Route("izmeni")]
        [HttpPost]
        public ActionResult Edit(int id, string Name)
        {
            try
            {
                // TODO: Add update logic here
                Genre genre = BookstoreController.listaZanrova.Where(g => g.Id == id).SingleOrDefault();
                if (genre != null)
                {
                    genre.Name = Name;

                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("nema bre");
                }
                
            }
            catch
            {
                return View();
            }
        }



        [Route("obrisi")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            /*
             Da li bi pri brisanju žanra trebalo setovati na null Genre
             property svih knjiga koje su bile tog žanra ?
             */

            Genre genre = BookstoreController.listaZanrova.Where(g => g.Id == id).SingleOrDefault();
            if (genre != null)
            {
                genre.isDeleted = true;
                return RedirectToAction("Index");

            }
            else
            {
                return Content("nema bre");
            }
        }
    }
}

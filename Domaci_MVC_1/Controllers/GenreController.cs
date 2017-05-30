using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domaci_MVC_1.Models;
namespace Domaci_MVC_1.Controllers
{
    public class GenreController : Controller
    {
        // GET: Genre
        public ActionResult Index()
        {
            List<Genre> genres = BookstoreController.listaZanrova;
            return View(genres);
        }

        // GET: Genre/Details/5
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
            // GET: Genre/Create
            public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
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
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genre/Edit/5
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

        // POST: Genre/Edit/5
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



        // POST: Genre/Delete/5
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domaci_MVC_1.Models;
using Domaci_MVC_1.ViewModels;
using Domaci_MVC_1.Repository;

namespace Domaci_MVC_1.Controllers
{
    [RoutePrefix("zanr")]
    public class GenreController : Controller
    {
        private IRepository<Genre> genreRepo = new GenreRepository();
        private static IRepository<Book> bookRepo = new BookRepository();

        [Route("")]
        public ActionResult Index()
        {
            var genres = genreRepo.GetAll();
            return View(genres);
        }

        [Route("detalji")]
        public ActionResult Details(int id)
        {
            Genre genre = genreRepo.GetById(id);
            var books = bookRepo.GetAll().Where(b => b.Genre.Id == id).ToList();
            if (genre != null)
            {
                if (books.Count > 0)
                {
                    genre.Books = books;
                }
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

            Genre genre = new Genre(Name);
            if (genreRepo.Create(genre))
            {
                return RedirectToAction("Index");
            }
            return View(genre);
            
            //try
            //{
            //    Genre genre = new Genre(Name);
            //    if (!BookstoreController.listaZanrova.Contains(genre))
            //    {
            //        BookstoreController.listaZanrova.Add(genre);
                    
            //    }
            //    if (genreRepo.Create(genre))
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        return View();

            //    }

            //    //return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        [Route("izmeni")]
        public ActionResult Edit(int id)
        {
            Genre genre = genreRepo.GetById(id);
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


            //Genre genre = BookstoreController.listaZanrova.Where(g => g.Id == id).SingleOrDefault();
            Genre genre = genreRepo.GetById(id);
                if (genre != null)
                {
                    genre.Name = Name;
                genreRepo.Update(genre);

                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("nema bre");
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

            //Genre genre = BookstoreController.listaZanrova.Where(g => g.Id == id).SingleOrDefault();
            Genre genre = genreRepo.GetById(id);
            if (genre != null)
            {
                genre.isDeleted = true;
                genreRepo.Delete(id);
                return RedirectToAction("Index");

            }
            else
            {
                return Content("nema bre");
            }
        }
    }
}

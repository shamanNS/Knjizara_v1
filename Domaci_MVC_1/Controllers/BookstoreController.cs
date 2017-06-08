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
    [RoutePrefix("knjige")]
    public class BookstoreController : Controller
    {

        //public static List<Book> listaKnjiga  = new List<Book>();
        //public static List<Genre> listaZanrova = new List<Genre>();
        private static IRepository<Genre> genreRepo = new GenreRepository();
        private static IRepository<Book> bookRepo = new BookRepository();

        #region nekoriscen metod
        //public static void PopuniListuKnjiga()
        //{
        //         Book book1 = new Book("Harry Potter", 200, BookGenre.Science, false);
        //        Book book2 = new Book("Game of Thrones", 222, BookGenre.Comedy, false);

        //        book1.AddChapter("Chapter 1", "Ovo je sadržaj Chapter 1, prve knjige");
        //        book1.AddChapter("Chapter 2", "Ovo je sadržaj Chapter 2, prve knjige");

        //        book2.AddChapter("Chapter 1", "Ovo je sadržaj Chapter 1, druge knjige");
        //        book2.AddChapter("Chapter 2", "Ovo je sadržaj Chapter 2, druge knjige");

        //        listaKnjiga.Add(book1);
        //        listaKnjiga.Add(book2);

        //}
        #endregion
            
        static BookstoreController()
        {
            #region hardocoded liste
            //PopuniListuKnjiga();
            //Genre genre1 = new Genre("Science");
            //Genre genre2 = new Genre("Comedy");
            //Genre genre3 = new Genre("Horror");

            //Book book1 = new Book("Harry Potter", 200, genre1, false);
            //Book book2 = new Book("Game of Thrones", 222, genre2, false);
            //Book book3 = new Book("Lord of the Rings", 300, genre3, false);

            //book1.AddChapter("Chapter 1", "Ovo je sadržaj Chapter 1, prve knjige");
            //book1.AddChapter("Chapter 2", "Ovo je sadržaj Chapter 2, prve knjige");

            //book2.AddChapter("Chapter 1", "Ovo je sadržaj Chapter 1, druge knjige");
            //book2.AddChapter("Chapter 2", "Ovo je sadržaj Chapter 2, druge knjige");

            //book3.AddChapter("Chapter 1", "Ovo je sadržaj Chapter 1, treće knjige");
            //book3.AddChapter("Chapter 2", "Ovo je sadržaj Chapter 2, treće knjige");

            //listaKnjiga.Add(book1);
            //listaKnjiga.Add(book2);
            //listaKnjiga.Add(book3);

            //genre1.Books.Add(book1);
            //genre2.Books.Add(book2);
            //genre3.Books.Add(book3);

            //listaZanrova.Add(genre1);
            //listaZanrova.Add(genre2);
            //listaZanrova.Add(genre3);
        #endregion

            //listaKnjiga = (List<Book>)bookRepo.GetAll();
            //listaZanrova = (List<Genre>)genreRepo.GetAll();
        }
        

        private List<Book> SortBooks(IEnumerable<Book> listaKnjiga, string kriterijum)
        {
           
            List<Book> sortiranaLista;
            switch (kriterijum)
            {
                case "1":
                    sortiranaLista = (from b in listaKnjiga
                                      orderby b.Name ascending
                                      select b).ToList();

                    break;

                case "2":
                    sortiranaLista = (from b in listaKnjiga
                                      orderby b.Name descending
                                      select b).ToList();

                    break;

                case "3":
                    sortiranaLista = (from b in listaKnjiga
                                      orderby b.Price ascending
                                      select b).ToList();

                    break;

                case "4":
                    sortiranaLista = (from b in listaKnjiga
                                      orderby b.Price descending
                                      select b).ToList();
                    break;

                default:
                    sortiranaLista = (from b in listaKnjiga
                                      orderby b.Id ascending
                                      select b).ToList();
                    break;
            }

            return sortiranaLista;

        }

        [Route("dodaj-novu")]
        public ActionResult Index()
        {
            BookGenreViewModel vm = new BookGenreViewModel();

            vm.Genres = genreRepo.GetAll();
            return View(vm);
        }

        [Route("lista-knjiga")]
        public ActionResult List()
        {
            var books = bookRepo.GetAll().Where(b => b.isDeleted == false).ToList();
            return View(books);
            

            #region komentar za SelectList
            /*
             treba dodati SelectList u ViewBag/Session/gde god
             koji se koristi u Html.DropDownListFor da napravi
             select html element sa svim Genre-ovima


            ViewBag.Genres = new SelectList(storeDB.Genres.OrderBy(g => g.Name), 

             "GenreId", "Name", album.GenreId);
             */

            //ViewBag.Genres = new SelectList(listaZanrova.OrderBy(g => g.Name),

            // "Id", "Name", null);
            #endregion
        }



        //akcija za sortiranje
        [Route("lista-knjiga")]
        [HttpPost]
        public ActionResult List(string kriterijumSortiranja)
        {
            var books = bookRepo.GetAll().Where(b => b.isDeleted == false);
            var sortiranaLista = SortBooks(books, kriterijumSortiranja);
            if (sortiranaLista != null & sortiranaLista.Count > 0)
            {
                return View(sortiranaLista);
            }
            else
            {
                return View("List", null);
            }
        }

        [Route("obrisane")]
        public ActionResult Deleted()
        {
            var deletedBooks = bookRepo.GetAll().Where(b => b.isDeleted == true);
            return View(deletedBooks);
        }

        //akcija za sortiranje
        [Route("obrisane")]
        [HttpPost]
        public ActionResult Deleted(string kriterijumSortiranja)
        {
            var deletedBooks = bookRepo.GetAll().Where(b => b.isDeleted == true);
            var sortiranaLista = SortBooks(deletedBooks, kriterijumSortiranja);
            return View(sortiranaLista);
        }

        [HttpPost]

        public ActionResult AddBook(BookGenreViewModel vm)
        {
            Book book = vm.Book;
            Genre genre = genreRepo.GetById(vm.SelectedGenreId);
            book.Genre = genre;
            if (bookRepo.Create(book))
            {
                return RedirectToAction("List");
            }
            return View("Error");
          

            #region komenaterisan kod
            //if (ModelState.IsValid)
            //{

            // listaKnjiga.Add(book);
            // return RedirectToAction("List");
            //}

            //return View(book);

            //if (book != null)
            //{
            //    listaKnjiga.Add(book);
            //    return RedirectToAction("List");
            //}
            //else
            //{
            //    return View(book);
            //}
            #endregion
        }

        [HttpPost]
        public ActionResult DeleteBook(int Id)
        {
            bookRepo.Delete(Id);
          
            return RedirectToAction("List");

            // Book knjiga = null;
            //foreach (Book b in listaKnjiga)
            //{
            //    if (b.Id == Id)
            //    {
            //        b.isDeleted = true;
            //       // UKLANJANJE KNJIGE IZ Genre.Books kolekcije!
            //        b.Genre.Books.Remove(b);
            //        break;
            //    }
            //}
        }

        [Route("izmeni")]
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            BookGenreViewModel vm = new BookGenreViewModel();
            vm.Genres = genreRepo.GetAll();

            var book = bookRepo.GetAll().Where(b => b.Id == Id).SingleOrDefault();
            if (book != null)
            {
                vm.Book = book;
                return View(vm);
            }
            else
            {
                //ispraviti da obaveštava da nema knjiga sa tim Id
                return RedirectToAction("List");
            }

        }
        

        [Route("izmeni")]
        [HttpPost]
        //public ActionResult Edit(int Id, string Name, double Price, int GenreId)
        public ActionResult Edit(BookGenreViewModel vm)
        {

            Book book = bookRepo.GetAll().Where(b => b.Id == vm.Book.Id).SingleOrDefault();
            Genre genre = genreRepo.GetAll().Where(g => g.Id == vm.SelectedGenreId).SingleOrDefault();

            if (book != null && genre != null)
            {
                book.Name = vm.Book.Name;
                book.Price = vm.Book.Price;
                book.Genre = genre;

                bookRepo.Update(book);

                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }
    }
}

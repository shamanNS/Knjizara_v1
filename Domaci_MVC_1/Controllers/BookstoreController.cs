using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domaci_MVC_1.Models;

namespace Domaci_MVC_1.Controllers
{
    public class BookstoreController : Controller
    {

        public static List<Book> listaKnjiga  = new List<Book>();

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
            //PopuniListuKnjiga();
            Book book1 = new Book("Harry Potter", 200, BookGenre.Science, false);
            Book book2 = new Book("Game of Thrones", 222, BookGenre.Comedy, false);

            book1.AddChapter("Chapter 1", "Ovo je sadržaj Chapter 1, prve knjige");
            book1.AddChapter("Chapter 2", "Ovo je sadržaj Chapter 2, prve knjige");

            book2.AddChapter("Chapter 1", "Ovo je sadržaj Chapter 1, druge knjige");
            book2.AddChapter("Chapter 2", "Ovo je sadržaj Chapter 2, druge knjige");

            listaKnjiga.Add(book1);
            listaKnjiga.Add(book2);
        }


        static bool CheckForDeletedBooks() {
            bool retVal = false;
            List<Book> filtriranaLista = new List<Book>();
            for (int i = 0; i < listaKnjiga.Count; i++)
            {
                if (listaKnjiga[i].isDeleted == true)
                {
                    filtriranaLista.Add(listaKnjiga[i]);
                    retVal = true;
                    break;
                }
            }

            return retVal;
        }

        static List<Book> SortBooks(string kriterijum)
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
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {

            /*
             * ukoliko želim da sadržaj view izgleda drugačije
             * ukoliko nema šta da prikaže, znači da ne prikazuje
             * praznu tabelu ovde bi u principu trebalo nešto
             * na sličan fazon kao za Deleted metod ispod
              */


            return View(listaKnjiga);
        }



        //akcija za sortiranje
        [HttpPost]
        public ActionResult List(string kriterijumSortiranja)
        {
            var sortiranaLista = SortBooks(kriterijumSortiranja);
            if (sortiranaLista != null & sortiranaLista.Count > 0)
            {
                return View(sortiranaLista);
            }
            else
            {
                return View("List", null);
            }
            //return View(sortiranaLista);
        }

        public ActionResult Deleted()
        {
            /*
             * ukoliko želim da sadržaj view izgleda drugačije
             * ukoliko nema šta da prikaže, znači da ne prikazuje
             * praznu tabelu ovde bi u principu trebalo proveri jel lista 
             * ima obrisanih knjiga i na osnovu toga rezultuje različitim prikazom
              */

            #region komentarisan kod
            //bool HasDeletedBooks = false;

            //for (int i = 0; i < listaKnjiga.Count; i++)
            //{
            //    if (listaKnjiga[i].isDeleted == true)
            //    {
            //        HasDeletedBooks = true;
            //        break;
            //    }
            //}

            //if (HasDeletedBooks != false)
            //{
            //    return View(listaKnjiga);
            //}
            //else
            //{
            //    //return null;
            //    return Content("<h2>Trenutno nema obrisanih knjiga!</h2>");
            //}
           #endregion
            if (CheckForDeletedBooks() == true)
            {
                return View(listaKnjiga);
            }
            else
            {
                return View("Deleted", null);
            }

        }

        [HttpPost]
        public ActionResult Deleted(string kriterijumSortiranja)
        {
            var sortiranaLista = SortBooks(kriterijumSortiranja);
            return View(sortiranaLista);
        }

        [HttpPost]
        public ActionResult AddBook(string Name, double Price, BookGenre Genre)
        {
            Book book = new Book(Name, Price, Genre, false);
            
            bool PostojiVec = false;
            foreach (Book b in listaKnjiga)
            {
                if (b.Name.ToLower() == book.Name.ToLower())
                {
                    PostojiVec = true;
                    break;
                }
            }
            if (!PostojiVec)
            {
                listaKnjiga.Add(book);
                return RedirectToAction("List");
            }
            else
            {
                return Content(String.Format("<h3>Već postoji knjiga sa nazivom {0} !<br />", Name));
            }

            //listaKnjiga.Add(book);
            //return RedirectToAction("List");


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
        #region book kao parametar
        //[HttpPost]
        //public ActionResult DeleteBook(Book book)
        //{

        //    //if (ModelState.IsValid)
        //    //{
        //    //    
        //    //    return RedirectToAction("List");
        //    //}

        //    if (book != null && listaKnjiga.Contains(book))
        //    {
        //        int pozicija = listaKnjiga.IndexOf(book);
        //        listaKnjiga[pozicija].isDeleted = true;
        //        return RedirectToAction("List");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
        #endregion


        [HttpPost]
        public ActionResult DeleteBook(int Id)
        {
            Book knjiga = null;
            foreach (Book b in listaKnjiga)
            {
                if (b.Id == Id)
                {
                    knjiga = b;
                    break;
                }
            }
            if (knjiga != null)
            {
                int index = listaKnjiga.IndexOf(knjiga);
                listaKnjiga[index].isDeleted = true;
                return RedirectToAction("List");
            }
            else
            {
                /* ovde bi onda trebalo izbaciti Alert u browseru
                 * koji obaveštava korisnika da ne postoji knjiga sa datim ID-em
                 * ili dodavanje novom DOM elementa koje sadrži sličnu informaciju
                 * ali oboje podrazumeva da znamo JavaScript :P
                 * 
                 * Mada, pitanje je da li uopšte ovde u kodu treba osigurati podršku i za takav neki scenario
                 * ako po dizajnu ovoj Akciji se pristupa isključivo putem "Delete" action linka
                 * koji se pojavljuje u "All books" tabeli.
                 * Ali neko uvek može da van UI proba da pošalje zahtev na pravi route...
                 * 
                 * Ne bi baš trebalo da se samo redirektuje na listu svih knjiga kao što radim ispod:
                */
                return RedirectToAction("List");
            }


        }
    }
}

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

        public  static List<Book> listaKnjiga = new List<Book>()
            {
                new Book("Harry Potter", 200, BookGenre.Science, false),
                 new Book("Game of Thrones", 222, BookGenre.Comedy, false)
            };


        public ActionResult Index()
        {
            
            return View();
        }


        public ActionResult List()
        {

            /*
             * ukoliko želim da sadržaj view izgleda drugačije
             * ukoliko nema šta da prikaže, znači da ne prikazuje
             * praznu tabelu ovde bi u principu trebalo napraviti podListu knjiga
             * kod kojih je isDeleted == false i tu listu proslediti kao model na view
             * tj proslediti null ako nema knjiga koje zadovoljavaju kriterijum
             * jer u view kodu proveravam da li je model null
              */
            return View(listaKnjiga);
        }

        public ActionResult Deleted()
        {
            /*
             * ukoliko želim da sadržaj view izgleda drugačije
             * ukoliko nema šta da prikaže, znači da ne prikazuje
             * praznu tabelu ovde bi u principu trebalo napraviti podListu knjiga
             * kod kojih je isDeleted == false i tu listu proslediti kao model na view
             * tj proslediti null ako nema knjiga koje zadovoljavaju kriterijum
             * jer u view kodu proveravam da li je model null
              */

            return View(listaKnjiga);
        }

        [HttpPost]
        //public ActionResult AddBook(Book book)
        public ActionResult AddBook(string Name, double Price, BookGenre Genre)
        {

            /*
             * fali naravno prvo provera da li lista/baza već sadrži knjgu
             * što bi se objektivno radilo preko provere Id, znači trebala
             * bi izmena parametara metode/akcije
              */
            Book book = new Book(Name, Price, Genre, false);
            listaKnjiga.Add(book);
            return RedirectToAction("List");


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

            /* PITATI NA PREDAVANJU
             * kako bi se linkovao "Delete" link u "All books" tabeli
             * da brisanje radi pomoću POST metode umesto GET?
              */
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

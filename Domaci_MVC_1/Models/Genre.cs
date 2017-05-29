using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domaci_MVC_1.Models
{
    public class Genre
    {
        private static int IdCounter = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }

        public Genre()
        {
            this.Id = IdCounter;
            IdCounter++;
            this.Books = new List<Book>();
            this.Name = "";
        }
        public Genre(string name)
        {
            
                this.Id = IdCounter;
                IdCounter++;
            this.Books = new List<Book>();
                this.Name = name;

        }
        public Genre(string name, List<Book> books)
        {
            this.Id = IdCounter;
            IdCounter++;
            this.Books =books;
            this.Name = name;
        }
    }
}
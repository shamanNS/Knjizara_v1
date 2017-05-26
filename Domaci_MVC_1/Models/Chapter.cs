using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domaci_MVC_1.Models
{
    public class Chapter
    {
        public static int IdCounter = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public Book Book { get; set; }

        public Chapter(string name, string content, Book book)
        {
            this.Id = IdCounter;
            IdCounter++;

            this.Name = name;
            this.Content = content;
            this.Book = book;
        }
    }
}
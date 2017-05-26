using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domaci_MVC_1.Models
{
   public enum BookGenre
    {
        Science = 1,
        Comedy = 2,
        Horror = 3
    }
    public class Book
    {
        public static int idCounter = 1;

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public BookGenre Genre { get; set; }
        public bool isDeleted { get; set; } = false;

       

        public Book(string name, double price, BookGenre genre, bool isDeleted)
        {
            this.Id = idCounter;
            idCounter++;
            this.Name = name;
            this.Price = price;
            this.Genre = genre;
            this.isDeleted = isDeleted;
        }
    }
}
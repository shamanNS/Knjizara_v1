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
        public Dictionary<string, Chapter> Chapters { get; set; }
        public bool isDeleted { get; set; } = false;

       

        public Book(string name, double price, BookGenre genre, bool isDeleted /*,List<Chapter> chapters*/)
        {
            this.Id = idCounter;
            idCounter++;
            this.Name = name;
            this.Price = price;
            this.Genre = genre;
            this.isDeleted = isDeleted;
            this.Chapters = new Dictionary<string, Chapter>();
            /*this.Chapters = new Dictionary<string, Chapter>();

            if (chapters !=null && chapters.Count > 0)
            {
                for (int i = 0; i < chapters.Count; i++)
                {
                    this.Chapters.Add(chapters[i].Name, chapters[i]);

                }
            }
            */
            
           
        }

        public void AddChapter( string name, string content)
        {
            Chapter chapter = new Chapter(name, content, this);
            this.Chapters.Add(chapter.Name, chapter);
        }
    }
}
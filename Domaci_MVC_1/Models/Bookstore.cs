using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domaci_MVC_1.Models
{
    public class Bookstore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
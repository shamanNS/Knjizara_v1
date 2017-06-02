using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domaci_MVC_1.Models;

namespace Domaci_MVC_1.ViewModels
{
    public class BookGenreViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public int SelectedGenreId { get; set; }
    }
}
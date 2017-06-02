using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domaci_MVC_1.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Domaci_MVC_1.Repository
{
    public class BookRepository : IRepository<Book>
    {
        private SqlConnection conn;
        private void LoadConnection()
        {
            //string connString = ConfigurationManager.ConnectionStrings["AlephDbContext"].ConnectionString;
            string connString = ConfigurationManager.ConnectionStrings["HomeDbContext"].ConnectionString;
            conn = new SqlConnection(connString);
        }

        
        public bool Create(Book obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAll()
        {
            LoadConnection();
            string query = "SELECT BookId, BookName, BookPrice, Book.GenreId, GenreName FROM Book JOIN Genre ON Book.GenreId = Genre.GenreId;";
            DataTable dt = new DataTable(); // objekti u 
            DataSet ds = new DataSet();     // koje smestam podatke


            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;

                SqlDataAdapter dadapter = new SqlDataAdapter(); // bitan objekat pomocu koga preuzimamo podatke i izvrsavamo upit
                dadapter.SelectCommand = cmd;                   // nakon izvrsenog upita

                // Fill(...) metoda je bitna, jer se prilikom poziva te metode izvrsava upit nad bazom podataka
                dadapter.Fill(ds, "Books"); // naziv tabele u dataset-u
                dt = ds.Tables["Books"];    // formiraj DataTable na osnovu tabele u DataSet-u
                conn.Close();                  // zatvori konekciju
            }

            List<Book> Books = new List<Book>();

            foreach (DataRow dataRow in dt.Rows)            // izvuci podatke iz svakog reda tj. zapisa tabele
            {
                int BookId = int.Parse(dataRow["BookId"].ToString());
                string BookName = dataRow["BookName"].ToString();
                double BookPrice = double.Parse(dataRow["BookPrice"].ToString());
                int GenreId = int.Parse(dataRow["GenreId"].ToString());    // iz svake kolone datog reda izvuci vrednost
                string GenreName = dataRow["GenreName"].ToString();

                Genre genre = new Genre() {Id = GenreId, Name = GenreName, isDeleted = false };
                Books.Add(new Book() { Id = BookId, Name = BookName, Price = BookPrice, Genre = genre, isDeleted = false });
            }

            return Books;
        }

        public Book GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Book obj)
        {
            throw new NotImplementedException();
        }
    }
}
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
    public class GenreRepository : IRepository<Genre>
    {

        private SqlConnection conn;
        private void LoadConnection()
        {
            //string connString = ConfigurationManager.ConnectionStrings["AlephDbContext"].ConnectionString;
            string connString = ConfigurationManager.ConnectionStrings["HomeDbContext"].ConnectionString;
            conn = new SqlConnection(connString);
        }

        public bool Create(Genre genre)
        {
            string query = "INSERT INTO Genre (GenreName) VALUES (@GenreName);";
            query += " SELECT SCOPE_IDENTITY()";        // selektuj id novododatog zapisa nakon upisa u bazu

            LoadConnection();   // inicijaizuj novu konekciju

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@GenreName", genre.Name);

                conn.Open();
                var newFormedId = cmd.ExecuteScalar();              // izvrsi upit nad bazom, vraca id novododatog zapisa
                conn.Close();

                if (newFormedId != null)
                {
                    return true;    // upis uspesan, generisan novi id
                }
            }
            return false;   // upis bezuspesan
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Genre> GetAll()
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
                dadapter.Fill(ds, "Genres"); // 'ProductCategory' je naziv tabele u dataset-u
                dt = ds.Tables["Genres"];    // formiraj DataTable na osnovu ProductCategory tabele u DataSet-u
                conn.Close();                  // zatvori konekciju
            }

            List<Genre> Genres = new List<Genre>();

            foreach (DataRow dataRow in dt.Rows)            // izvuci podatke iz svakog reda tj. zapisa tabele
            {
                int GenreId = int.Parse(dataRow["GenreId"].ToString());    // iz svake kolone datog reda izvuci vrednost
                string GenreName = dataRow["GenreName"].ToString();

                Genres.Add(new Genre() { Id = GenreId, Name = GenreName });
            }

            return Genres;
        }

        public Genre GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Genre obj)
        {
            throw new NotImplementedException();
        }
    }
}
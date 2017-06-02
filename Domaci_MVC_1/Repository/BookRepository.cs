using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domaci_MVC_1.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Domaci_MVC_1.Repository
{
    public class BookRepository : IRepository<Book>
    {
        private SqlConnection conn;
        private void LoadConnection()
        {
            //string connString = ConfigurationManager.ConnectionStrings["AlephDbContext"].ConnectionString;
            string connString = ConfigurationManager.ConnectionStrings["HomeDbContext"].ConnectionString;
            conn = new SqlConnection();
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
            throw new NotImplementedException();
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
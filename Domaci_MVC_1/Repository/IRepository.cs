using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaci_MVC_1.Repository
{
    interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        bool Create(TEntity obj);
        void Update(TEntity obj);
        void Delete(int id);
    }
}

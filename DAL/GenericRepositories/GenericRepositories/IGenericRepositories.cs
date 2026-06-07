using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.GenericRepositories.GenericRepositories
{
    public interface IGenericRepositories<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity? GetById(int id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity );
        void SaveChanges();


    }
}

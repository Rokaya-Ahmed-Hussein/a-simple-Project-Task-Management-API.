using DAL.Data.Context;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.GenericRepositories.GenericRepositories
{
    public class GenericRepositories<TEntity> : IGenericRepositories<TEntity> where TEntity : class
    {
        private readonly AppDbContext _appDbContext;
        public GenericRepositories(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public List<TEntity> GetAll()
        {
            
                return _appDbContext.Set<TEntity>().ToList();

        }

        public TEntity? GetById(int id)
        {
            return _appDbContext.Set<TEntity>().Find(id);

        }

        //CRUD Operations
        public void Create(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Remove(entity);
        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {

        }

    }
}

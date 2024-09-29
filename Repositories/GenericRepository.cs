using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MyDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MyDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Product)) 
            {
                return _dbSet.Include("Category").ToList();
            }
            else if (typeof(T) == typeof(Category))
            {
                return _dbSet.Include("Products").ToList();
            }
            else
            {
                return _dbSet.ToList(); 
            }
        }

        public T GetById(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return _dbSet.Include("Category").FirstOrDefault(p => ((Product)(object)p).ProductId == id) as T;
            }
            else if (typeof(T) == typeof(Category))
            {
                return _dbSet.Include("Products").FirstOrDefault(c => ((Category)(object)c).CategoryId == id) as T;
            }
            else
            {
                return _dbSet.Find(id);
            }
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}


using BussinessObjects.Models;
using Repositories;
using Repositories.Interfaces;
using UnitOfWorks.Interfaces;

namespace UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _context;

        private IGenericRepository<Product> _products;
        private IGenericRepository<Category> _categories;

        public UnitOfWork(MyDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Product> Products =>
            _products ??= new GenericRepository<Product>(_context);

        public IGenericRepository<Category> Categories =>
            _categories ??= new GenericRepository<Category>(_context);

        public int Complete()
        {
            return _context.SaveChanges(); 
        }

        public void Dispose()
        {
            _context.Dispose(); 
        }
    }
}

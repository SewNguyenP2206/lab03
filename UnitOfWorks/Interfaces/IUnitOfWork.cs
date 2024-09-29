using BussinessObjects.Models;
using Repositories.Interfaces;
using System;

namespace UnitOfWorks.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }
        int Complete(); 
    }
}

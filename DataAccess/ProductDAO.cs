using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
   public class ProductDAO
    {
        // Method to get all products
        public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using (var context = new MyDbContext())
                {
                    listProducts = context.Products.Include(p => p.Category).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProducts;
        }

        // Method to find a product by its ID
        public static Product FindProductById(int prodId)
        {
            Product p = null;
            try
            {
                using (var context = new MyDbContext())
                {
                    p = context.Products.SingleOrDefault(x => x.ProductId == prodId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        // Method to save a product
        public static void SaveProduct(Product p)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Products.Add(p);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateProduct(Product p)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Method to delete a product
        public static void DeleteProduct(Product p)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var productToDelete = context.Products.SingleOrDefault(c => c.ProductId == p.ProductId);
                    if (productToDelete != null)
                    {
                        context.Products.Remove(productToDelete);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

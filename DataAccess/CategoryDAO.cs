using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDAO
    {
        // Method to get all Categorys
        public static List<Category> GetCategories()
        {
            var listCategorys = new List<Category>();
            try
            {
                using (var context = new MyDbContext())
                {
                    listCategorys = context.Categories.Include(p => p.Products).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCategorys;
        }

        // Method to find a Category by its ID
        public static Category FindCategoryById(int prodId)
        {
            Category p = null;
            try
            {
                using (var context = new MyDbContext())
                {
                    p = context.Categories.SingleOrDefault(x => x.CategoryId == prodId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        // Method to save a Category
        public static void SaveCategory(Category p)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Categories.Add(p);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateCategory(Category p)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<Category>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Method to delete a Category
        public static void DeleteCategory(Category p)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var CategoryToDelete = context.Categories.SingleOrDefault(c => c.CategoryId == p.CategoryId);
                    if (CategoryToDelete != null)
                    {
                        context.Categories.Remove(CategoryToDelete);
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

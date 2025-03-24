using EFCoreProductApp.Business.Models;
using EFCoreProductApp.Business.Repository;
using EFCoreProductApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreProductApp.DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                _context.SaveChanges();
            }
        }

        //public void DeleteCategory(int id)
        //{
        //    var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        //    if (category != null)
        //    {
        //        _context.Categories.Remove(category);
        //        _context.SaveChanges();
        //    }
        //}
        public void DeleteCategory(int id)
        {
            var category = _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);

            if (category != null && !category.Products.Any())  // Check if products exist
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Cannot delete category with associated products.");
            }
        }
        //public bool DeleteCategory(int id)
        //{
        //    var category = _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);

        //    if (category != null && !category.Products.Any())  // If no associated products
        //    {
        //        _context.Categories.Remove(category);
        //        _context.SaveChanges();
        //        return true;  // Successfully deleted
        //    }

        //    return false;  // Deletion failed due to associated products
        //}


    }
}

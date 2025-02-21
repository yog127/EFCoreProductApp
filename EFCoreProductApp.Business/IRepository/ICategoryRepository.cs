using EFCoreProductApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreProductApp.Business.Repository
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetAllCategories();
        public Category GetCategoryById(int id);
        public void AddCategory(Category category);
        public void UpdateCategory(Category category);
        public void DeleteCategory(int id);
    }
}

using EFCoreProductApp.Business.Models;
using EFCoreProductApp.Business.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EFCoreProductApp.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var categories = _categoryRepository.GetAllCategories().ToList();
            ViewBag.TotalPages = Math.Ceiling((double)categories.Count() / pageSize);
            categories = _categoryRepository.GetAllCategories()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category != null)
            {
                _categoryRepository.AddCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category != null)
            {
                _categoryRepository.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryRepository.DeleteCategory(id);
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public IActionResult DeleteCategory(int id)
        //{
        //    bool isDeleted = _categoryRepository.DeleteCategory(id);

        //    if (!isDeleted)
        //    {
        //        TempData["ErrorMessage"] = "Cannot delete this category because it has associated products.";
        //    }

        //    return RedirectToAction("CategoryList");  // Adjust according to your view name
        //}

    }
}

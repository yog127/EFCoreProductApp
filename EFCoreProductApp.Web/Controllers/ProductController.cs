using EFCoreProductApp.Business.Models;
using EFCoreProductApp.Business.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EFCoreProductApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var products = _productRepository.GetAllProducts().ToList();
            ViewBag.TotalPages = Math.Ceiling((double)products.Count / pageSize);

            products = _productRepository.GetAllProducts()
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();


            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (product != null)
            {
                _productRepository.AddProduct(product);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (product != null)
            {
                _productRepository.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        //public IActionResult HandlePagination(int pageNumber)
        //{

        //}
    }
}

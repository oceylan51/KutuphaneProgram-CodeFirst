using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF_CodeFirst.Models;
using EF_CodeFirst.Models.Context;
using System.IO;
namespace Controllers
{
    public class CategoriesController : Controller
    {
        public readonly EF_CodeFirst.Models.Context.LibraryContext _context;

        public CategoriesController(LibraryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.Where(x => x.IsDeleted == false).ToList();
            ViewData["Result"] = "0";
            return View(categories);
        }
        public IActionResult GetDeletedCategories()
        {
            ViewData["Result"] = "1";
            var categories = _context.Categories.Where(x => x.IsDeleted == true).ToList();
            return View("Index", categories);

        }
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _context.Categories.Find(id);
            category.IsDeleted = true;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var categories = _context.Categories.Find(id);
            return View(categories);
        }
        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            _context.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var selectedCategory = _context.Categories.Find(id);
            return View(selectedCategory);
        }
    }
}
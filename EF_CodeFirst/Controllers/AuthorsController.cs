using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF_CodeFirst.Models;
using EF_CodeFirst.Models.Context;



namespace Controllers
{
    public class AuthorsController : Controller
    {
        public readonly LibraryContext _context;
        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["Result"] = "0";
            var author = _context.Authors.Where(x => x.IsDeleted == false).ToList();
            return View(author);
        }
        public IActionResult GetDeletedAuthors()
        {
            ViewData["Result"] = "1";
            var author = _context.Authors.Where(x => x.IsDeleted == true).ToList();
            return View("Index", author);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Author author)
        {
            _context.Add(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var author = _context.Authors.Find(id);
            return View(author);
        }
        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var author = _context.Authors.Find(id);
            return View(author);
        }
        public IActionResult Delete(int id)
        {
            var author = _context.Authors.Find(id);
            return View(author);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var authors = _context.Authors.Find(id);
            authors.IsDeleted = true;
            _context.Authors.Update(authors);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
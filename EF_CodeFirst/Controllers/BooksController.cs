using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF_CodeFirst.Models;
using EF_CodeFirst.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Controllers
{
    public class BooksController : Controller
    {
        public readonly LibraryContext _context;
        public BooksController(LibraryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var book = _context.Books.Where(x => x.IsDeleted == false).Include(y => y.Author).Include(k => k.Category).Include(i => i.Publisher).ToList();
            ViewData["Result"] = "0";
            return View(book);
        }

        public IActionResult IsDeletedBooks()
        {
            ViewData["Result"] = "1";
            var book = _context.Books.Where(x => x.IsDeleted).ToList();
            return View("Index", book);
        }
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories.Where(x => x.IsDeleted == false), "CategoryId", "CategoryName");
            ViewData["Authors"] = new SelectList(_context.Authors.Where(x => x.IsDeleted == false), "AuthorId", "AuthorName");
            ViewData["Publishers"] = new SelectList(_context.Publishers.Where(x => x.IsDeleted == false), "PublisherId", "PublisherName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            _context.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var selectedBook = _context.Books.Find(id);
            ViewData["Categories"] = new SelectList(_context.Categories.Where(x => x.IsDeleted == false), "CategoryId", "CategoryName", selectedBook.CategoryId);
            ViewData["Authors"] = new SelectList(_context.Authors.Where(x => x.IsDeleted == false), "AuthorId", "AuthorName", selectedBook.AuthorId);
            ViewData["Publishers"] = new SelectList(_context.Publishers.Where(x => x.IsDeleted == false), "PublisherId", "PublisherName", selectedBook.PublisherId);
            return View(selectedBook);
        }
        [HttpPost]
        public IActionResult Edit(int id, Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var book = _context.Books.Where(x => x.IsDeleted == false).Include(y => y.Author).Include(t => t.Category).Include(i => i.Publisher).First(s => s.BookId == id);
            return View(book);
        }
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Where(x => x.IsDeleted == false).Include(x => x.Author).Include(x => x.Category).Include(x => x.Publisher).First(x => x.BookId == id);
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteFonfirmed(int id)
        {
            var book = _context.Books.Find(id);
            book.IsDeleted = true;
            _context.Books.Update(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

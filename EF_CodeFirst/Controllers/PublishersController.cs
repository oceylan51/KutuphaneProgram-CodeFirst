using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF_CodeFirst.Models;
using EF_CodeFirst.Models.Context;

namespace Controllers
{
    public class PublishersController : Controller
    {
        public readonly LibraryContext _context;
        public PublishersController(LibraryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["Result"] = "0";
            var publisher = _context.Publishers.Where(x => x.IsDeleted == false).ToList();
            return View(publisher);
        }
        public IActionResult GetDeletedPublishers()
        {
            ViewData["Result"] = "1";
            var publisher = _context.Publishers.Where(x => x.IsDeleted).ToList();
            return View("Index", publisher);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Publisher publisher)
        {
            _context.Add(publisher);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var publisher = _context.Publishers.Find(id);
            return View(publisher);
        }
        [HttpPost]
        public IActionResult Edit(Publisher publisher)
        {
            _context.Update(publisher);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var publisher = _context.Publishers.Find(id);
            return View(publisher);
        }
        public IActionResult Delete(int id)
        {
            var publisher = _context.Publishers.Find(id);
            return View(publisher);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult IsDeletedPublisher(int id)
        {
            var newPublisher = _context.Publishers.Find(id);
            newPublisher.IsDeleted = true;
            _context.Publishers.Update(newPublisher);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
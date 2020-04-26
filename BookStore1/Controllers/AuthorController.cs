using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore1.Models;
using BookStore1.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore1.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookStorRepositore<Author> authorRepositore;

        public AuthorController(IBookStorRepositore<Author> authorRepositore)
        {
            this.authorRepositore = authorRepositore;
        }

        // GET: Author 
        public ActionResult Index()
        {
            var authors = authorRepositore.List();
            return View(authors);
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            var author = authorRepositore.Find(id);
            return View(author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                // TODO: Add insert logic here
                authorRepositore.Add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            var auther = authorRepositore.Find(id);
            return View(auther);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                authorRepositore.Update(id,author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            var auther = authorRepositore.Find(id);
            return View(auther);
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                authorRepositore.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
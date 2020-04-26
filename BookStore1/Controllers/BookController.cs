using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore1.Models.Repositories;
using BookStore1.Models;
using BookStore1.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.IO;

namespace BookStore1.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStorRepositore<Book> bookRepositore;
        private readonly IBookStorRepositore<Author> authorRepository;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookStorRepositore<Book> bookRepositore,
            IBookStorRepositore<Author> authorRepository,
            IHostingEnvironment hosting)    
        {
            this.bookRepositore = bookRepositore;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }


        // GET: Book
        public ActionResult Index()
        {
            var books = bookRepositore.List();
            return View(books);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepositore.Find(id);
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View(GetAllAuthors());
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //string fileName = UploadFile(model.File) == null ? string.Empty : UploadFile(model.File);
                    string fileName = UploadFile(model.File) ?? string.Empty; //either returen filename or empty   

                    if (model.AuthorId == -1)
                    {
                        ViewBag.message = "Please Selected an Author list";
                        return View(GetAllAuthors());
                    }
                    var author = authorRepository.Find(model.AuthorId);
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        ImageUrl = fileName
                    };
                    bookRepositore.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }

            }

            ModelState.AddModelError("", "You have to fill all the required fileds!");
            return View(GetAllAuthors());  
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepositore.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;

            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = authorRepository.List().ToList(),
                ImageUrl = book.ImageUrl
            };
            return View(viewModel);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewmodel)
        {
            try
            {
                string fileName = UploadFile(viewmodel.File, viewmodel.ImageUrl);  // update the image
                var author = authorRepository.Find(viewmodel.AuthorId);
                Book book = new Book
                {
                    Id = viewmodel.BookId,
                    Title = viewmodel.Title,
                    Description = viewmodel.Description,
                    Author = author,
                    ImageUrl = fileName
                };
                bookRepositore.Update(viewmodel.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepositore.Find(id);
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepositore.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "-- Please Selected an Author --" });
            return authors;

        } 

        BookAuthorViewModel GetAllAuthors()
        {
            var amodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return amodel;
        }


        // upload 
        public string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                // WebRootPath    wwwroot   يمثل مجلد  
                string uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                // file.FileName    للحصول ع اسم الملف  
                string fullpath = Path.Combine(uploads, file.FileName);
                // CopyTo()    تسمح بحفظ الملف 
                file.CopyTo(new FileStream(fullpath, FileMode.Create));
                return file.FileName;
            }
            return null;
        }



        // upload new file and delete old file 
        public string UploadFile(IFormFile file, string imageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                string newPath = Path.Combine(uploads, file.FileName);   
                string OldPath = Path.Combine(uploads, imageUrl);

                if (OldPath != newPath)
                {
                    System.IO.File.Delete(OldPath);   //   delete the old image file 
                    file.CopyTo(new FileStream(newPath, FileMode.Create));     // save new file
                }
                return file.FileName;
            }
            return imageUrl;
        }





        public ActionResult Search(string term)
        { 
            var result = bookRepositore.Search(term);
            return View("Index", result);   ///  take the result and display  in view Index
        }

    }
}
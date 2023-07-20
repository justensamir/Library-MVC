using AutoMapper;
using Library.Models;
using Library.Repositories.Interfaces;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepository;
        readonly IMapper mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var books = bookRepository.GetAll();
            return View(books);
        }

        
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(BookVM bookVM) 
        {
            if(ModelState.IsValid)
            {
                var book = mapper.Map<Book>(bookVM);

                bookRepository.Add(book);
                return RedirectToAction("Index");
            }
            return View(bookVM);
        }

        public IActionResult Edit(int id)
        {
            var book = bookRepository.Get(id);
            if(book is null) return View("NotFound");
            var bookVM = mapper.Map<BookVM>(book);

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BookVM bookVM)
        {
            if (id != bookVM.Id) return View("NotFound");

            var borrowersCount = bookRepository.GetBorrowersCount(id);
            if(borrowersCount > bookVM.Copies)
            {
                ModelState.AddModelError("copies", "Borrowers number greater than Copies");
                return View(bookVM);
            }

            if (ModelState.IsValid)
            {
                var book = mapper.Map<Book>(bookVM);

                bookRepository.Update(book);
                return RedirectToAction("Index");
            }
            return View(bookVM);
        }

        public IActionResult Delete(int id)
        {
            var book = bookRepository.Get(id);
            if (book is null) return View("NotFound");
            var borrowersCount = bookRepository.GetBorrowersCount(id);
            if (borrowersCount == 0)
            {
                bookRepository.Delete(book);
                return Ok(book);
            }
            return BadRequest();
        }
    }
}

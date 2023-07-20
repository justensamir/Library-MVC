using AutoMapper;
using Library.Models;
using Library.Repositories.Interfaces;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class BorrowBookController : Controller
    {
        readonly IBorrowerRepository borrowerRepository;
        readonly IBookRepository bookRepository;
        readonly IBorrowBookRepository borrowBookRepository;
        readonly IMapper mapper;
        public BorrowBookController(IBorrowerRepository borrowerRepository, IBookRepository bookRepository, IBorrowBookRepository borrowBookRepository, IMapper mapper)
        {
            this.borrowerRepository = borrowerRepository;
            this.bookRepository = bookRepository;
            this.borrowBookRepository = borrowBookRepository;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Borrow()
        {
            ViewData["Borrowers"] = new SelectList(borrowerRepository.GetAll(), "Code", "Name");
            ViewData["books"] = new SelectList(bookRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Borrow(BorrowBookVM borrowBookVM)
        {
            ViewData["Borrowers"] = new SelectList(borrowerRepository.GetAll(), "Code", "Name");
            ViewData["books"] = new SelectList(bookRepository.GetAll(), "Id", "Name");

            if (borrowBookVM.BorrowerCode == 0 && borrowBookVM.BookId == 0)
            {
                ModelState.AddModelError("", "Borrower and Book are required");
                return View(borrowBookVM);
            }
            if (borrowBookVM.BorrowerCode == 0)
            {
                ModelState.AddModelError("", "Borrower is required");
                return View(borrowBookVM);
            }
            if (borrowBookVM.BookId == 0)
            {
                ModelState.AddModelError("", "Book is required");
                return View(borrowBookVM);
            }

            if (ModelState.IsValid)
            {
                var bookCopiesInStok = bookRepository.GetBookCopiesInStock(borrowBookVM.BookId);
                if (bookCopiesInStok == 0)
                {
                    ModelState.AddModelError("", "No available copies for this book");
                    return View(borrowBookVM);
                }

                var borrowBook = mapper.Map<BorrowBook>(borrowBookVM);
                
                borrowBookRepository.Add(borrowBook);
                return RedirectToAction("Index","Book");
            }
            return View(borrowBookVM);
        }


        public IActionResult Retrieve()
        {
            ViewData["Borrowers"] = new SelectList(borrowerRepository.GetAll(), "Code", "Name");
            ViewData["books"] = new SelectList(bookRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Retrieve(BorrowBookVM borrowBookVM)
        {
            ViewData["Borrowers"] = new SelectList(borrowerRepository.GetAll(), "Code", "Name");
            ViewData["books"] = new SelectList(bookRepository.GetAll(), "Id", "Name");

            if (borrowBookVM.BorrowerCode == 0 && borrowBookVM.BookId == 0)
            {
                ModelState.AddModelError("", "Borrower and Book are required");
                return View(borrowBookVM);
            }
            if (borrowBookVM.BorrowerCode == 0)
            {
                ModelState.AddModelError("", "Borrower is required");
                return View(borrowBookVM);
            }
            if (borrowBookVM.BookId == 0)
            {
                ModelState.AddModelError("", "Book is required");
                return View(borrowBookVM);
            }

            if (ModelState.IsValid)
            {
                var bookBorrowers = bookRepository.GetBorrowersCount(borrowBookVM.BookId);
                if (bookBorrowers == 0)
                {
                    ModelState.AddModelError("", "All copies are in stock");
                    return View(borrowBookVM);
                }
                var borrowBook = borrowBookRepository.GetBorrowedBook(borrowBookVM.BookId, borrowBookVM.BorrowerCode);
                if (borrowBook is null)
                {
                    ModelState.AddModelError("", "This borrower didn't borrow this book");
                    return View(borrowBookVM);
                }
                borrowBookRepository.Delete(borrowBook);
                return RedirectToAction("Index", "Book");
            }

            return View(borrowBookVM);
        }


    }
}

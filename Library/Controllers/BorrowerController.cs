using AutoMapper;
using Library.Models;
using Library.Repositories;
using Library.Repositories.Interfaces;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BorrowerController : Controller
    {
        readonly IBorrowerRepository borrowerRepository;
        readonly IMapper mapper;
        public BorrowerController(IBorrowerRepository borrowerRepository, IMapper mapper)
        {
            this.borrowerRepository = borrowerRepository;
            this.mapper = mapper;
        }
        
        public IActionResult Index()
        {
            var borrowers = borrowerRepository.GetAll();
            return View(borrowers);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(BorrowerVM borrowerVM)
        {
            if (ModelState.IsValid)
            {
                var borrower = mapper.Map<Borrower>(borrowerVM);
                borrowerRepository.Add(borrower);
                return RedirectToAction("Index");
            }
            return View(borrowerVM);
        }

        public IActionResult Edit(int id)
        {
            var borrower = borrowerRepository.Get(id);
            
            if (borrower is null) return View("NotFound");
            
            var borrowerVM = mapper.Map<BorrowerVM>(borrower);
            return View(borrowerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,BorrowerVM borrowerVM)
        {
            if (id != borrowerVM.Code) return View("NotFound");

            if (ModelState.IsValid)
            {
                var borrower = mapper.Map<Borrower>(borrowerVM);
                borrowerRepository.Update(borrower);
                return RedirectToAction("Index");
            }
            return View(borrowerVM);
        }

        public IActionResult Delete(int id)
        {
            var borrower = borrowerRepository.Get(id);
            if (borrower is null) return View("NotFound");
            var borrowedBooksCount = borrowerRepository.GetBooksCount(id);
            if (borrowedBooksCount == 0)
            {
                borrowerRepository.Delete(borrower);
                return Ok(borrower);
            }
            return BadRequest();
        }
    }
}

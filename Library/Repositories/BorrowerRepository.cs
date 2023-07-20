using Library.Models;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    public class BorrowerRepository : GenericRepository<Borrower>, IBorrowerRepository
    {
        public BorrowerRepository(LibraryDbContext context) : base(context) { }
        public int GetBooksCount(int borrowerId)
        {
            return context.BorrowBooks.Where(b => b.BorrowerCode == borrowerId).Count();
        }
    }
}

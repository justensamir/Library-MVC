using Library.Models;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    public class BorrowBookRepository : IBorrowBookRepository
    {
        readonly LibraryDbContext context;
        public BorrowBookRepository(LibraryDbContext context)
        {
            this.context = context;
        }
        
        public int Add(BorrowBook borrowBook)
        {
            context.BorrowBooks.Add(borrowBook);
            return context.SaveChanges();
        }

        public int Delete(BorrowBook borrowBook)
        {
            context.BorrowBooks.Remove(borrowBook);
            return context.SaveChanges();
        }

        public BorrowBook? GetBorrowedBook(int bookId, int borrowerId)
        {
            return context.BorrowBooks.FirstOrDefault(b => b.BookId == bookId && b.BorrowerCode == borrowerId);
        }

        public List<Book> GetBorrowedBooks(int borrowerId)
        {
            return context.BorrowBooks.Include(b => b.Book)
                                      .Where(b => b.BorrowerCode == borrowerId)
                                      .Select(b => b.Book)
                                      .ToList();
        }


    }
}

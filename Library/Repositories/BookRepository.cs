using Library.Models;
using Library.Repositories.Interfaces;

namespace Library.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context) { }

        public int GetBorrowersCount(int bookId)
        {
            return context.BorrowBooks.Where(b => b.BookId == bookId).Count();
        }

        public int GetBookCopiesInStock(int bookId)
        {
            int borrowedCopies = GetBorrowersCount(bookId);
            var book = Get(bookId);
            return book.Copies - borrowedCopies;
        }
    }
}

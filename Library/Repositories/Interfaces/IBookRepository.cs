using Library.Models;

namespace Library.Repositories.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        int GetBorrowersCount(int bookId);
        int GetBookCopiesInStock(int bookId);
    }
}

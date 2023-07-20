using Library.Models;

namespace Library.Repositories.Interfaces
{
    public interface IBorrowBookRepository
    {
        BorrowBook? GetBorrowedBook(int bookId, int borrowerId);
        List<Book> GetBorrowedBooks(int borrowerId);
        int Add(BorrowBook borrowBook);
        int Delete(BorrowBook borrowBook);
    }
}

using Library.Models;

namespace Library.Repositories.Interfaces
{
    public interface IBorrowerRepository : IGenericRepository<Borrower>
    {
        int GetBooksCount(int borrowerId);
    }
}

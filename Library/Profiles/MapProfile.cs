using AutoMapper;
using Library.Models;
using Library.ViewModels;

namespace Library.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Book, BookVM>();
            CreateMap<BookVM, Book>();
            
            CreateMap<Borrower, BorrowerVM>();
            CreateMap<BorrowerVM, Borrower>();

            CreateMap<BorrowBook, BorrowBookVM>();
            CreateMap<BorrowBookVM, BorrowBook>();
        }
    }
}

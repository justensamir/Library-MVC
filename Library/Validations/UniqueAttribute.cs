using Library.Models;
using Library.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Library.Validations
{
    public class UniqueCodeAttribute : ValidationAttribute
    {
        readonly LibraryDbContext context = new ();
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            int bookCode = (int)value;

            BookVM? BookFromRequest = validationContext.ObjectInstance as BookVM;

            Book? bookFromDb = context.Books.FirstOrDefault(book => book.Code == bookCode && book.Id != BookFromRequest.Id);

            if (bookFromDb is not null) return new ValidationResult("This code already exist");
            
            return ValidationResult.Success;
        }
    }
}

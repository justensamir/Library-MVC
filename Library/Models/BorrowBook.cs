using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BorrowBook
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public int BorrowerCode { get; set; }
        public Borrower? Borrower { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}

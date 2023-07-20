using Library.Validations;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Borrower
    {
        [Key]
        public int Code { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public required string Name { get; set; }
        public List<BorrowBook>? BorrowerBooks { get; set; }
    }
}

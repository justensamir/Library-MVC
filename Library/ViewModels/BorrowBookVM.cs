using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class BorrowBookVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public int BorrowerCode { get; set; }

    }
}

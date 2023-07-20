using Library.Validations;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class BorrowerVM
    {
        public int Code { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        
        public required string Name { get; set; }
    }
}

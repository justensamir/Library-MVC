﻿using Library.Validations;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class BookVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [UniqueCode]
        public int Code { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public int Copies { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace TheWall.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(2)]
        [RegularExpression("^[a-zA-Z]+$",ErrorMessage = "Letter only")]
        public string first_name{get; set;}

        [Required]
        [MinLength(2)]
        [RegularExpression("^[a-zA-Z]+$",ErrorMessage = "Letter only")]
        public string last_name{get; set;}

        [Required]
        [EmailAddress]
        public string email {get; set;}

        [Required]
        [MinLength(8)]
        public string password {get; set;}

        [Required]
        [Compare("password", ErrorMessage=" Password should match")]
        public string confirm {get;set;}
    }
}
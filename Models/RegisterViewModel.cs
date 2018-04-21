using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WeddingPlanner.Models{
    public class RegisterViewModel{
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters")]
        [MinLength(2, ErrorMessage= "First Name must have at least 2 letters")]
        public string firstname {get;set;}
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters")]
        [MinLength(2, ErrorMessage= "Last Name must have at least 2 letters")]
        public string lastname {get;set;}
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$")]
        public string email {get;set;}
        [Required]
        [MinLength(8, ErrorMessage= "Password must have at least 8 characters")]
        public string password {get;set;}
        [Compare(nameof(password),ErrorMessage= "Password Confirmation does not match Password")]
        public string confirmpassword {get;set;}
    }
}
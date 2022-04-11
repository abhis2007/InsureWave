using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UILayer.Models
{
    public class NewUser
    {
        [Required(ErrorMessage = "Seems you forgot to choose your unique userid")]
        [Display(Name ="User ka naam")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "First name please")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name please")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Your gender please")]
        public string GenderId { get; set; }
        [Required(ErrorMessage = "Email address can't be empty")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Who want you to became?")]
        public int RoleId { get; set; }

    }
}

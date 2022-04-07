using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UILayer.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Seems you forgot to provide your userid")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password can't be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Role can't be empty")]
        public int RoleId { get; set; }
    }
}

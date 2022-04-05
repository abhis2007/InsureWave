using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Users = new HashSet<User>();
        }

        public string GenderId { get; set; }
        public string GenderName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

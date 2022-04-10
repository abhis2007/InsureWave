using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UILayer.Models
{
    public class UIDataContext:DbContext
    {
        public DbSet<InsurerResponse> Insurerresponses { get; set; }
    }
}

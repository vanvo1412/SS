using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SSApp.Models;

namespace SSApp.Data
{
    public class SSAppContext : DbContext
    {
        public SSAppContext (DbContextOptions<SSAppContext> options)
            : base(options)
        {
        }

        public DbSet<SSApp.Models.Alteration> Alteration { get; set; }
    }
}

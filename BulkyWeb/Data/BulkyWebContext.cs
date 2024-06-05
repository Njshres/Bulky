using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BulkyWeb.Models;

namespace BulkyWeb.Data
{
    public class BulkyWebContext : DbContext
    {
        public BulkyWebContext (DbContextOptions<BulkyWebContext> options)
            : base(options)
        {
        }

        public DbSet<BulkyWeb.Models.Category> Categories { get; set; } = default!;
    }
}

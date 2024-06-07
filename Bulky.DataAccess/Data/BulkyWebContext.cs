using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bulky.Models;

namespace Bulky.DataAccess.Data
{
    public class BulkyWebContext : DbContext
    {
        public BulkyWebContext (DbContextOptions<BulkyWebContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DispalyOrder = 1 },
                    new Category { Id = 2, Name = "Scifi", DispalyOrder = 2 },
                        new Category { Id = 3, Name = "History", DispalyOrder = 3 }

                );
        }
    }
}

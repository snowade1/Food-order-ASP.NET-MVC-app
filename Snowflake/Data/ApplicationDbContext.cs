using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Snowflake.Models;

namespace Snowflake.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }

		public DbSet<ProductType> ProductType { get; set; }

		public DbSet<Product> Product { get; set; }
	}
}

using Microsoft.EntityFrameworkCore;
using PMD_CRUD.Models;

namespace PMD_CRUD.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext>
options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Details> Details { get; set; }
    }
}

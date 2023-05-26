using D7CRUD.NETCrud_Example.Models;
using Microsoft.EntityFrameworkCore;

namespace D7CRUD.NETCrud_Example.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Category> Category {get; set; }
    }
}
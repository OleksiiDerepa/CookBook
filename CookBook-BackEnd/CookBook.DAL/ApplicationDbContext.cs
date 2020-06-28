using CookBook.DAL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CookBook.DAL
{
    public sealed class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
           // Database.EnsureDeleted();
           // Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecipeConfiguration).Assembly);
        }
    }
}
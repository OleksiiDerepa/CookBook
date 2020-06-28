using CookBook.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.DAL.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder
                .Property(x => x.Id)
                .HasMaxLength(100);
            
            builder
                .Property(x => x.ParentId)
                .HasMaxLength(100)
                .IsRequired(false);
            
            builder
                .Property(x => x.Title)
                .HasMaxLength(150)
                .IsRequired();
            
            builder
                .Property(x => x.Description)
                .HasMaxLength(1000)
                .IsRequired();
        }
    }
}
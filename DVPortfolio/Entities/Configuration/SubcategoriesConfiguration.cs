using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class SubcategoriesConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.HasOne(c => c.MainCategory)
                .WithMany(e => e.Subcategories)
                .HasForeignKey("MainCategoryId")
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(c => c.Photos)
                .WithOne(e => e.Subcategory)
                .HasForeignKey("SubcategoryId")
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(c => c.Videos)
                .WithOne(e => e.Subcategory)
                .HasForeignKey("SubcategoryId")
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(c => c.Websites)
                .WithOne(e => e.Subcategory)
                .HasForeignKey("SubcategoryId")
                .OnDelete(DeleteBehavior.ClientCascade);


            builder.HasData
            (
                new Subcategory
                {
                    Id = 1,
                    MainCategoryId = 1,
                    Name = "Berlin",
                    ImageURL = "berlin.png",
                    Hidden = false
                }
            );
        }
    }
}

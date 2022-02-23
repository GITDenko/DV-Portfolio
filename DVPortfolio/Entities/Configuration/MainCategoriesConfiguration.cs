using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class MainCategoriesConfiguration : IEntityTypeConfiguration<MainCategory>
    {
        public void Configure(EntityTypeBuilder<MainCategory> builder)
        {
            builder.HasMany(c => c.Subcategories)
                .WithOne(e => e.MainCategory)
                .HasForeignKey("MainCategoryId");

            builder.HasMany(c => c.Photos)
                .WithOne(e => e.MainCategory)
                .HasForeignKey("MainCategoryId");

            builder.HasMany(c => c.Videos)
                .WithOne(e => e.MainCategory)
                .HasForeignKey("MainCategoryId");

            builder.HasMany(c => c.Websites)
                .WithOne(e => e.MainCategory)
                .HasForeignKey("MainCategoryId");

            builder.HasData
            (
                new MainCategory
                {
                    Id = 1,
                    Name = "Photography",
                    ImageURL = "photography.png",
                    Hidden = false,
                },

                new MainCategory
                {
                    Id = 2,
                    Name = "Videos",
                    ImageURL = "videos.png",
                    Hidden = false,
                },

                new MainCategory
                {
                    Id = 3,
                    Name = "Websites",
                    ImageURL = "websites.png",
                    Hidden = false,
                }


            );
        }
    }
}

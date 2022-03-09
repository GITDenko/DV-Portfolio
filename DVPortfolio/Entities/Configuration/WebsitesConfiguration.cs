using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class WebsitesConfiguration : IEntityTypeConfiguration<Website>
    {
        public void Configure(EntityTypeBuilder<Website> builder)
        {
            builder.HasOne(c => c.MainCategory)
                .WithMany(e => e.Websites)
                .HasForeignKey("MainCategoryId")
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(c => c.Subcategory)
               .WithMany(e => e.Websites)
               .HasForeignKey("SubcategoryId")
               .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasData
            (
                new Website
                {
                    Id = 1,
                    Name = "Google",
                    ImageURL = "google.jpg",
                    MainCategoryId = 3,
                    SubcategoryId = null,
                    ProductUrl = "https://www.google.com",
                    CreatedOn = DateTime.Now,
                    Hidden = false
                }
            );
        }
    }
}

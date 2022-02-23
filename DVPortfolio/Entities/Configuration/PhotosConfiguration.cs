using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class PhotosConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasOne(c => c.MainCategory)
                .WithMany(e => e.Photos)
                .HasForeignKey("MainCategoryId")
                .OnDelete(DeleteBehavior.ClientCascade);

             builder.HasOne(c => c.Subcategory)
                .WithMany(e => e.Photos)
                .HasForeignKey("SubcategoryId")
                .OnDelete(DeleteBehavior.ClientCascade);


            builder.HasData
            (
                new Photo
                {
                    Id = 1,
                    MainCategoryId = null,
                    SubcategoryId = 1,
                    ProductUrl = "berlin.png",
                    CreatedOn = DateTime.Now,
                    Hidden = false
                }
            );
        }
    }
}

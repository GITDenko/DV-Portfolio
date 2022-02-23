using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class VideosConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasOne(c => c.MainCategory)
                .WithMany(e => e.Videos)
                .HasForeignKey("MainCategoryId")
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(c => c.Subcategory)
               .WithMany(e => e.Videos)
               .HasForeignKey("SubcategoryId")
               .OnDelete(DeleteBehavior.ClientCascade);


            builder.HasData
            (
                new Video
                {
                    Id = 1,
                    MainCategoryId = 2,
                    SubcategoryId = null,
                    ProductUrl = "https://www.youtube.com/watch?v=izGwDsrQ1eQ",
                    CreatedOn = DateTime.Now,
                    Hidden = false
                }
            );
        }
    }
}

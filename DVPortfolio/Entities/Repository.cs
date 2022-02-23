using Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MainCategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new SubcategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new PhotosConfiguration());
            modelBuilder.ApplyConfiguration(new VideosConfiguration());
            modelBuilder.ApplyConfiguration(new WebsitesConfiguration());
        }

        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Photo> Websites { get; set; }
    }
}

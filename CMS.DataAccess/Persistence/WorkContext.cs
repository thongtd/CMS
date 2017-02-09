using System.Data.Entity;
using CMS.DataAccess.Core.Domain;

namespace CMS.DataAccess.Persistence
{
    public class WorkContext : DbContext
    {
        public WorkContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<BlogCategory> BlogCategorys { get; set; }

        public virtual DbSet<Blog> Blogs { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Gallery> Gallerys { get; set; }

        public DbSet<GalleryCategory> GalleryCategorys { get; set; }

        public virtual DbSet<TagCategory> TagCategorys { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<VideoCategory> VideoCategorys { get; set; }

        public DbSet<ProductCategory> ProductCategorys { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductSetting> ProductSettings { get; set; }

        public DbSet<SiteSetting> SiteSettingRepositories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Configurations.Add(new BlogCategoryMap());
            modelBuilder.Configurations.Add(new BlogMap());
            modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new GalleryMap());
            modelBuilder.Configurations.Add(new GalleryCategoryMap());
            modelBuilder.Configurations.Add(new TagMap());
            modelBuilder.Configurations.Add(new TagCategoryMap());
            modelBuilder.Configurations.Add(new VideoMap());
            modelBuilder.Configurations.Add(new VideoCategoryMap());
            modelBuilder.Configurations.Add(new ProductCategoryMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductSettingMap());
            modelBuilder.Configurations.Add(new SiteSettingMap());
        }
    }
}

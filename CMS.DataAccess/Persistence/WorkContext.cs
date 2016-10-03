using System.Data.Entity;
using CMS.DataAccess.Core.Domain;

namespace CMS.DataAccess.Persistence
{
    public class WorkContext : DbContext
    {
        public WorkContext()
            : base("DefaultConnection")
        { }

        public DbSet<BlogCategory> BlogCategorys { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Gallery> Gallerys { get; set; }

        public DbSet<GalleryCategory> GalleryCategorys { get; set; }

        public DbSet<TagCategory> TagCategorys { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<VideoCategory> VideoCategorys { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<WorkContext>(null);
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
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Models;
using Newtonsoft.Json;

namespace CMS.DataAccess.Core.Domain
{
    [Table("Blog")]
    public class Blog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public int BlogCategoryId { get; set; }

        public string Thumbnail { get; set; }

        public string OriginImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModeifiedDate { get; set; }

        public string SubContent { get; set; }

        public string BodyContent { get; set; }

        public string Target { get; set; }

        public int Click { get; set; }

        public Guid IdentityCode { get; set; }

        public bool PinToTop { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Keyword { get; set; }

        public string CultureCode { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public virtual BlogCategory BlogCategory { get; set; }

        public static implicit operator Blog(BlogRequest model)
        {
            if (model == null)
            {
                return null;
            }

            return new Blog
            {
                Name = model.Name,
                Slug = string.IsNullOrEmpty(model.Slug) ? model.Name.NameToSlug() : model.Slug,
                Title = string.IsNullOrEmpty(model.Title) ? model.Name : model.Title,
                BlogCategoryId = model.BlogCategoryId,
                Thumbnail = model.Thumbnail,
                OriginImage = string.IsNullOrEmpty(model.OriginImage) ? model.Thumbnail : model.OriginImage,
                CreatedDate = model.CreatedDate,
                ModeifiedDate = DateTime.UtcNow,
                SubContent = model.SubContent,
                BodyContent = model.BodyContent,
                Target = model.Target,
                Click = model.Click,
                Description = model.Description,
                Keyword = model.Keyword,
                CultureCode = model.CultureCode,
                IsActive = model.IsActive,
                IdentityCode = model.IdentityCode == Guid.Empty ? Guid.NewGuid() : model.IdentityCode,
                PinToTop = model.PinToTop
            };
        }
    }

    public class BlogMap : EntityTypeConfiguration<Blog>
    {
        public BlogMap()
        {
            ToTable("Blog");
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(255).IsRequired();
            Property(x => x.Slug).HasMaxLength(255).IsRequired();
            Property(x => x.BlogCategoryId).IsRequired();
            Property(x => x.Thumbnail).HasMaxLength(512);
            Property(x => x.OriginImage).HasMaxLength(512);
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.ModeifiedDate).IsRequired();
            Property(x => x.SubContent);
            Property(x => x.BodyContent);
            Property(x => x.Target).HasMaxLength(10);
            Property(x => x.Click);
            Property(x => x.Title).HasMaxLength(255);
            Property(x => x.Description).HasMaxLength(512);
            Property(x => x.Keyword).HasMaxLength(512);
            Property(x => x.CultureCode).HasMaxLength(50);
            Property(x => x.IsActive);
            Property(x => x.IdentityCode).IsRequired();
            Property(x => x.PinToTop).IsRequired();
        }
    }
}

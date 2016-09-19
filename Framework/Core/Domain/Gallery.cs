using System;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Core.Extension;
using Framework.Models;
using Newtonsoft.Json;
using System.Data.Entity.ModelConfiguration;

namespace Framework.Core.Domain
{
    [Table("Gallery")]
    public class Gallery
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public int GalleryCategoryId { get; set; }

        public string Thumbnail { get; set; }

        public string OriginImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModeifiedDate { get; set; }
        
        public string BodyContent { get; set; }
        
        public string GalleryType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Keyword { get; set; }

        public string CultureCode { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public virtual GalleryCategory GalleryCategory { get; set; }

        public static implicit operator Gallery(GalleryRequest model)
        {
            if (model == null)
            {
                return null;
            }

            return new Gallery
            {
                Name = model.Name,
                Slug = string.IsNullOrEmpty(model.Slug) ? model.Name.NameToSlug() : model.Slug,
                GalleryCategoryId = model.GalleryCategoryId,
                Thumbnail = model.Thumbnail,
                OriginImage = string.IsNullOrEmpty(model.OriginImage) ? model.Thumbnail : model.OriginImage,
                CreatedDate = model.CreatedDate,
                ModeifiedDate = DateTime.UtcNow,
                BodyContent = model.BodyContent,
                Title = model.Title,
                Description = model.Description,
                Keyword = model.Keyword,
                GalleryType = model.GalleryType,
                CultureCode = model.CultureCode,
                IsActive = model.IsActive
            };
        }
    }

    public class GalleryMap : EntityTypeConfiguration<Gallery>
    {
        public GalleryMap()
        {
            ToTable("Gallery");
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(255).IsRequired();
            Property(x => x.Slug).HasMaxLength(255).IsRequired();
            Property(x => x.GalleryCategoryId).IsRequired();
            Property(x => x.Thumbnail).HasMaxLength(512);
            Property(x => x.OriginImage).HasMaxLength(512);
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.ModeifiedDate).IsRequired();
            Property(x => x.BodyContent);
            Property(x => x.GalleryType).HasMaxLength(10);
            Property(x => x.Title).HasMaxLength(255);
            Property(x => x.Description).HasMaxLength(512);
            Property(x => x.Keyword).HasMaxLength(512);
            Property(x => x.CultureCode).HasMaxLength(50);
            Property(x => x.IsActive);
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Framework.Core.Extension;
using Framework.Models;
using Newtonsoft.Json;

namespace Framework.Core.Domain
{
    [Table("Video")]
    public class Video
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public int VideoCategoryId { get; set; }

        public string Thumbnail { get; set; }

        public string OriginImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModeifiedDate { get; set; }

        public string BodyContent { get; set; }

        public string VideoType { get; set; }

        public string PlayerType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Keyword { get; set; }

        public string CultureCode { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public virtual VideoCategory VideoCategory { get; set; }

        public static implicit operator Video(VideoRequest model)
        {
            if (model == null)
            {
                return null;
            }

            return new Video
            {
                Name = model.Name,
                Slug = string.IsNullOrEmpty(model.Slug) ? model.Name.NameToSlug() : model.Slug,
                VideoCategoryId = model.VideoCategoryId,
                Thumbnail = model.Thumbnail,
                OriginImage = string.IsNullOrEmpty(model.OriginImage) ? model.Thumbnail : model.OriginImage,
                CreatedDate = model.CreatedDate,
                ModeifiedDate = DateTime.UtcNow,
                BodyContent = model.BodyContent,
                Title = model.Title,
                Description = model.Description,
                Keyword = model.Keyword,
                VideoType = model.VideoType,
                PlayerType = model.PlayerType,
                CultureCode = model.CultureCode,
                IsActive = model.IsActive
            };
        }
    }

    public class VideoMap : EntityTypeConfiguration<Video>
    {
        public VideoMap()
        {
            ToTable("Video");
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(255).IsRequired();
            Property(x => x.Slug).HasMaxLength(255).IsRequired();
            Property(x => x.VideoCategoryId).IsRequired();
            Property(x => x.Thumbnail).HasMaxLength(512);
            Property(x => x.OriginImage).HasMaxLength(512);
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.ModeifiedDate).IsRequired();
            Property(x => x.BodyContent);
            Property(x => x.VideoType).HasMaxLength(10);
            Property(x => x.PlayerType).HasMaxLength(10);
            Property(x => x.Title).HasMaxLength(255);
            Property(x => x.Description).HasMaxLength(512);
            Property(x => x.Keyword).HasMaxLength(512);
            Property(x => x.CultureCode).HasMaxLength(50);
            Property(x => x.IsActive);
        }
    }
}

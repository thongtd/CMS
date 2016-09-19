﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Framework.Core.Extension;
using Framework.Models;
using Framework.Persistence;

namespace Framework.Core.Domain
{
    [Table("GalleryCategory")]
    public class GalleryCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public int ParentId { get; set; }

        public string Thumbnail { get; set; }

        public string OriginImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModeifiedDate { get; set; }

        public int Order { get; set; }

        public string Level { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Keyword { get; set; }

        public string CultureCode { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Gallery> Gallerys { get; set; }

        public static implicit operator GalleryCategory(GalleryCategoryRequest model)
        {
            if (model == null)
            {
                return null;
            }

            var parentLevel = string.Empty;
            if (model.ParentId != 0)
            {
                using (var unitOfWork = new UnitOfWork(new WorkContext()))
                {
                    var parent = unitOfWork.GalleryCategory.Get(model.ParentId);
                    if (parent != null)
                    {
                        parentLevel = parent.Level;
                    }
                }
            }

            return new GalleryCategory
            {
                Name = model.Name,
                Slug = string.IsNullOrEmpty(model.Slug) ? model.Name.NameToSlug() : model.Slug,
                ParentId = model.ParentId,
                Thumbnail = model.Thumbnail,
                OriginImage = string.IsNullOrEmpty(model.OriginImage) ? model.Thumbnail : model.OriginImage,
                CreatedDate = model.CreatedDate,
                ModeifiedDate = DateTime.UtcNow,
                Order = model.Order,
                Level = StringExtension.MakeLevel(model.Order, parentLevel),
                Title = model.Title,
                Description = model.Description,
                Keyword = model.Keyword,
                CultureCode = model.CultureCode,
                IsActive = model.IsActive
            };
        }
    }
    public class GalleryCategoryMap : EntityTypeConfiguration<GalleryCategory>
    {
        public GalleryCategoryMap()
        {
            ToTable("GalleryCategory");
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(255).IsRequired();
            Property(x => x.Slug).HasMaxLength(255).IsRequired();
            Property(x => x.ParentId).IsRequired();
            Property(x => x.Thumbnail).HasMaxLength(512);
            Property(x => x.OriginImage).HasMaxLength(512);
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.ModeifiedDate).IsRequired();
            Property(x => x.Order).IsRequired();
            Property(x => x.Level).HasMaxLength(255);
            Property(x => x.Title).HasMaxLength(255);
            Property(x => x.Description).HasMaxLength(512);
            Property(x => x.Keyword).HasMaxLength(512);
            Property(x => x.CultureCode).HasMaxLength(50);
            Property(x => x.IsActive);

            HasMany(x => x.Gallerys).WithRequired(x => x.GalleryCategory).HasForeignKey(x => x.GalleryCategoryId);
        }
    }
}

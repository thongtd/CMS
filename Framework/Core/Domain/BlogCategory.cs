using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Core.Extension;
using Framework.Models;
using Framework.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Domain
{
    [Table("BlogCategory")]
    public class BlogCategory
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

        public virtual ICollection<Blog> Blogs { get; set; }

        public static implicit operator BlogCategory(BlogCategoryRequest model)
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
                    var parent = unitOfWork.BlogCategory.Get(model.ParentId);
                    if (parent != null)
                    {
                        parentLevel = parent.Level;
                    }
                }
            }

            return new BlogCategory
            {
                Name = model.Name,
                Slug = string.IsNullOrEmpty(model.Slug) ? model.Name.NameToSlug() : model.Slug,
                ParentId = model.ParentId,
                Thumbnail = model.Thumbnail,
                OriginImage = string.IsNullOrEmpty(model.OriginImage) ? model.Thumbnail : model.OriginImage ,
                CreatedDate = model.CreatedDate,
                ModeifiedDate = DateTime.UtcNow,
                Order = model.Order,
                Level = StringExtension.MakeLevel(model.Order, parentLevel),
                Title = string.IsNullOrEmpty(model.Title) ?  model.Name : model.Title,
                Description = model.Description,
                Keyword = model.Keyword,
                CultureCode = model.CultureCode,
                IsActive = model.IsActive
            };
        }

        public static void BlogCategoryMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogCategory>(b =>
            {
                b.ToTable("BlogCategory");
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).HasMaxLength(255).IsRequired();
                b.Property(x => x.Slug).HasMaxLength(255).IsRequired();
                b.Property(x => x.ParentId).IsRequired();
                b.Property(x => x.Thumbnail).HasMaxLength(512);
                b.Property(x => x.OriginImage).HasMaxLength(512);
                b.Property(x => x.CreatedDate).IsRequired();
                b.Property(x => x.ModeifiedDate).IsRequired();
                b.Property(x => x.Order).IsRequired();
                b.Property(x => x.Level).HasMaxLength(255);
                b.Property(x => x.Title).HasMaxLength(255);
                b.Property(x => x.Description).HasMaxLength(512);
                b.Property(x => x.Keyword).HasMaxLength(512);
                b.Property(x => x.CultureCode).HasMaxLength(50);
                b.Property(x => x.IsActive);

                b.HasMany(x => x.Blogs).WithOne(x => x.BlogCategory).HasForeignKey(x => x.BlogCategoryId);
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace CMS.DataAccess.Core.Domain
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public int ProductCategoryId { get; set; }

        public string Thumbnail { get; set; }

        public string Images { get; set; }

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

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int DiscountType { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public virtual ProductCategory ProductCategory { get; set; }

        public static implicit operator Product(ProductRequest model)
        {
            if (model == null)
            {
                return null;
            }

            var images = new List<string> { model.Thumbnail };
            var jsonSerialiser = new JavaScriptSerializer();

            var json = jsonSerialiser.Serialize(images);

            return new Product
            {
                Name = model.Name,
                Slug = string.IsNullOrEmpty(model.Slug) ? model.Name.NameToSlug() : model.Slug,
                Title = string.IsNullOrEmpty(model.Title) ? model.Name : model.Title,
                ProductCategoryId = model.ProductCategoryId,
                Thumbnail = model.Thumbnail,
                Images = !model.Images.Any() ? json : model.Images,
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
                PinToTop = model.PinToTop,
                Price = model.Price,
                Discount = model.Discount,
                DiscountType = model.DiscountType
            };
        }
    }

    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable("Product");
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(255).IsRequired();
            Property(x => x.Slug).HasMaxLength(255).IsRequired();
            Property(x => x.ProductCategoryId).IsRequired();
            Property(x => x.Thumbnail).HasMaxLength(512);
            Property(x => x.Images).HasMaxLength(Int32.MaxValue);
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
            Property(x => x.Price).IsRequired();
            Property(x => x.Discount);
            Property(x => x.DiscountType);
        }
    }
}

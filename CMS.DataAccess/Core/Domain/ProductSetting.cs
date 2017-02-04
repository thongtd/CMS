using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CMS.DataAccess.Models;

namespace CMS.DataAccess.Core.Domain
{
    [Table("ProductSetting")]
    public class ProductSetting
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public static implicit operator ProductSetting(ProductSettingRequest model)
        {
            if (model == null)
            {
                return null;
            }

            return new ProductSetting
            {
                Name = model.Name,
                Type = model.Type
            };
        }
    }

    public class ProductSettingMap : EntityTypeConfiguration<ProductSetting>
    {
        public ProductSettingMap()
        {
            ToTable("ProductSetting");
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(255).IsRequired();
            Property(x => x.Type).HasMaxLength(255).IsRequired();
        }
    }
}

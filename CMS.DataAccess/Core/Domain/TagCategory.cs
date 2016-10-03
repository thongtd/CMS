using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using CMS.DataAccess.Models;

namespace CMS.DataAccess.Core.Domain
{
    public class TagCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MetaTag { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public static implicit operator TagCategory(TagCategoryRequest model)
        {
            if (model == null)
            {
                return null;
            }

            return new TagCategory
            {
                Name = model.Name,
                MetaTag = model.MetaTag,
            };
        }
    }

    public class TagCategoryMap : EntityTypeConfiguration<TagCategory>
    {
        public TagCategoryMap()
        {
            ToTable("TagCategory");
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(255).IsRequired();
            Property(x => x.MetaTag).HasMaxLength(255).IsRequired();

            HasMany(x => x.Tags).WithRequired(x => x.TagCategory).HasForeignKey(x => x.TagCategoryId);
        }
    }
}

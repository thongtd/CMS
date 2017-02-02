using System;
using System.Data.Entity.ModelConfiguration;
using CMS.DataAccess.Models;
using Newtonsoft.Json;

namespace CMS.DataAccess.Core.Domain
{
    public class Tag
    {
        public int Id { get; set; }

        public string ObjectName { get; set; }

        public Guid ObjectIdentityId { get; set; }

        public int TagCategoryId { get; set; }

        [JsonIgnore]
        public virtual TagCategory TagCategory { get; set; }

        public static implicit operator Tag(TagRequest model)
        {
            if (model == null)
            {
                return null;
            }

            return new Tag
            {
                ObjectName = model.ObjectName,
                ObjectIdentityId = model.ObjectIdentityId,
                TagCategoryId = model.TagCategory,
            };
        }
    }

    public class TagMap : EntityTypeConfiguration<Tag>
    {
        public TagMap()
        {
            ToTable("Tag");
            HasKey(x => x.Id);
            Property(x => x.ObjectName).HasMaxLength(50).IsRequired();
            Property(x => x.ObjectIdentityId).IsRequired();
            Property(x => x.TagCategoryId).IsRequired();
        }
    }
}

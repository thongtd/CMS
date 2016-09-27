using Framework.Models;
using Newtonsoft.Json;

namespace Framework.Core.Domain
{
    public class Tag
    {
        public int Id { get; set; }

        public string ObjectName { get; set; }

        public string ObjectProperty { get; set; }

        public string ObjectValue { get; set; }

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
                ObjectProperty = model.ObjectProperty,
                ObjectValue = model.ObjectValue,
                TagCategoryId = model.TagCategory,
            };
        }
    }

    //public class TagMap : EntityTypeConfiguration<Tag>
    //{
    //    public TagMap()
    //    {
    //        ToTable("Tag");
    //        HasKey(x => x.Id);
    //        Property(x => x.ObjectName).HasMaxLength(50).IsRequired();
    //        Property(x => x.ObjectProperty).HasMaxLength(50).IsRequired();
    //        Property(x => x.ObjectValue).HasMaxLength(50).IsRequired();
    //        Property(x => x.TagCategoryId).IsRequired();
    //    }
    //}
}

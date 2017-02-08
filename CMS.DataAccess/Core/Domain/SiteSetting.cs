using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CMS.DataAccess.Core.Domain
{
    [Table("SiteSetting")]
    public class SiteSetting
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Group { get; set; }
    }

    public class SiteSettingMap : EntityTypeConfiguration<SiteSetting>
    {
        public SiteSettingMap()
        {
            ToTable("SiteSetting");
            HasKey(x => x.Id);
            Property(x => x.Key).HasMaxLength(255).IsRequired();
            Property(x => x.Value).IsRequired();
            Property(x => x.Group).HasMaxLength(255).IsRequired();
        }
    }
}

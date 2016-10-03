using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CMS.DataAccess.Core.Domain
{
    [Table("Contact")]
    public class Contact
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string AddressLine { get; set; }

        public string BodyContent { get; set; }

        public DateTime CreatedDate { get; set; }

        public string IsReaded { get; set; }
    }

    public class ContactMap : EntityTypeConfiguration<Contact>
    {
        public ContactMap()
        {
            ToTable("Contact");
            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(255).IsRequired();
            Property(x => x.EmailAddress).HasMaxLength(255).IsRequired();
            Property(x => x.PhoneNumber).IsRequired();
            Property(x => x.AddressLine).HasMaxLength(512);
            Property(x => x.BodyContent).HasMaxLength(512).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.IsReaded).IsRequired();
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CMS.DataAccess.Models;

namespace CMS.DataAccess.Core.Domain
{
    [Table("ExamCategory")]
    public class ExamCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public static implicit operator ExamCategory(ExamCategoryRequest model)
        {
            if (model == null)
            {
                return null;
            }

            return new ExamCategory
            {
                Name = model.Name,
                Description = model.Description
            };
        }
    }

    public class ExamCategoryMap : EntityTypeConfiguration<ExamCategory>
    {
        public ExamCategoryMap()
        {
            ToTable("ExamCategory");
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(255).IsRequired();
            Property(x => x.Description);
        }
    }
}

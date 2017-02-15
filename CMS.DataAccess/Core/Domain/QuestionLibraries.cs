using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using CMS.DataAccess.Models;
using Newtonsoft.Json;

namespace CMS.DataAccess.Core.Domain
{
    [Table("QuestionLibraries")]
    public class QuestionLibraries
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Question { get; set; }

        public string Answers { get; set; }

        public bool IsActive { get; set; }

        public int ExamCategoryId { get; set; }

        [JsonIgnore]
        public virtual ExamCategory ExamCategory { get; set; }

        public static implicit operator QuestionLibraries(QuestionLibrariesRequest model)
        {
            if (model == null)
            {
                return null;
            }

            return new QuestionLibraries
            {
                Answers = (model.Answers !=null && model.Answers.Any()) ? JsonConvert.SerializeObject(model.Answers) : string.Empty,
                Question = model.Question,
                Title = model.Title,
                IsActive = model.IsActive,
                ExamCategoryId = model.ExamCategoryId
            };
        }
    }

    public class QuestionLibrariesMap : EntityTypeConfiguration<QuestionLibraries>
    {
        public QuestionLibrariesMap()
        {
            ToTable("QuestionLibraries");
            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(512).IsRequired();
            Property(x => x.Question).HasMaxLength(512).IsRequired();
            Property(x => x.Answers).IsRequired();
            Property(x => x.IsActive).IsRequired();
            Property(x => x.ExamCategoryId).IsRequired();
        }
    }
}

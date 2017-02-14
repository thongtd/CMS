using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CMS.DataAccess.Models;
using Newtonsoft.Json;

namespace CMS.DataAccess.Core.Domain
{
    [Table("Exam")]
    public class Exam
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public int ExamCategoryId { get; set; }

        public string Thumbnail { get; set; }

        public string Description { get; set; }

        public int NumberOfQuestions { get; set; }

        public int TimeToTest { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModeifiedDate { get; set; }

        public int PassNumberOfAnswers { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public virtual ExamCategory ExamCategory { get; set; }

        public static implicit operator Exam(ExamRequest model)
        {
            if (model == null)
            {
                return null;
            }

            return new Exam
            {
                Name = model.Name,
                Slug = model.Slug,
                ExamCategoryId = model.ExamCategoryId,
                Thumbnail = model.Thumbnail,
                Description = model.Description,
                NumberOfQuestions = model.NumberOfQuestions,
                TimeToTest = model.TimeToTest,
                CreatedDate = model.CreatedDate,
                ModeifiedDate = model.ModeifiedDate,
                PassNumberOfAnswers = model.PassNumberOfAnswers,
                IsActive = model.IsActive
            };
        }
    }

    public class ExamMap : EntityTypeConfiguration<Exam>
    {
        public ExamMap()
        {
            ToTable("Exam");
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(255).IsRequired();
            Property(x => x.Slug).HasMaxLength(255).IsRequired();
            Property(x => x.ExamCategoryId).IsRequired();
            Property(x => x.Thumbnail).HasMaxLength(512);
            Property(x => x.Description).HasMaxLength(512);
            Property(x => x.NumberOfQuestions).IsRequired();
            Property(x => x.TimeToTest).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.ModeifiedDate).IsRequired();
            Property(x => x.PassNumberOfAnswers).IsRequired();
            Property(x => x.IsActive).IsRequired();
        }
    }
}

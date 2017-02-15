using System;

namespace CMS.DataAccess.Models
{
    public class ExamRequest
    {
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
    }
}

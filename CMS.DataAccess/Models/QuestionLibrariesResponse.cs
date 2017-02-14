using System.Collections.Generic;

namespace CMS.DataAccess.Models
{
    public class QuestionLibrariesResponse
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string Title { get; set; }

        public IList<Answer> Answers { get; set; }

        public bool IsActive { get; set; }
    }
}

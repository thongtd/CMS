using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CMS.DataAccess.Core.Extension;

namespace CMS.DataAccess.Models
{
    public class QuestionLibrariesRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string Question { get; set; }

        public string Title { get; set; }

        public IList<Answer> Answers { get; set; }

        public bool IsActive { get; set; }
    }
}

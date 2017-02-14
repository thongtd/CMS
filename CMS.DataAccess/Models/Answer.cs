using System.ComponentModel.DataAnnotations;
using CMS.DataAccess.Core.Extension;

namespace CMS.DataAccess.Models
{
    public class Answer
    {
        [Required(ErrorMessage = Constants.Validation.Required)]
        public string AnswerContent { get; set; }

        public bool IsCorrect { get; set; }

        public string WhyTrue { get; set; }
    }
}

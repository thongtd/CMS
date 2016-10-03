using System.ComponentModel.DataAnnotations;
using CMS.DataAccess.Core.Extension;

namespace CMS.DataAccess.Models
{
    public class TagCategoryRequest
    {
        [Required(ErrorMessage = Constants.Validation.Required)]
        [StringLength(50, ErrorMessage = Constants.Validation.MaxLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        [StringLength(50, ErrorMessage = Constants.Validation.MaxLength)]
        public string MetaTag { get; set; }
    }
}

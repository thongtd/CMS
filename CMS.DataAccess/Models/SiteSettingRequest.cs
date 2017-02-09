using System.ComponentModel.DataAnnotations;
using CMS.DataAccess.Core.Extension;

namespace CMS.DataAccess.Models
{
    public class SiteSettingRequest
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = Constants.Validation.Required)]
        [StringLength(255, ErrorMessage = Constants.Validation.MaxLength)]
        public string Key { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string Value { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        [StringLength(255, ErrorMessage = Constants.Validation.MaxLength)]
        public string Group { get; set; }
    }
}
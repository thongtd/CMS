using System.ComponentModel.DataAnnotations;
using CMS.DataAccess.Core.Extension;

namespace CMS.DataAccess.Models
{
    public class ProductSettingRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string Type { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string Name { get; set; }
    }
}

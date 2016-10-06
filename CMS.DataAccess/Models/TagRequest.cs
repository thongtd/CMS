using System;
using System.ComponentModel.DataAnnotations;
using CMS.DataAccess.Core.Extension;

namespace CMS.DataAccess.Models
{
    public class TagRequest
    {
        [Required(ErrorMessage = Constants.Validation.Required)]
        public string ObjectName { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string ObjectProperty { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public Guid ObjectIdentityId { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public int TagCategory { get; set; }
    }
}

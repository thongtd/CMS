using System.ComponentModel.DataAnnotations;
using CMS.DataAccess.Core.Extension;

namespace Dashboard.Models
{
    public class LoginModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = Constants.Validation.Required)]
        [EmailAddress(ErrorMessage = Constants.Validation.Email)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = Constants.Validation.Required)]
        [StringLength(50, ErrorMessage = Constants.Validation.MinLength, MinimumLength = 6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

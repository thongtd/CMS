using System;
using System.ComponentModel.DataAnnotations;
using Framework.Core.Extension;

namespace Framework.Models
{
    public class ContactRequest
    {
        [Required(ErrorMessage = Constants.Validation.Required)]
        [StringLength(255, ErrorMessage = Constants.Validation.MaxLength, MinimumLength = 30)]
        public string Title { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        [EmailAddress(ErrorMessage = Constants.Validation.Email)]
        public string EmailAddress { get; set; }

        [DisplayFormat(DataFormatString = Constants.Validation.PhoneNumberFormat)]
        public string PhoneNumber { get; set; }

        public string AddressLine { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string BodyContent { get; set; }

        public DateTime CreatedDate { get; set; }

        public string IsReaded { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CMS.DataAccess.Core.Extension;

namespace CMS.DataAccess.Models
{
    public class BlogRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        [StringLength(255, ErrorMessage = Constants.Validation.MaxLength)]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public int BlogCategoryId { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string Thumbnail { get; set; }

        public string OriginImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModeifiedDate { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string SubContent { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = Constants.Validation.Required)]
        public string BodyContent { get; set; }

        public string Target { get; set; }

        public int Click { get; set; }

        public Guid IdentityCode { get; set; }

        public bool PinToTop { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Keyword { get; set; }

        public string CultureCode { get; set; }

        public bool IsActive { get; set; }
    }
}

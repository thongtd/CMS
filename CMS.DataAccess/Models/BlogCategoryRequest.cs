using System;
using System.ComponentModel.DataAnnotations;
using CMS.DataAccess.Core.Extension;

namespace CMS.DataAccess.Models
{
    public class BlogCategoryRequest
    {
        [Required(ErrorMessage = Constants.Validation.Required)]
        [StringLength(255, ErrorMessage = Constants.Validation.MaxLength)]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public int ParentId { get; set; }

        public string Thumbnail { get; set; }

        public string OriginImage { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public DateTime? ModeifiedDate { get; set; }

        public int Order { get; set; }

        public string Level { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public string Keyword { get; set; }

        public string CultureCode { get; set; }

        public bool IsActive { get; set; }
    }
}

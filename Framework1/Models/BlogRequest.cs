﻿using System;
using System.ComponentModel.DataAnnotations;
using Framework.Core.Extension;

namespace Framework.Models
{
    public class BlogRequest
    {
        [Required(ErrorMessage = Constants.Validation.Required)]
        [StringLength(255, ErrorMessage = Constants.Validation.MaxLength, MinimumLength = 30)]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public int BlogCategoryId { get; set; }

        public string Thumbnail { get; set; }

        public string OriginImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModeifiedDate { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string SubContent { get; set; }

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

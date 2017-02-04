using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;

namespace CMS.DataAccess.Models
{
    public class ProductRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        [StringLength(255, ErrorMessage = Constants.Validation.MaxLength)]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public int ProductCategoryId { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string Thumbnail { get; set; }

        public IList<string> Images { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModeifiedDate { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string SubContent { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = Constants.Validation.Required)]
        public string BodyContent { get; set; }

        public string Target { get; set; }

        public int Click { get; set; }

        public string TagClouds { get; set; }   //Separate by ,

        public Guid IdentityCode { get; set; }

        public bool PinToTop { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Keyword { get; set; }

        public string CultureCode { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public bool DiscountIsPercent { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public decimal NumberOfProduct { get; set; }

        public decimal SellingOfProduct { get; set; }

        public IList<string> Color { get; set; }

        public IList<string> Size { get; set; }

        public string Unit { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}

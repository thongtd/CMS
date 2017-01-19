﻿using System;
using System.Collections.Generic;
using CMS.DataAccess.Core.Domain;

namespace CMS.Dashboard.ViewModels
{
    public class EditProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public int ProductCategoryId { get; set; }

        public string Thumbnail { get; set; }

        public string Images { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModeifiedDate { get; set; }

        public string SubContent { get; set; }

        public string BodyContent { get; set; }

        public string Target { get; set; }

        public int Click { get; set; }

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

        public IEnumerable<Tag> Tags { get; set; }
    }
}
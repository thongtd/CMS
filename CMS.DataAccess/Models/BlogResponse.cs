using System;
using System.Collections;
using CMS.DataAccess.Core.Domain;
using System.Collections.Generic;

namespace CMS.DataAccess.Models
{
    public class BlogResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public int BlogCategoryId { get; set; }

        public string Thumbnail { get; set; }

        public string OriginImage { get; set; }

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

        public bool IsActive { get; set; }
        
        public BlogCategory BlogCategory { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}

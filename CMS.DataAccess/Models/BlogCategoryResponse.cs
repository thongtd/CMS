﻿using System;
using System.Collections.Generic;
using CMS.DataAccess.Core.Domain;

namespace CMS.DataAccess.Models
{
    public class BlogCategoryResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

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
        
        public IList<Blog> Blogs { get; set; }
    }
}

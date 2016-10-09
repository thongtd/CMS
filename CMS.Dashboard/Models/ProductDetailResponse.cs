using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.DataAccess.Models;

namespace CMS.Dashboard.Models
{
    public class ProductDetailResponse
    {
        public ProductResponse ProductResponse { get; set; }

        public IList<ProductResponse> RelatedProducts { get; set; }
    }
}
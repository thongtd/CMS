using System.Collections.Generic;
using CMS.DataAccess.Models;

namespace CMS.Dashboard.Models
{
    public class ProductDetailModel
    {
        public ProductResponse ProductResponse { get; set; }

        public IList<ProductResponse> RelatedProducts { get; set; }
    }
}
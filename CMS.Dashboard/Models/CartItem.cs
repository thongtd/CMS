using CMS.DataAccess.Core.Domain;

namespace CMS.Dashboard.Models
{
    public class CartItem
    {
        public Product Product { get; set; }

        public int Quality { get; set; }
    }
}
using System;

namespace CMS.Dashboard.ViewModels
{
    public class ProductPagingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ProductCategoryName { get; set; }

        public bool IsActive { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;

namespace CMS.Dashboard.Code
{
    public static class SelectListUnitities
    {
        public static List<SelectListItem> BlogCategorys()
        {
            var blogCategoryRepository = new BlogCategoryRepository(new WorkContext());
            var blogCategoryTrees = blogCategoryRepository.BlogCategoryTree().ToList();
            var result = new List<SelectListItem>();

            if (blogCategoryTrees.Any())
            {
                foreach (var item in blogCategoryTrees)
                {
                    result.Add(new SelectListItem { Text = item.Text, Value = item.Value });
                }
            }
            return result;
        }

        public static List<SelectListItem> ProductCategorys()
        {
            var productCategoryRepository = new ProductCategoryRepository(new WorkContext());
            var productCategoryTrees = productCategoryRepository.ProductCategoryTree().ToList();
            var result = new List<SelectListItem>();

            if (productCategoryTrees.Any())
            {
                foreach (var item in productCategoryTrees)
                {
                    result.Add(new SelectListItem { Text = item.Text, Value = item.Value });
                }
            }
            return result;
        }

        public static List<SelectListItem> ProductSettings()
        {
            var result = new List<SelectListItem>
            {
                new SelectListItem {Text = "Chọn loại cấu hình", Value = ""},
                new SelectListItem {Text = "Mầu sản phẩm", Value = "COLOR"},
                new SelectListItem {Text = "Kích thước sản phẩm", Value = "SIZE"},
                new SelectListItem {Text = "Đơn vị sản phẩm", Value = "UNIT"}
            };
            return result;
        }
    }
}

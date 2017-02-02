using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;

namespace CMS.Dashboard.Code
{
    public static class Unitities
    {
        public static List<SelectListItem> BlogCategoryTree()
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

        public static List<SelectListItem> ProductCategoryTree()
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
    }
}

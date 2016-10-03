using System.Collections.Generic;
using System.Linq;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dashboard.Code
{
    public static class Unititys
    {
        public static List<SelectListItem> BlogCategoryTree()
        {
            var blogCategoryRepository = new BlogCategoryRepository(new WorkContext());
            var blogCategoryTrees = blogCategoryRepository.BlogCategoryTree().ToList();
            var result = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "[Chọn nhóm tin]",
                    Value = "0"
                }
            };


            if (blogCategoryTrees.Any())
            {
                foreach (var item in blogCategoryTrees)
                {
                    result.Add(new SelectListItem { Text = item.Text, Value = item.Value });
                }
            }
            return result;
        }
    }
}

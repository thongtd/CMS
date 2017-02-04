using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;

namespace CMS.Dashboard.Code
{
    public static class Unitities
    {
        public static IList<SelectListItem> BlogCategorys()
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

        public static IList<SelectListItem> ProductCategorys()
        {
            var blogCategorys = new List<SelectedList> { new SelectedList { Value = "", Text = "[Chọn nhóm sản phẩm]" } };

            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                var predicate = PredicateBuilder.Create<ProductCategory>(s => s.IsActive);

                var records = unitOfWork.ProductCategory.Find(predicate).ToList();

                if (records.Any())
                {
                    for (int i = 0; i < records.Count(); i++)
                    {
                        int len = records[i].Level.Length;
                        if (len == 5)
                        {
                            blogCategorys.Add(new SelectedList
                            {
                                Value = records[i].Id.ToString(),
                                Text = records[i].Name
                            });
                        }
                        else
                        {
                            string strTemp = "";
                            while (len > 5 && len % 5 == 0)
                            {
                                strTemp += "_____";
                                len = len - 5;
                            }

                            blogCategorys.Add(new SelectedList
                            {
                                Value = records[i].Id.ToString(),
                                Text = strTemp + records[i].Name
                            });
                        }
                    }
                }
            }

            var result = new List<SelectListItem>();

            if (blogCategorys.Any())
            {
                foreach (var item in blogCategorys)
                {
                    result.Add(new SelectListItem { Text = item.Text, Value = item.Value });
                }
            }
            return result;
        }

        public static IList<SelectListItem> ProductSettings()
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

        public static IList<ProductSetting> GetProductSetting(string type)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var predicate = PredicateBuilder.Create<ProductSetting>(s => s.Type.Equals(type));

                return uow.ProductSetting.Find(predicate).ToList();
            }
        }

        public static IList<SelectListItem> ProductUnit()
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var predicate = PredicateBuilder.Create<ProductSetting>(s => s.Type.Equals(Constants.ObjectName.Unit));

                var units = uow.ProductSetting.Find(predicate).ToList();
                var result = new List<SelectListItem> {new SelectListItem { Value = "", Text = "[Chọn đơn vị]" }};

                if (units.Any())
                {
                    foreach (var item in units)
                    {
                        result.Add(new SelectListItem { Text = item.Name, Value = item.Name });
                    }
                }

                return result;
            }
        }
    }
}

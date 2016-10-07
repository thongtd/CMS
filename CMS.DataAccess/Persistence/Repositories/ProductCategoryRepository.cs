using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using MvcConnerstore.Collections;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(WorkContext context) : base(context)
        {
        }

        public ProductCategory ConvertToModel(ref ProductCategory productCategory, ProductCategoryRequest model)
        {
            var parentLevel = string.Empty;
            if (model.ParentId != 0)
            {
                using (var unitOfWork = new UnitOfWork(new WorkContext()))
                {
                    var parent = unitOfWork.BlogCategory.Get(model.ParentId);
                    if (parent != null)
                    {
                        parentLevel = parent.Level;
                    }
                }
            }
            productCategory.Name = model.Name;
            productCategory.Slug = string.IsNullOrEmpty(model.Slug) ? model.Name.NameToSlug() : model.Slug;
            productCategory.Order = model.Order;
            productCategory.CultureCode = model.CultureCode;
            productCategory.Description = model.Description;
            productCategory.IsActive = model.IsActive;
            productCategory.Keyword = model.Keyword;
            productCategory.OriginImage = string.IsNullOrEmpty(model.OriginImage) ? model.Thumbnail : model.OriginImage;
            productCategory.Thumbnail = model.Thumbnail;
            productCategory.ParentId = model.ParentId;
            productCategory.Level = StringExtension.MakeLevel(model.Order, parentLevel);
            productCategory.Title = string.IsNullOrEmpty(model.Title) ? model.Name : model.Title;
            productCategory.Level = StringExtension.MakeLevel(model.Order, parentLevel);

            return productCategory;
        }

        public IPagedList<ProductCategory> Paging(int pageIndex, int pageSize, out int totalRecord,
            Expression<Func<ProductCategory, bool>> predicate)
        {
            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                var records = unitOfWork.ProductCategory.Find(predicate).ToList();

                if (records.Any())
                {
                    totalRecord = records.Count();
                    records = records.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    return new PagedList<ProductCategory>(records, totalRecord);
                }
            }

            totalRecord = 0;
            return new PagedList<ProductCategory>(null, 0);
        }

        public IEnumerable<ProductCategory> GetByTop(int top, Expression<Func<ProductCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectedList> BlogCategoryTree()
        {
            var blogCategorys = new List<SelectedList> { new SelectedList { Value = "0", Text = "[Danh mục gốc]" } };

            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                var predicate = PredicateBuilder.Create<ProductCategory>(s => s.IsActive);

                var records = unitOfWork.ProductCategory.Find(predicate).ToList();

                if (records.Any())
                {
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
            }
            return blogCategorys;
        }
    }
}
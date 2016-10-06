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
    public class BlogCategoryRepository : BaseRepository<BlogCategory>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(WorkContext context) : base(context)
        {
        }

        public BlogCategory ConvertToModel(ref BlogCategory blogCategory, BlogCategoryRequest model)
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
            blogCategory.Name = model.Name;
            blogCategory.Slug = string.IsNullOrEmpty(model.Slug) ? model.Name.NameToSlug() : model.Slug;
            blogCategory.Order = model.Order;
            blogCategory.CultureCode = model.CultureCode;
            blogCategory.Description = model.Description;
            blogCategory.IsActive = model.IsActive;
            blogCategory.Keyword = model.Keyword;
            blogCategory.OriginImage = string.IsNullOrEmpty(model.OriginImage) ? model.Thumbnail : model.OriginImage;
            blogCategory.Thumbnail = model.Thumbnail;
            blogCategory.ParentId = model.ParentId;
            blogCategory.Level = StringExtension.MakeLevel(model.Order, parentLevel);
            blogCategory.Title = string.IsNullOrEmpty(model.Title) ? model.Name : model.Title;
            blogCategory.Level = StringExtension.MakeLevel(model.Order, parentLevel);

            return blogCategory;
        }

        public IPagedList<BlogCategory> Paging(int pageIndex, int pageSize, out int totalRecord,
            Expression<Func<BlogCategory, bool>> predicate)
        {
            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                var records = unitOfWork.BlogCategory.Find(predicate).ToList();

                if (records.Any())
                {
                    totalRecord = records.Count();
                    records = records.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    return new PagedList<BlogCategory>(records, totalRecord);
                }
            }

            totalRecord = 0;
            return new PagedList<BlogCategory>(null, 0);
        }

        public IEnumerable<BlogCategory> GetByTop(int top, Expression<Func<BlogCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectedList> BlogCategoryTree()
        {
            var blogCategorys = new List<SelectedList> { new SelectedList { Value = "0", Text = "[Danh mục gốc]" } };

            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                var predicate = PredicateBuilder.Create<BlogCategory>(s => s.IsActive);

                var records = unitOfWork.BlogCategory.Find(predicate).ToList();

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
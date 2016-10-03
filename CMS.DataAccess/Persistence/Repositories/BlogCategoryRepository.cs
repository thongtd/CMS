using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Core.Repositories;
using MvcConnerstore.Collections;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class BlogCategoryRepository : BaseRepository<BlogCategory>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(WorkContext context) : base(context)
        {
        }

        public IPagedList<BlogCategory> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<BlogCategory, bool>> predicate)
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
            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                var predicate = PredicateBuilder.Create<BlogCategory>(s => s.IsActive);

                var records = unitOfWork.BlogCategory.Find(predicate).ToList();

                if (records.Any())
                {
                    var blogCategorys = new List<SelectedList> { new SelectedList { Value = "0", Text = "[Danh mục gốc]" } };

                    if (records.Any())
                    {
                        for (int i = 0; i < records.Count(); i++)
                        {
                            int len = records[i].Level.Length;
                            if (len == 5)
                            {
                                blogCategorys.Add(new SelectedList { Value = records[i].Id.ToString(), Text = records[i].Name });
                            }
                            else
                            {
                                string strTemp = "";
                                while (len > 5 && len % 5 == 0)
                                {
                                    strTemp += "_____";
                                    len = len - 5;
                                }

                                blogCategorys.Add(new SelectedList { Value = records[i].Id.ToString(), Text = strTemp + records[i].Name });
                            }
                        }
                    }
                    return blogCategorys;
                }
            }
            return new List<SelectedList>();
        }
    }
}
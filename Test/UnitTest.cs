using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;

namespace Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var blogCategoryRepository = new BlogCategoryRepository(new WorkContext());

            int total = 0;
            var blogCategorys = blogCategoryRepository.Paging(1, 50, out total, null);
        }

        [TestMethod]
        public void AddBlogCategory()
        {
            var blogCategoryRequest = new BlogCategoryRequest
            {
                Thumbnail = "http://i.ebayimg.com/images/g/oUMAAOSwu1VW3iEj/s-l300.jpg",
                Name = "Name",
                Description = "",
                Level = "00001",
                Order = 1,
                OriginImage = "",
                CultureCode = "vn-VI",
                IsActive = true,
                CreatedDate = DateTime.Now,
                Keyword = "",
                ModeifiedDate = DateTime.Now,
                ParentId = 0
            };

            var blogCategory = (BlogCategory)blogCategoryRequest;

            //var blogCategoryRepository = new BlogCategoryRepository(new WorkContext());
            //blogCategoryRepository.Add(blogCategory);
            //blogCategoryRepository.SaveChange();

            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                for (int i = 0; i < 100; i++)
                {
                    unitOfWork.BlogCategory.Add(blogCategory);
                    unitOfWork.Complete();
                }
            }
        }

        [TestMethod]
        public void UpdateBlogCategory()
        {
            IBlogCategoryRepository blogCategoryRepository = new BlogCategoryRepository(new WorkContext());

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var blogCategoryRequest = new BlogCategoryRequest
                {
                    Thumbnail = "http://i.ebayimg.com/images/g/oUMAAOSwu1VW3iEj/s-l300.jpg",
                    Name = "Name ````11111111111111",
                    Description = "",
                    Level = "00001",
                    Order = 1,
                    OriginImage = "",
                    CultureCode = "vn-VI",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    Keyword = "",
                    ModeifiedDate = DateTime.Now,
                    ParentId = 0
                };

                var category = uow.BlogCategory.Get(415);
                
                category.ModeifiedDate = DateTime.UtcNow;

                blogCategoryRepository.ConvertToModel(ref category, blogCategoryRequest);
                uow.Complete();
            }
        }

        [TestMethod]
        public void BlogForCategory()
        {
            //var blog = WorkContext.Blogs.Include(s=>s.BlogCategory);
        }
    }
}

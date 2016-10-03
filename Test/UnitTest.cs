using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
                Thumbnail = "",
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

            var blogCategoryRepository = new BlogCategoryRepository(new WorkContext());
            blogCategoryRepository.Add(blogCategory);

            //using (var unitOfWork = new UnitOfWork(new WorkContext()))
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        unitOfWork.BlogCategory.Add(blogCategory);
            //    }
            //    unitOfWork.Complete();
            //}
        }
    }
}

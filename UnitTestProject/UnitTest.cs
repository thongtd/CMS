using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Framework.Persistence;
using Framework.Core.Domain;
using Framework.Core.Extension;
using Framework.Core.Linqkit;
using Framework.Models;
using Framework.Persistence.Repositories;

namespace UnitTestProject
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void AddStudent()
        {
            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                var student = new Student
                {
                    CreatedDate = DateTime.Now,
                    DateOfBirth = DateTime.Now,
                    FirstName = "Thong",
                    LastName = "Tran"
                };

                unitOfWork.Student.Add(student);
                unitOfWork.Complete();
            }
        }

        [Test]
        public void AddBlogCategory()
        {
            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                for (int i = 0; i < 1000; i++)
                {
                    var blogCategoryRequest = new BlogCategoryRequest
                    {
                        CreatedDate = DateTime.UtcNow,
                        CultureCode = Constants.CultureCode.Vietnamese,
                        Description = "Description" + i,
                        IsActive = true,
                        Keyword = "Keyword" + i,
                        Order = i,
                        Name = "Name xxx" + i,
                        ModeifiedDate = DateTime.UtcNow,
                        OriginImage = "OriginImage" + i,
                        ParentId = 0,
                        Thumbnail = "Thumbnail" + i
                    };

                    BlogCategory blogCategory = blogCategoryRequest;

                    unitOfWork.BlogCategory.Add(blogCategory);
                    unitOfWork.Complete();
                }
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void GetBlogCategory()
        {
            using (var uniOfWork = new UnitOfWork(new WorkContext()))
            {
                int t = 1;
                var predicate = PredicateBuilder.Create<BlogCategory>(x => x.IsActive == true);
                if (t == 1)
                {
                    predicate.And(x => x.Level.Equals("00001"));
                }

                //var blogCategory = uniOfWork.BlogCategory.Find(predicate).ToList();
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        public void BlogCategoryPaging(int pageIndex)
        {
            var blogCategoryRepo = new BlogCategoryRepository(new WorkContext());
            var totalRecords = 0;

            var predicate = PredicateBuilder.Create<BlogCategory>(x => x.IsActive);
            predicate.And(x => x.Order > 0);

            var orderBy = PredicateBuilder.True<BlogCategory>();
            orderBy.And(x => x.Order == 1);

            var results = blogCategoryRepo.Paging(pageIndex, 10, out totalRecords, predicate).ToList();

            Debug.WriteLine("------------------------pageIndex: " + pageIndex);
            foreach (var item in results)
            {
                Debug.WriteLine("Id: " + item.Id);
            }

            Assert.AreEqual(10, results.Count());
        }
    }
}

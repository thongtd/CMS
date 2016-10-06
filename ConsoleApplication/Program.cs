using System;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //var blogCategory = new BlogCategoryRequest
            //{
            //    Thumbnail = "",
            //    Name = "Name",
            //    Description = "",
            //    Level = "00001",
            //    Order = 1,
            //    OriginImage = "",
            //    CultureCode = "vn-VI",
            //    IsActive = true,
            //    CreatedDate = DateTime.Now,
            //    Keyword = "",
            //    ModeifiedDate = DateTime.Now,
            //    ParentId = 0
            //};

            //using (var unitOfWork = new UnitOfWork(new WorkContext()))
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        unitOfWork.BlogCategory.Add(blogCategory);
            //    }
            //    unitOfWork.Complete();
            //}

            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                unitOfWork.Tag.GetAll();
                unitOfWork.Complete();
            }
        }
    }
}

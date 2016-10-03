using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}

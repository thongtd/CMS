using System.Data.Entity;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Repositories;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class ExamCategoryRepository : BaseRepository<ExamCategory>, IExamCategoryRepository
    {
        public ExamCategoryRepository(DbContext context) : base(context)
        {
        }
    }
}

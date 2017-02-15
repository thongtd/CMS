using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Models;

namespace CMS.DataAccess.Core.Repositories
{
    public interface IExamRepository : IBaseRepository<Exam>
    {
        IEnumerable<ExamResponse> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Exam, bool>> predicate);

        void Add(ExamRequest examRequest);

        Task Update(ExamRequest examRequest);
    }
}

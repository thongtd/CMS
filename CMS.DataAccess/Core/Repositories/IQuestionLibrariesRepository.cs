using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Models;

namespace CMS.DataAccess.Core.Repositories
{
    public interface IQuestionLibrariesRepository : IBaseRepository<QuestionLibraries>
    {
        IEnumerable<QuestionLibrariesResponse> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<QuestionLibraries, bool>> predicate);

        void Add(QuestionLibrariesRequest questionLibrariesRequest);

        Task Update(QuestionLibrariesRequest questionLibrariesRequest);
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        public ExamRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<ExamResponse> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Exam, bool>> predicate)
        {
            var questionLibrarieses = WorkContext.Exams.OrderByDescending(s => s.Id).ToList();

            if (!questionLibrarieses.Any())
            {
                totalRecord = 0;
                return new List<ExamResponse>();
            }

            totalRecord = questionLibrarieses.Count();
            var responses = questionLibrarieses.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList().Select(s => new ExamResponse
            {
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive,
                ExamCategoryId = s.ExamCategoryId,
                ExamCategoryName = WorkContext.ExamCategorys.Find(s.ExamCategoryId)?.Name,
                CreatedDate = s.CreatedDate,
                Description = s.Description,
                ModeifiedDate = s.ModeifiedDate,
                Slug = s.Slug,
                Thumbnail = s.Thumbnail,
                TimeToTest = s.TimeToTest,
                PassNumberOfAnswers = s.PassNumberOfAnswers,
                NumberOfQuestions = s.NumberOfQuestions
            }).ToList();

            return responses;
        }

        public void Add(ExamRequest examRequest)
        {
            throw new NotImplementedException();
        }

        public Task Update(ExamRequest examRequest)
        {
            throw new NotImplementedException();
        }

        private WorkContext WorkContext
        {
            get { return Context as WorkContext; }
        }
    }
}

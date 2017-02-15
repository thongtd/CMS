using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class QuestionLibrariesRepository:BaseRepository<QuestionLibraries>, IQuestionLibrariesRepository
    {
        public QuestionLibrariesRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<QuestionLibrariesResponse> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<QuestionLibraries, bool>> predicate)
        {
            var questionLibrarieses = WorkContext.QuestionLibraries.OrderByDescending(s => s.Id).ToList();

            if (!questionLibrarieses.Any())
            {
                totalRecord = 0;
                return new List<QuestionLibrariesResponse>();
            }

            totalRecord = questionLibrarieses.Count();
            var responses = questionLibrarieses.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList().Select(s => new QuestionLibrariesResponse
            {
                Id = s.Id,
                Question = s.Question,
                Answers = s.Answers != null ? JsonConvert.DeserializeObject<IList<Answer>>(s.Answers) : null,
                IsActive = s.IsActive,
                Title = s.Title,
                ExamCategoryId = s.ExamCategoryId,
                ExamCategoryName = WorkContext.ExamCategorys.Find(s.ExamCategoryId)?.Name
            }).ToList();

            return responses;
        }

        public void Add(QuestionLibrariesRequest questionLibrariesRequest)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var model = (QuestionLibraries) questionLibrariesRequest;

                uow.QuestionLibraries.Add(model);
                uow.Complete();
            }
        }

        public async Task Update(QuestionLibrariesRequest questionLibrariesRequest)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var question = uow.QuestionLibraries.Get(questionLibrariesRequest.Id);
                question.Title = questionLibrariesRequest.Title;
                question.Question = questionLibrariesRequest.Question;
                question.IsActive = questionLibrariesRequest.IsActive;
                question.Answers = questionLibrariesRequest.Answers.Any() ? JsonConvert.SerializeObject(questionLibrariesRequest.Answers) : null;

                uow.Complete();
            }
        }

        public WorkContext WorkContext
        {
            get { return Context as WorkContext; }
        }
    }
}

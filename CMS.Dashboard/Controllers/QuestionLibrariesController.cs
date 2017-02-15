using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CMS.Dashboard.Filters;
using CMS.Dashboard.Models;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using MvcConnerstore.Collections;
using Newtonsoft.Json;

namespace CMS.Dashboard.Controllers
{
    [RoutePrefix("admin")]
    [DashboardActionFilter(IndexPageTile = "Danh sách câu hỏi", EditPageTile = "Sửa thông câu hỏi", CreatePageTile = "Thêm mới câu hỏi")]
    public class QuestionLibrariesController : Controller
    {
        private readonly IQuestionLibrariesRepository questionLibrariesRepository = new QuestionLibrariesRepository(new WorkContext());

        [Route("question-libraries/get")]
        public ActionResult Get(KendoGridFilterModel filter)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                int totalRecord;
                var questions = uow.QuestionLibraries.Paging(PagedExtention.TryGetPageIndex(filter.page.ToString()), filter.pageSize, out totalRecord, null);

                return Json(new
                {
                    data = questions.Select(s => new
                    {
                        Id = s.Id,
                        Question = new MvcHtmlString(s.Question),
                        IsActive = s.IsActive,
                        Title = s.Title,
                        ExamCategoryName = s.ExamCategoryName
                    }),
                    total = totalRecord
                });
            }
        }

        [Route("question-libraries")]
        public ActionResult Index()
        {
            ViewBag.Question = "active";

            return View();
        }

        [Route("question-libraries/create")]
        public ActionResult Create()
        {
            ViewBag.Question = "active";

            return View();
        }

        [HttpPost, ValidateInput(false), Route("question-libraries/create")]
        public async Task<ActionResult> Create(QuestionLibrariesRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            questionLibrariesRepository.Add(model);

            return RedirectToAction("Index");
        }

        [Route("question-libraries/edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Product = "active";

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var questionLibraries = uow.QuestionLibraries.Get(id);

                var model = new QuestionLibrariesRequest
                {
                    Id = questionLibraries.Id,
                    Title = questionLibraries.Title,
                    Question = questionLibraries.Question,
                    IsActive = questionLibraries.IsActive,
                    Answers= JsonConvert.DeserializeObject<IList<Answer>>(questionLibraries.Answers)
                };

                return View(model);
            }
        }

        [HttpPost, ValidateInput(false), Route("question-libraries/edit")]
        public async Task<ActionResult> Edit(QuestionLibrariesRequest model)
        {
            if (ModelState.IsValid)
            {
                await questionLibrariesRepository.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost, Route("question-libraries/active")]
        public ActionResult Active(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var questionLibraries = uow.QuestionLibraries.Get(id);

                questionLibraries.IsActive = !questionLibraries.IsActive;
                uow.Complete();
                return Json(new
                {
                    Status = true,
                    Message = string.Empty
                });
            }
        }

        [HttpPost, Route("question-libraries/delete")]
        public ActionResult Delete(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var questionLibraries = uow.QuestionLibraries.Get(id);

                uow.QuestionLibraries.Remove(questionLibraries);
                uow.Complete();
                return Json(new
                {
                    Status = true,
                    Message = string.Empty
                });
            }
        }
    }
}
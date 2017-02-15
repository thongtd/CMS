using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CMS.Dashboard.Filters;
using CMS.Dashboard.Models;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using MvcConnerstore.Collections;

namespace CMS.Dashboard.Controllers
{
    [RoutePrefix("admin")]
    [DashboardActionFilter(IndexPageTile = "Danh sách đề thi", EditPageTile = "Sửa thông đề thi", CreatePageTile = "Thêm mới đề thi")]
    public class ExamController : Controller
    {
        private readonly IExamRepository examRepository = new ExamRepository(new WorkContext());

        [Route("exam/get")]
        public ActionResult Get(KendoGridFilterModel filter)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                int totalRecord;
                var examResponses = uow.Exam.Paging(PagedExtention.TryGetPageIndex(filter.page.ToString()), filter.pageSize, out totalRecord, null);

                return Json(new
                {
                    data = examResponses.Select(s => new
                    {
                        Id = s.Id,
                        Name = s.Name,
                        IsActive = s.IsActive,
                        ExamCategoryId = s.ExamCategoryId,
                        ExamCategoryName = s.ExamCategoryName,
                        CreatedDate = s.CreatedDate,
                        Description = s.Description,
                        ModeifiedDate = s.ModeifiedDate,
                        Slug = s.Slug,
                        Thumbnail = s.Thumbnail,
                        TimeToTest = s.TimeToTest,
                        PassNumberOfAnswers = s.PassNumberOfAnswers,
                        NumberOfQuestions = s.NumberOfQuestions
                    }),
                    total = totalRecord
                });
            }
        }

        [Route("exam")]
        public ActionResult Index()
        {
            ViewBag.Question = "active";

            return View();
        }

        [Route("exam/create")]
        public ActionResult Create()
        {
            ViewBag.Question = "active";

            return View();
        }

        [HttpPost, ValidateInput(false), Route("exam/create")]
        public async Task<ActionResult> Create(ExamRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            examRepository.Add(model);

            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;
using CMS.DataAccess.Persistence.Repositories;
using MvcConnerstore.Collections;

namespace CMS.Dashboard.Controllers
{
    [RoutePrefix("Dashboard")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository = new BlogRepository(new WorkContext());

        [Route("Blog/Gets")]
        public ActionResult Gets()
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var total = 0;
                var blogs = uow.Blog.Paging(PagedExtention.TryGetPageIndex("1"), int.MaxValue, out total, null);

                return Json(blogs, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("Blog/Index")]
        public ActionResult Index(string pageIndex)
        {
            ViewBag.News = "active";

            return View();
        }

        [Route("Blog/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, Route("Blog/Create")]
        public ActionResult Create(BlogRequest model, FormCollection frmCollect)
        {
            if (ModelState.IsValid)
            {
                var blog = (Blog)model;
                blog.CreatedDate = DateTime.UtcNow;
                var strTags = frmCollect["hidden-tags"];

                using (var uow = new UnitOfWork(new WorkContext()))
                {
                    var blogCategory = uow.BlogCategory.Get(model.BlogCategoryId);

                    blog.CultureCode = blogCategory.CultureCode;
                    var identity = model.IdentityCode = new Guid();
                    uow.Blog.Add(blog);

                    #region Add tags for blog
                    if (!string.IsNullOrEmpty(strTags))
                    {
                        string[] arrTags = strTags.Split(',');
                        var tags = uow.TagCategory.GetAll();
                        var tagIds = (from s in tags where arrTags.Contains(s.Name) select s).ToList();

                        foreach (var item in tagIds)
                        {
                            int tagId = int.Parse(item.Id.ToString());
                            var tag = new Tag()
                            {
                                ObjectName = Constants.ObjectName.Blog,
                                ObjectProperty = Constants.ObjectName.BlogIdenityCode,
                                ObjectIdentityId = identity,
                                TagCategoryId = tagId
                            };
                            uow.Tag.Add(tag);
                        }
                    }
                    #endregion

                    uow.Complete();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [Route("Blog/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var blog = uow.Blog.Get(id);

                return View(blog);
            }
        }

        [HttpPost, Route("Blog/Edit")]
        public ActionResult Edit(BlogRequest model, FormCollection frmCollect)
        {
            if (ModelState.IsValid)
            {
                var tags = frmCollect["hidden-tags"];

                using (var uow = new UnitOfWork(new WorkContext()))
                {
                    var blog = uow.Blog.Get(model.Id);

                    _blogRepository.ConvertToModel(ref blog, model);
                    blog.ModeifiedDate = DateTime.UtcNow;
                    
                    var predicate = PredicateBuilder.Create<Tag>(
                        s => s.ObjectIdentityId == model.IdentityCode && s.ObjectName == Constants.ObjectName.Blog
                        && s.ObjectProperty == Constants.ObjectName.BlogIdenityCode);
                    var oldTags = uow.Tag.Find(predicate).ToList();

                    uow.Tag.RemoveRange(oldTags);

                    if (!string.IsNullOrEmpty(tags))
                    {
                        string[] arrTags = tags.Split(',');
                        var tagCategories = uow.TagCategory.GetAll();
                        var tagIds = (from s in tagCategories where arrTags.Contains(s.Name) select s).ToList();

                        foreach (var item in tagIds)
                        {
                            int tagId = int.Parse(item.Id.ToString());
                            var tag = new Tag
                            {
                                ObjectName = Constants.ObjectName.Blog,
                                ObjectProperty = Constants.ObjectName.BlogIdenityCode,
                                ObjectIdentityId = model.IdentityCode,
                                TagCategoryId = tagId
                            };
                            uow.Tag.Add(tag);
                        }
                    }
                    
                    uow.Complete();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost, Route("Blog/Active")]
        public ActionResult Active(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var blog = uow.Blog.Get(id);

                blog.IsActive = !blog.IsActive;
                uow.Complete();
                return Json(new
                {
                    Status = true,
                    Message = string.Empty
                });
            }
        }

        [HttpPost, Route("Blog/Delete")]
        public ActionResult Delete(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var blog = uow.Blog.Get(id);

                uow.Blog.Remove(blog);
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
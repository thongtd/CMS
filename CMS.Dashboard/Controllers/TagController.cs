using System;
using System.Linq;
using System.Web.Mvc;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Linqkit;
using CMS.DataAccess.Models;
using CMS.DataAccess.Persistence;

namespace CMS.Dashboard.Controllers
{
    [RoutePrefix("admin")]
    public class TagController : Controller
    {
        [HttpPost, Route("tag/get-tags")]
        public ActionResult GetTags(string tag)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var tags = uow.TagCategory.GetAll().ToList();

                var allTags = new string[tags.Count];
                for (var i = 0; i < tags.Count; i++)
                {
                    allTags[i] = tags[i].Name;
                }
                return Json(allTags, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, Route("tag/add-tag")]
        public JsonResult AddTag(string tags)
        {
            try
            {
                if (string.IsNullOrEmpty(tags))
                {
                    return Json(new
                    {
                        Message = "Error",
                        Status = false
                    });
                }

                var arrayTags = tags.Split(',');
                using (var uow = new UnitOfWork(new WorkContext()))
                {
                    foreach (var item in arrayTags)
                    {
                        if (item.Trim().Length > 0)
                        {
                            var tag = item.NameToSlug();

                            var predicate = PredicateBuilder.Create<TagCategory>(s => s.Name.Equals(item));

                            var tagCategorys = uow.TagCategory.Find(predicate).ToList();
                            if (!tagCategorys.Any())
                            {
                                var metaTag = new TagCategory { Name = item.ToLower().Trim(), MetaTag = tag };
                                uow.TagCategory.Add(metaTag);
                            }
                        }
                    }
                    uow.Complete();
                }
                return Json(new
                {
                    Message = "Ok",
                    Status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = ex.Message,
                    Status = false
                });
            }
        }
    }
}
using System;
using System.Linq;
using System.Web.Mvc;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Persistence;

namespace CMS.Dashboard.Controllers
{
    [RoutePrefix("Dashboard")]
    public class TagController : Controller
    {
        [HttpPost]
        [Route("Tag/GetAllTag")]
        public ActionResult GetAllTag(string tag)
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

        [HttpPost]
        [Route("Tag/AddTag")]
        public JsonResult AddTag(string tag)
        {
            try
            {
                if (string.IsNullOrEmpty(tag))
                {
                    return Json(new
                    {
                        Message = "Error",
                        Status = false
                    });
                }

                string[] strArrayTags = tag.Split(',');
                using (var uow = new UnitOfWork(new WorkContext()))
                {
                    foreach (var item in strArrayTags)
                    {
                        if (item.Trim().Length > 0)
                        {
                            string strTags = item.NameToSlug();
                            var tags = uow.Tag.GetAll().ToList();
                            if (!tags.Any())
                            {
                                var objTag = new TagCategory { Name = item.ToLower().Trim(), MetaTag = strTags };
                                uow.TagCategory.Add(objTag);
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
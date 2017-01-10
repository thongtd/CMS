using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(WorkContext context) : base(context)
        {

        }

        public IEnumerable<Tag> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Tag, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetByTop(int top, Expression<Func<Tag, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetTagByBlogId(int blogId)
        {
            throw new NotImplementedException();
        }

        public TagHtmlResponse GetTagsForObject(Guid objectValue, string objectName, string objectProperty)
        {
            var tagsListHtml = new TagHtmlResponse();

            if (objectValue == Guid.Empty) return tagsListHtml;
            var lstTags = WorkContext.Tags
                .Include(s => s.TagCategory).Where(s => s.ObjectIdentityId == objectValue
                                                        && s.ObjectName == objectName && s.ObjectProperty == objectProperty).ToList();

            foreach (var tag in lstTags)
            {
                tagsListHtml.HtmlTag += "<span><a onclick=\"RemoveTags(&quot;" + tag.TagCategory.Name + "&quot;)\" id='" + tag.TagCategory.Name
                                            + "' class=\"ntdelbutton remove-tag\">[x]&nbsp;</a>" + tag.TagCategory.Name + "</span>&nbsp;";
                tagsListHtml.TagValue += tag.TagCategory.Name + ",";
            }
            return tagsListHtml;
        }

        public async Task AddTagToObject(string[] arrTags, string objectName, string objectProperty, Guid objectIdentityId)
        {
            if (!arrTags.Any()) { return; }

            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var tags = await uow.TagCategory.FindAsyn(null);

                var tagExits = tags.Where(t => arrTags.Contains(t.Name)).Select(t => t.Name).ToList();
                var newTags = arrTags.Except(tagExits);

                var tagCategorys = new List<TagCategory>();

                foreach (var tag in newTags)
                {
                    tagCategorys.Add(new TagCategory
                    {
                        Name = tag,
                        MetaTag = tag.NameToSlug()
                    });
                }

                uow.TagCategory.AddRange(tagCategorys);
                uow.Complete();

                var tagIds = await uow.TagCategory.FindAsyn(s => arrTags.Contains(s.Name));

                var tagEntitys = new List<Tag>();
                foreach (var item in tagIds)
                {
                    tagEntitys.Add(new Tag()
                    {
                        ObjectName = objectName,
                        ObjectProperty = objectProperty,
                        ObjectIdentityId = objectIdentityId,
                        TagCategoryId = item.Id
                    });
                }

                uow.Tag.AddRange(tagEntitys);
                uow.Complete();
            }
        }

        public WorkContext WorkContext
        {
            get { return Context as WorkContext; }
        }
    }
}

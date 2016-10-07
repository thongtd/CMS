using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Linq;
using CMS.DataAccess.Core.Domain;
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

        public WorkContext WorkContext
        {
            get { return Context as WorkContext; }
        }
    }
}

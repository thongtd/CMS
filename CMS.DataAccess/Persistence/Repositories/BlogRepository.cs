using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using MvcConnerstore.Collections;
using System.Data.Entity;
using CMS.DataAccess.Core.Linqkit;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository(WorkContext context) : base(context)
        {
        }

        public void ConvertToModel(ref Blog blog, BlogRequest model)
        {
            blog.Name = model.Name;
            blog.Slug = string.IsNullOrEmpty(model.Slug) ? model.Name.NameToSlug() : model.Slug;
            blog.BlogCategoryId = model.BlogCategoryId;
            blog.Thumbnail = model.Thumbnail;
            blog.OriginImage = string.IsNullOrEmpty(model.OriginImage) ? model.Thumbnail : model.OriginImage;
            blog.CreatedDate = model.CreatedDate;
            blog.ModeifiedDate = DateTime.UtcNow;
            blog.SubContent = model.SubContent;
            blog.BodyContent = model.BodyContent;
            blog.Target = model.Target;
            blog.Click = model.Click;
            blog.Title = model.Title;
            blog.Description = model.Description;
            blog.Keyword = model.Keyword;
            blog.CultureCode = model.CultureCode;
            blog.IsActive = model.IsActive;
            blog.PinToTop = model.PinToTop;
        }

        public IEnumerable<BlogResponse> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Blog, bool>> predicate)
        {
            var blogs = WorkContext.Blogs
                .Include(s => s.BlogCategory);

            if (blogs.Any())
            {
                var blogResponses = blogs.Select(s => new BlogResponse
                {
                    BlogCategory = s.BlogCategory,
                    IsActive = s.IsActive,
                    BlogCategoryId = s.BlogCategoryId,
                    Name = s.Name,
                    BodyContent = s.BodyContent,
                    Click = s.Click,
                    ModeifiedDate = s.ModeifiedDate,
                    CreatedDate = s.CreatedDate,
                    CultureCode = s.CultureCode,
                    Description = s.Description,
                    Id = s.Id,
                    Title = s.Title,
                    Slug = s.Slug,
                    OriginImage = s.OriginImage,
                    Keyword = s.Keyword,
                    Thumbnail = s.Thumbnail,
                    PinToTop = s.PinToTop,
                    SubContent = s.SubContent,
                    IdentityCode = s.IdentityCode,
                    Target = s.Target,
                    Tags = WorkContext.Tags.Where(t => s.IdentityCode == t.ObjectIdentityId).ToList()
                }).ToList();

                totalRecord = blogResponses.Count();
                blogResponses = blogResponses.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new PagedList<BlogResponse>(blogResponses, totalRecord);
            }
            
            totalRecord = 0;
            return new PagedList<BlogResponse>(null, 0);
        }

        public IEnumerable<BlogResponse> GetByTop(int top, Expression<Func<Blog, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogResponse> GetTagByBlogId(int blogId)
        {
            throw new NotImplementedException();
        }

        public void Add(BlogRequest model, string collectionTags)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var identity = Guid.NewGuid();

                var blog = (Blog)model;
                blog.CreatedDate = DateTime.UtcNow;

                var blogCategory = uow.BlogCategory.Get(model.BlogCategoryId);

                blog.CultureCode = blogCategory.CultureCode;

                blog.IdentityCode = identity;
                uow.Blog.Add(blog);

                #region Add tags for blog
                if (!string.IsNullOrEmpty(collectionTags))
                {
                    string[] arrTags = collectionTags.Split(',');
                    var tags = uow.TagCategory.FindAll();
                    var tagIds = (from s in tags where arrTags.Contains(s.Name) select s).ToList();

                    foreach (var item in tagIds)
                    {
                        int tagId = int.Parse(item.Id.ToString());
                        var tag = new Tag()
                        {
                            ObjectName = Constants.ObjectName.Blog,
                            ObjectIdentityId = identity,
                            TagCategoryId = tagId
                        };
                        uow.Tag.Add(tag);
                    }
                }
                #endregion

                uow.Complete();
            }
        }

        public void Update(BlogRequest model, string tags)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var blog = uow.Blog.Get(model.Id);

                ConvertToModel(ref blog, model);
                blog.ModeifiedDate = DateTime.UtcNow;

                var predicate = PredicateBuilder.Create<Tag>(s => s.ObjectIdentityId == blog.IdentityCode && s.ObjectName == Constants.ObjectName.Blog);
                var oldTags = uow.Tag.Find(predicate).ToList();

                uow.Tag.RemoveRange(oldTags);

                if (!string.IsNullOrEmpty(tags))
                {
                    string[] arrTags = tags.Split(',');
                    var tagCategories = uow.TagCategory.FindAll();

                    var tagIds = (from s in tagCategories where arrTags.Contains(s.Name) select s).ToList();

                    foreach (var item in tagIds)
                    {
                        int tagId = int.Parse(item.Id.ToString());
                        var tag = new Tag
                        {
                            ObjectName = Constants.ObjectName.Blog,
                            ObjectIdentityId = blog.IdentityCode,
                            TagCategoryId = tagId
                        };
                        uow.Tag.Add(tag);
                    }
                }

                uow.Complete();
            }
        }

        public WorkContext WorkContext
        {
            get { return Context as WorkContext; }
        }
    }
}
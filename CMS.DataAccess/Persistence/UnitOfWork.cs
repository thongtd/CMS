﻿using CMS.DataAccess.Core;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Persistence.Repositories;

namespace CMS.DataAccess.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly WorkContext WorkContext;

        public UnitOfWork(WorkContext workContext)
        {
            WorkContext = workContext;
            BlogCategory = new BlogCategoryRepository(WorkContext);
            Blog = new BlogRepository(WorkContext);
            Contact = new ContactRepository(WorkContext);
            GalleryCategory = new GalleryCategoryRepository(WorkContext);
            Gallery = new GalleryRepository(WorkContext);
            TagCategory = new TagCategoryRepository(WorkContext);
            Tag = new TagRepository(WorkContext);
            VideoCategory = new VideoCategoryRepository(WorkContext);
            Video = new VideoRepository(WorkContext);
        }

        public IBlogCategoryRepository BlogCategory { get; private set; }

        public IBlogRepository Blog { get; private set; }

        public IContactRepository Contact { get; private set; }

        public IGalleryCategoryRepository GalleryCategory { get; private set; }

        public IGalleryRepository Gallery { get; private set; }

        public ITagCategoryRepository TagCategory { get; private set; }

        public ITagRepository Tag { get; private set; }

        public IVideoCategoryRepository VideoCategory { get; private set; }

        public IVideoRepository Video { get; private set; }

        public int Complete()
        {
            return WorkContext.SaveChanges();
        }

        public void Dispose()
        {
            WorkContext.Dispose();
        }
    }
}
using System;
using CMS.DataAccess.Core.Repositories;

namespace CMS.DataAccess.Core
{
    public interface IUnitOfWork : IDependency, IDisposable
    {
        IBlogCategoryRepository BlogCategory { get; }

        IBlogRepository Blog { get;  }

        IContactRepository Contact { get;  }

        IGalleryCategoryRepository GalleryCategory { get;  }

        IGalleryRepository Gallery { get; }

        ITagCategoryRepository TagCategory { get; }

        ITagRepository Tag { get;  }

        IVideoCategoryRepository VideoCategory { get; }

        IVideoRepository Video { get;  }

        int Complete();
    }
}

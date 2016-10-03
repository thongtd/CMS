using System;
using System.Collections.Generic;

namespace MvcConnerstore.Collections
{

    [Serializable]
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> source)
        {
            AddRange(source);
            PageIndex = 1;
            PageSize = Count;
            ItemCount = Count;
            PageCount = 1;
        }

        public PagedList(IEnumerable<T> source, int itemCount)
        {
            if (source != null)
            {
                AddRange(source);
                PageIndex = 1;
                ItemCount = itemCount;
            }
        }

        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int itemCount)
        {
            ItemCount = itemCount;
            PageCount = (int) Math.Ceiling((double) itemCount/pageSize);
            PageIndex = pageIndex;
            PageSize = pageSize;

            AddRange(source);
        }

        #region IPagedList<T> Members

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int ItemCount { get; private set; }

        public int PageCount { get; private set; }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 1); }
        }

        public bool HasNextPage
        {
            get { return (PageIndex < PageCount); }
        }

        #endregion IPagedList<T> Members
    }

    public class PagedList<TParent, T> : PagedList<T>
    {
        public PagedList(TParent parent, IEnumerable<T> source)
            : base(source)
        {
            Parent = parent;
        }

        public PagedList(TParent parent, IEnumerable<T> source, int pageIndex, int pageSize, int itemCount)
            : base(source, pageIndex, pageSize, itemCount)
        {
            Parent = parent;
        }

        public TParent Parent { get; set; }
    }

    public static class PagedExtention
    {
        public static int PageSize = 50;
        public static int TryGetPageIndex(string pageIndex)
        {
            try
            {
                return int.Parse(pageIndex);
            }
            catch (Exception)
            {
                return 1;
            }
        }
    }
}

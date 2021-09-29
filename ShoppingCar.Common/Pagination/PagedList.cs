using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCar.Common.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public IEnumerable<T> Data { get; private set; }


        public bool HasPrevious
        {
            get
            {
                return (CurrentPage > 1);
            }
        }
        public bool HasNext
        {
            get
            {
                return (CurrentPage < TotalPages);
            }
        }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            Data = items;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            if (pageNumber == 0) pageNumber = 1;

            AddRange(items);
        }
        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber == 0) pageNumber = 1;
            var count = source.Count();
            if (count > 0)
            {
                var items = source.Skip((pageNumber - 1) * pageSize)
               .Take(pageSize).ToList();
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
            return null;

        }
    }
}

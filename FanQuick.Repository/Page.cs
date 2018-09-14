using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanQuick.Repository
{
    public class Page<T> where T : class
    {
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Datas { get; set; }
        /// <summary>
        /// 分页 页码从一开始
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Page<T> GetPage(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var page = new Page<T>();
            page.TotalCount = source.Count();
            page.TotalPages = page.TotalCount / pageSize;
            if (page.TotalCount % pageSize > 0)
            {
                page.TotalPages++;
            }
            page.PageSize = pageSize;
            page.PageIndex = pageIndex;
            var res = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            page.Datas = res.Any() ? res.ToList() : new List<T>();
            page.HasPreviousPage = pageIndex - 1 > 0;
            page.HasNextPage = pageIndex < page.TotalPages;
            return page;
        }
    }
}

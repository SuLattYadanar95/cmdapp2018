using PagedList;
using System.Collections.Generic;

namespace DataDC.ViewModels
{
    public class Model<T>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string prevLink { get; set; }
        public string nextLink { get; set; }
        public T[] Results { get; set; }
    }

    public class PagedListModelForServer<T>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string prevLink { get; set; }
        public string nextLink { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
    public class PagedListModelForClient<T>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string prevLink { get; set; }
        public string nextLink { get; set; }
        public IPagedList<T> Results { get; set; }
    }

    public class PageCountInfo
    {
        public int TotalPageCount { get; set; }
        public int TotalRecordCount { get; set; }
        public PageCountInfo(int totalrecordcount, int pagesize)

        {
            TotalRecordCount = totalrecordcount;
            var mod = TotalRecordCount % pagesize;
            TotalPageCount = (TotalRecordCount / pagesize) + (mod == 0 ? 0 : 1);

        }
        public PageCountInfo() { }

    }
}

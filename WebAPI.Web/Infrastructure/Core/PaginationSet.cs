using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Web.Infrastructure.Core
{
    public class PaginationSet<T>
    {
        public int Page { set; get; }
        //public int PageSize { set; get; }

        public int Count
        {
            get
            {
                return (Items != null) ? Items.Count() : 0; //nếu items !=null thì đếm, ngược lại thì = 0
            }
            set { }
        }

        public int TotalPages { set; get; }
        public int TotalCount { set; get; }
        public int Maxpage { set; get; }

        public IEnumerable<T> Items { set; get; }
    }
}
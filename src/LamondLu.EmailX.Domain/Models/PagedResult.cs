using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.Models
{
    public class PagedResult<T>
    {
        private List<T> _data = null;
        private int totalCount = 0;
        private int pageSize = 0;
        private int pageCount = 0;

        public PagedResult(List<T> data, int totalCount, int pageSize, int pageCount)
        {
            this._data = data;
            this.totalCount = totalCount;
            this.pageSize = pageSize;
            this.pageCount = pageCount;
        }

        public List<T> Data
        {
            get
            {
                return _data;
            }
        }

        public int TotalCount
        {
            get
            {
                return totalCount;
            }
        }

        public int PageSize
        {
            get
            {
                return pageSize;
            }
        }

        public int PageCount
        {
            get
            {
                return pageCount;
            }
        }
    }
}
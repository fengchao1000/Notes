using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class PagerSourceDto
    {
        public object Data
        {
            get;
            set;
        }

        public int PageCount
        {
            get;
            set;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public int RecordCount
        {
            get;
            set;
        }
    }
    public class PagerSourceDto<TData>
    {
        public List<TData> Data
        {
            get;
            set;
        }

        public int PageCount
        {
            get;
            set;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public int RecordCount
        {
            get;
            set;
        }
    }
}

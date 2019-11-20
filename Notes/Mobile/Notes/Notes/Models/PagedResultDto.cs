using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class PagedResultDto<TData>
    {
        public List<TData> Items { get; set; }
         
        public long TotalCount { get; set; }
    }
}

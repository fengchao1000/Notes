using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class PagedList<T>
    {
        public List<T> ListData { get; set; }
        public int RecordCount { get; set; }
    }
}

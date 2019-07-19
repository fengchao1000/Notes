using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class PagerEntity
    { 
        public int PageSize { get; set; } = 100;
        public int CurrentPage { get; set; } = 1;
    }
}

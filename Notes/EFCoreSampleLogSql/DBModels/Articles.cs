using System;
using System.Collections.Generic;

namespace EFCoreSampleLogSql.DBModels
{
    public partial class Articles
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Body { get; set; } 
        public DateTime CreateTime { get; set; }
    }
}

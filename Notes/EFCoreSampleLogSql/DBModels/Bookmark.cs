using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EFCoreSampleLogSql.DBModels
{
    public class Bookmark
    {
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
       
        public string Summary { get; set; }
       
        public string LinkUrl { get; set; }
        public int LinkSourceType { get; set; } = 0;
        public string Content { get; set; }
        public bool IsRead { get; set; } 
        public bool IsCrawl { get; set; }
        public virtual Collection<BookmarkCategory> Categorys { get; set; } = new Collection<BookmarkCategory>();

        public Guid? TenantId { get; set; }

        public virtual DateTime CreationTime { get; set; }
        public virtual Guid? CreatorId { get; set; }
    }
}

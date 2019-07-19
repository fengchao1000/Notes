using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace FC.Notes.Bookmarks
{
    /// <summary>
    /// 书签
    /// </summary>
    public class Bookmark : FullAuditedAggregateRoot<Guid>
    { 
        public string Title { get; set; }
        public string Summary { get; set; }
        public string LinkUrl { get; set; }
        public LinkSourceType LinkSourceType { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        /// <summary>
        /// 是否爬取内容
        /// </summary>
        public bool IsCrawl { get; set; }
        public virtual Collection<BookmarkTag> Tags { get; protected set; }
    }
}

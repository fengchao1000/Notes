using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace FC.Notes.Bookmarks
{ 
    /// <summary>
    /// 多对多关系
    /// </summary>
    public class BookmarkTag : CreationAuditedEntity
    {
        public virtual Guid BookmarkId { get; protected set; }

        public virtual Guid TagId { get; protected set; }

        protected BookmarkTag()
        {

        }

        public BookmarkTag(Guid bookmarkId, Guid tagId)
        {
            BookmarkId = bookmarkId;
            TagId = tagId;
        }

        public override object[] GetKeys()
        {
            return new object[] { BookmarkId, TagId };
        }
    }
}

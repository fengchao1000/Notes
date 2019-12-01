using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace FC.Notes.Bookmarks
{ 
    /// <summary>
    /// 多对多关系
    /// </summary>
    public class BookmarkTag : CreationAuditedEntity,IMultiTenant
    {
        public virtual Guid BookmarkId { get; protected set; }

        public virtual Guid TagId { get; protected set; }
        public Guid? TenantId { get; set; }
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

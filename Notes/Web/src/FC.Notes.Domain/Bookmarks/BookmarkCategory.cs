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
    public class BookmarkCategory : CreationAuditedEntity,IMultiTenant
    {
        public virtual Guid BookmarkId { get; protected set; }

        public virtual Guid CategoryId { get; protected set; }
        public Guid? TenantId { get; set; }

        protected BookmarkCategory()
        {

        }

        public BookmarkCategory(Guid bookmarkId, Guid categoryId)
        {
            BookmarkId = bookmarkId;
            CategoryId = categoryId;
        }

        public override object[] GetKeys()
        {
            return new object[] { BookmarkId, CategoryId };
        }
    }
}

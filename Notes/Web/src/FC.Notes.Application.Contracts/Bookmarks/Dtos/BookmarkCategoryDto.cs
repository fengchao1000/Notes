using System;
using System.Collections.Generic;
using System.Text;

namespace FC.Notes.Bookmarks.Dtos
{
    public class BookmarkCategoryDto
    {
        public virtual Guid BookmarkId { get; protected set; }

        public virtual Guid CategoryId { get; protected set; }
        public Guid? TenantId { get; set; }
    }
}

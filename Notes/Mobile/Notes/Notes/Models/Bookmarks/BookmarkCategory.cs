using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models.Bookmarks
{
    public class BookmarkCategory
    {
        public virtual Guid BookmarkId { get;  set; }

        public virtual Guid CategoryId { get;  set; }
        public Guid? TenantId { get; set; }
    }
}

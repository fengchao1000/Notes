using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreSampleLogSql.DBModels
{
    public class BookmarkCategory
    {
        public Guid BookmarkId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? CreatorId { get; set; }
        public Guid? TenantId { get; set; }
    }
}

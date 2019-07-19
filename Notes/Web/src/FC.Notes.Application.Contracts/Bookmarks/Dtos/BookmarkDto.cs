using FC.Notes.Tagging.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FC.Notes.Bookmarks.Dtos
{
    public class BookmarkDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string LinkUrl { get; set; }
        public LinkSourceType LinkSourceType { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; } 
        public List<TagDto> Tags { get; set; }
    }
}

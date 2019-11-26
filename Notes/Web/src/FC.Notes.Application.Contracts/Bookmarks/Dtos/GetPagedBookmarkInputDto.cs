using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FC.Notes.Bookmarks.Dtos
{
    public class GetPagedBookmarkInputDto: PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; } 
        public Guid? CategoryId { get; set; }
    }
}

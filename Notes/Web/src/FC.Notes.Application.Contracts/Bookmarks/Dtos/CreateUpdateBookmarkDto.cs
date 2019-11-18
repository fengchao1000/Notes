using FC.Notes.Posts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FC.Notes.Bookmarks.Dtos
{
    public class CreateUpdateBookmarkDto  
    { 
        [Required]
        [StringLength(BookmarkConsts.MaxTitleLength)]
        public string Title { get; set; }  
        public string Summary { get; set; } 
        [Required]
        [StringLength(BookmarkConsts.MaxUrlLength)]
        public string LinkUrl { get; set; } 
        
        public LinkSourceType LinkSourceType { get; set; }
        [StringLength(BookmarkConsts.MaxContentLength)]
        public string Content { get; set; }
        public string Tags { get; set; }
        
    }
}

using Notes.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models.Bookmarks
{
    public class Bookmark
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string LinkUrl { get; set; }
        public LinkSourceType LinkSourceType { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreationTime { get; set; }
        
    }
}

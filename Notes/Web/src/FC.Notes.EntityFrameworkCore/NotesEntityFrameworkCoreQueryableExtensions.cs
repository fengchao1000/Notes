using FC.Notes.Bookmarks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FC.Notes
{ 
    public static class NotesEntityFrameworkCoreQueryableExtensions
    {
        public static IQueryable<Bookmark> IncludeBookmarkDetails(this IQueryable<Bookmark> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.Tags)
                .Include(x => x.Categorys);
        }
    }
}

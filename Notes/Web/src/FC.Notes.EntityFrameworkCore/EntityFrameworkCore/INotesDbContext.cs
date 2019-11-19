using FC.Notes.Bookmarks;
using FC.Notes.Categorys;
using FC.Notes.Tagging;
using FC.Notes.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace FC.Notes.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public interface INotesDbContext : IEfCoreDbContext
    {
          DbSet<AppUser> Users { get; set; }
          DbSet<Bookmark> Bookmarks { get; set; }
          DbSet<BookmarkTag> BookmarkTags { get; set; }
          DbSet<Tag> Tags { get; set; }
          DbSet<Category> Categorys { get; set; }
    }
}

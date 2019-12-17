using Microsoft.EntityFrameworkCore;
using FC.Notes.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Users.EntityFrameworkCore;
using FC.Notes.Bookmarks;
using FC.Notes.Tagging;
using FC.Notes.Categorys;

namespace FC.Notes.EntityFrameworkCore
{
      //*这是运行时使用的实际DbContext 
      //*它只包含您的实体。 
      //*它不包括使用的模块的实体，因为每个模块都已经有了 
      //*它自己的DbContext类。如果您想与使用的模块共享一些数据库表， 
      //创建一个像AppUser那样的结构。 
      //* 
      //*不要将此DbContext用于数据库迁移，因为它不包含 
      //*使用模块(如上所述)。有关迁移，请参阅NotesMigrationsDbContext。 
      //* 

      [ConnectionStringName("Default")]
      public class NotesDbContext : AbpDbContext<NotesDbContext>, INotesDbContext
      {
          public DbSet<AppUser> Users { get; set; } 
          public DbSet<Bookmark> Bookmarks { get; set; } 
          public DbSet<BookmarkTag> BookmarkTags { get; set; } 
          public DbSet<Tag> Tags { get; set; } 
          public DbSet<Category> Categorys { get; set; }
          public DbSet<BookmarkCategory> BookmarkCategorys { get; set; }

          /* 在这里添加聚合根/实体的DbSet属性。
           * 还可以将它们映射到NotesDbContextModelCreatingExtensions.ConfigureNotes中
           */

    public NotesDbContext(DbContextOptions<NotesDbContext> options)
            : base(options)
        {
             
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* 在这里配置共享表(包含模块) */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable("AbpUsers"); //与IdentityUser共享同一个表“AbpUsers”

                b.ConfigureFullAudited();
                b.ConfigureExtraProperties();
                b.ConfigureConcurrencyStamp();
                b.ConfigureAbpUser();

                //将定制移动到一个方法，以便我们可以与BookStoreMigrationsDbContext类共享它
                b.ConfigureCustomUserProperties();
            });

            /* 在ConfigureBookStore方法中配置您自己的表/实体 */

            builder.ConfigureNotes();
        }
    }
}

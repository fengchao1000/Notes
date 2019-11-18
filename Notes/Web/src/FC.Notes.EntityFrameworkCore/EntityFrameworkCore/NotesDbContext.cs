using Microsoft.EntityFrameworkCore;
using FC.Notes.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Users.EntityFrameworkCore;
using FC.Notes.Bookmarks;
using FC.Notes.Tagging;

namespace FC.Notes.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See NotesMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class NotesDbContext : AbpDbContext<NotesDbContext>, INotesDbContext
    {
        public DbSet<AppUser> Users { get; set; } 
        public DbSet<Bookmark> Bookmarks { get; set; } 
        public DbSet<BookmarkTag> BookmarkTags { get; set; } 
        public DbSet<Tag> Tags { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside NotesDbContextModelCreatingExtensions.ConfigureNotes
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

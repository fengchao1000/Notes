using FC.Notes.Bookmarks;
using FC.Notes.Categorys;
using FC.Notes.Posts;
using FC.Notes.Tagging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Users;

namespace FC.Notes.EntityFrameworkCore
{
    public static class NotesDbContextModelCreatingExtensions
    {
        public static void ConfigureNotes(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(NotesConsts.DbTablePrefix + "YourEntities", NotesConsts.DbSchema);

            //    //...
            //}); 

            builder.Entity<BookmarkTag>(b =>
            {
                b.ToTable(NotesConsts.DbTablePrefix + "BookmarkTags", NotesConsts.DbSchema);

                b.Property(x => x.BookmarkId).HasColumnName(nameof(BookmarkTag.BookmarkId));
                b.Property(x => x.TagId).HasColumnName(nameof(BookmarkTag.TagId));

                b.HasKey(x => new { x.BookmarkId, x.TagId });
            });

            builder.Entity<Bookmark>(b =>
            {
                b.ToTable(NotesConsts.DbTablePrefix + "Bookmarks", NotesConsts.DbSchema);

                b.ConfigureFullAuditedAggregateRoot();

                b.Property(x => x.Title).IsRequired().HasMaxLength(BookmarkConsts.MaxTitleLength).HasColumnName(nameof(Bookmark.Title));
                b.Property(x => x.Summary).IsRequired(false).HasColumnName(nameof(Bookmark.Summary));
                b.Property(x => x.LinkUrl).IsRequired().HasMaxLength(BookmarkConsts.MaxUrlLength).HasColumnName(nameof(Bookmark.LinkUrl));
                b.Property(x => x.Content).IsRequired(false).HasMaxLength(BookmarkConsts.MaxContentLength).HasColumnName(nameof(Bookmark.Content));
                b.Property(x => x.IsRead).HasColumnName(nameof(Bookmark.IsRead)).HasDefaultValue(false);
                b.Property(x => x.IsCrawl).HasColumnName(nameof(Bookmark.IsCrawl)).HasDefaultValue(false);

                b.HasMany(p => p.Tags).WithOne().HasForeignKey(qt => qt.BookmarkId);

            });

            builder.Entity<Tag>(b =>
            {
                b.ToTable(NotesConsts.DbTablePrefix + "Tags", NotesConsts.DbSchema);

                b.ConfigureFullAuditedAggregateRoot();

                b.Property(x => x.Name).IsRequired().HasMaxLength(TagConsts.MaxNameLength).HasColumnName(nameof(Tag.Name));
                b.Property(x => x.Description).HasMaxLength(TagConsts.MaxDescriptionLength).HasColumnName(nameof(Tag.Description));
                b.Property(x => x.UsageCount).HasColumnName(nameof(Tag.UsageCount));

                b.HasMany<BookmarkTag>().WithOne().HasForeignKey(qt => qt.TagId);
            });

            builder.Entity<Category>(b =>
            {
                b.ToTable(NotesConsts.DbTablePrefix + "Categorys", NotesConsts.DbSchema);

                b.ConfigureFullAuditedAggregateRoot();

                b.Property(x => x.Name).IsRequired().HasMaxLength(TagConsts.MaxNameLength).HasColumnName(nameof(Category.Name));
                b.Property(x => x.Description).HasMaxLength(TagConsts.MaxDescriptionLength).HasColumnName(nameof(Category.Description));
                b.Property(x => x.UsageCount).HasColumnName(nameof(Category.UsageCount));
                
            });
        }

        public static void ConfigureCustomUserProperties<TUser>(this EntityTypeBuilder<TUser> b)
            where TUser: class, IUser
        {
            //b.Property<string>(nameof(AppUser.MyProperty))...
        }
    }
}
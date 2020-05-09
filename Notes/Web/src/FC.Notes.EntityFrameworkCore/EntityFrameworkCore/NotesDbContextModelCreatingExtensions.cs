using FC.Notes.Bookmarks;
using FC.Notes.Categorys;
using FC.Notes.Itinerarys;
using FC.Notes.Posts;
using FC.Notes.Tagging;
using FC.Notes.Tasks;
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

            builder.Entity<BookmarkCategory>(b =>
            {
                b.ToTable(NotesConsts.DbTablePrefix + "BookmarkCategorys", NotesConsts.DbSchema);

                b.Property(x => x.BookmarkId).HasColumnName(nameof(BookmarkCategory.BookmarkId));
                b.Property(x => x.CategoryId).HasColumnName(nameof(BookmarkCategory.CategoryId));

                b.HasKey(x => new { x.BookmarkId, x.CategoryId });
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

                b.HasMany(p => p.Tags).WithOne().HasForeignKey(qt => qt.BookmarkId);//外键

                b.HasMany(p => p.Categorys).WithOne().HasForeignKey(qt => qt.BookmarkId);//外键

            });

            builder.Entity<Tag>(b =>
            {
                b.ToTable(NotesConsts.DbTablePrefix + "Tags", NotesConsts.DbSchema);

                b.ConfigureFullAuditedAggregateRoot();

                b.Property(x => x.Name).IsRequired().HasMaxLength(TagConsts.MaxNameLength).HasColumnName(nameof(Tag.Name));
                b.Property(x => x.Description).HasMaxLength(TagConsts.MaxDescriptionLength).HasColumnName(nameof(Tag.Description));
                b.Property(x => x.UsageCount).HasColumnName(nameof(Tag.UsageCount));

                b.HasMany<BookmarkTag>().WithOne().HasForeignKey(qt => qt.TagId);//外键
            });

            builder.Entity<Category>(b =>
            {
                b.ToTable(NotesConsts.DbTablePrefix + "Categorys", NotesConsts.DbSchema);

                b.ConfigureFullAuditedAggregateRoot();

                b.Property(x => x.Name).IsRequired().HasMaxLength(CategoryConsts.MaxNameLength).HasColumnName(nameof(Category.Name));
                b.Property(x => x.Description).HasMaxLength(CategoryConsts.MaxDescriptionLength).HasColumnName(nameof(Category.Description));
                b.Property(x => x.UsageCount).HasColumnName(nameof(Category.UsageCount));
                
            });

            builder.Entity<Itinerary>(b =>
            {
                b.ToTable(NotesConsts.DbTableItinerarysPrefix + "Itinerarys", NotesConsts.DbSchema);

                b.ConfigureFullAuditedAggregateRoot();

                b.Property(x => x.Note).IsRequired().HasMaxLength(ItineraryConsts.MaxNoteLength).HasColumnName(nameof(Itinerary.Note));
                  
            });

            builder.Entity<AccountBook>(b =>
            {
                b.ToTable(NotesConsts.DbTableItinerarysPrefix + "AccountBooks", NotesConsts.DbSchema);

                // Relationships  
                b.HasMany(p => p.OverheadItems).WithOne().HasForeignKey(qt => qt.AccountBookID);//一对多，一个AccountBook包含多个OverheadItem，AccountBookID表示关联id

                b.HasOne<Itinerary>().WithMany().IsRequired().HasForeignKey(p => p.ItineraryID);//一对一，一个Itinerary对应一个AccountBook，ItineraryID表示关联id
            });

            builder.Entity<OverheadItem>(b =>
            {
                b.ToTable(NotesConsts.DbTableItinerarysPrefix + "OverheadItems", NotesConsts.DbSchema);

                b.Property(x => x.Content).IsRequired().HasMaxLength(ItineraryConsts.MaxNoteLength).HasColumnName(nameof(OverheadItem.Content));

            });


            builder.Entity<Task>(b =>
            {
                b.ToTable(NotesConsts.DbTableTaskPrefix + "Tasks", NotesConsts.DbSchema);

                b.ConfigureFullAuditedAggregateRoot(); 

            });


        }

        public static void ConfigureCustomUserProperties<TUser>(this EntityTypeBuilder<TUser> b)
            where TUser: class, IUser
        {
            //b.Property<string>(nameof(AppUser.MyProperty))...
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace EFCoreSampleLogSql.DBModels
{
    public partial class NotesContext : DbContext
    {  
        public const string DbTablePrefix = "BM";

        public static readonly ILoggerFactory MyLoggerFactory
              = LoggerFactory.Create(builder => { builder.AddDebug(); });

        public NotesContext()
        {
        } 

        public NotesContext(DbContextOptions<NotesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<BookmarkCategory> BookmarkCategorys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                optionsBuilder.UseMySql("Server=114.67.251.186;User Id=root;Password=fengchao!@#123;Database=Notes;");
                 
                optionsBuilder.UseLoggerFactory(MyLoggerFactory);
                base.OnConfiguring(optionsBuilder);
            } 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BookmarkCategory>(b =>
            {
                b.ToTable(DbTablePrefix + "BookmarkCategorys", null); 
                b.Property(x => x.BookmarkId).HasColumnName(nameof(BookmarkCategory.BookmarkId));
                b.Property(x => x.CategoryId).HasColumnName(nameof(BookmarkCategory.CategoryId));

                b.HasKey(x => new { x.BookmarkId, x.CategoryId });
            });

            modelBuilder.Entity<Bookmark>(b =>
            {
                b.ToTable(DbTablePrefix + "Bookmarks", null);
                b.Property(x => x.Id).HasColumnName(nameof(Bookmark.Id));
                b.Property(x => x.Title).IsRequired().HasMaxLength(512).HasColumnName(nameof(Bookmark.Title));
                b.Property(x => x.Summary).IsRequired(false).HasColumnName(nameof(Bookmark.Summary));
                b.Property(x => x.LinkUrl).IsRequired().HasMaxLength(64).HasColumnName(nameof(Bookmark.LinkUrl));
                b.Property(x => x.Content).IsRequired(false).HasMaxLength(1024 * 1024).HasColumnName(nameof(Bookmark.Content));
                b.Property(x => x.IsRead).HasColumnName(nameof(Bookmark.IsRead)).HasDefaultValue(false);
                b.Property(x => x.IsCrawl).HasColumnName(nameof(Bookmark.IsCrawl)).HasDefaultValue(false);
                b.Property(x => x.CreationTime).HasColumnName(nameof(Bookmark.CreationTime));
                b.HasMany(p => p.Categorys).WithOne().HasForeignKey(qt => qt.BookmarkId);

            });

            modelBuilder.Entity<Articles>(entity =>
            {
                entity.ToTable("articles");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnType("varchar(200)");
                 
            });
        }
    }
}

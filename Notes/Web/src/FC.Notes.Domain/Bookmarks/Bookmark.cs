using FC.Notes.Categorys;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace FC.Notes.Bookmarks
{
    /// <summary>
    /// 书签
    /// </summary>
    public class Bookmark : FullAuditedAggregateRoot<Guid>,IMultiTenant
    {
        [NotNull]
        public string Title { get; set; }
        [CanBeNull]
        public string Summary { get; set; }
        [NotNull]
        public string LinkUrl { get; set; }
        public LinkSourceType LinkSourceType { get; set; }
        [CanBeNull]
        public string Content { get; set; }
        public bool IsRead { get; set; }

        /// <summary>
        /// 是否爬取内容
        /// </summary>
        public bool IsCrawl { get; set; }
        public virtual Collection<BookmarkTag> Tags { get; protected set; }
        public virtual Collection<BookmarkCategory> Categorys { get; protected set; }

        public Guid? TenantId { get; set; }
        public Guid? UserId { get; set; }

        protected Bookmark()
        {

        }

        public Bookmark(Guid id, [NotNull] string title, [NotNull] string linkUrl)
        {
            Id = id; 
            Title = Check.NotNullOrWhiteSpace(title, nameof(title));
            LinkUrl = Check.NotNullOrWhiteSpace(linkUrl, nameof(linkUrl));  
            Tags = new Collection<BookmarkTag>();
            Categorys = new Collection<BookmarkCategory>();
        }

        public virtual void RemoveTag(Guid tagId)
        {
            Tags.RemoveAll(t => t.TagId == tagId);
        }

        public virtual void RemoveCategory(Guid categoryId)
        {
            Categorys.RemoveAll(t => t.CategoryId == categoryId);
        }

        public virtual void AddCategory(Guid categoryId)
        {
            Categorys.Add(new BookmarkCategory(Id, categoryId));
        }

        public virtual void AddTag(Guid tagId)
        {
            Tags.Add(new BookmarkTag(Id, tagId));
        }
    }
}

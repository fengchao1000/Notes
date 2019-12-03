using Notes.Helpers;
using Notes.Interfaces.Bookmarks;
using Notes.Models;
using Notes.Models.Bookmarks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Bookmarks
{
    public class BookmarkService : BaseService<Bookmark>, IBookmarkService
    {
        public async Task<ResultData<PagedResultDto<Bookmark>>> GetBookmarks(string skipCount, string maxResultCount, int sorting)
        {
            return await RequestProvider.Current.GetAsync<PagedResultDto<Bookmark>>($"{AppConfig.GetBookmarkUrl}");
        }

        public async Task<ResultData<PagedResultDto<Bookmark>>> GetBookmarkPaged(string keyword, Guid? categoryId, int skipCount = 0, int maxResultCount = 10, string sorting = null)
        {
            return await RequestProvider.Current.GetAsync<PagedResultDto<Bookmark>>($"{AppConfig.GetBookmarkPagedUrl}?keyword={keyword}&categoryId={categoryId}&skipCount={skipCount}&maxResultCount={maxResultCount}&sorting={sorting}");
        }

        public async Task<ResultData<Bookmark>> UpdateRead(Guid id, bool isRead)
        {
            return await RequestProvider.Current.PutAsync<Bookmark>(string.Format(AppConfig.UpdateBookmarkReadUrl, id, isRead));
        }

        /// <summary>
        /// 查询Bookmark
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<Bookmark>> SearchBookmarkFromSqlite(string keyword, Guid? categoryId, int skipCount = 0, int maxResultCount = 0, string sorting = null)
        {
            var query = TableQueryFromSqliteAsync();

            if (categoryId != null)
            {
                string queryCategoryId = categoryId.ToString();
                query = query.Where(q => q.CategoryIds.Contains(queryCategoryId));
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(q => q.Title.Contains(keyword) || q.Content.Contains(keyword));
            }

            PagedResultDto<Bookmark> bookmarkPagedList = new PagedResultDto<Bookmark>();
            bookmarkPagedList.Items = await query.OrderByDescending(a => a.CreationTime).Skip(skipCount).Take(maxResultCount).ToListAsync();
            bookmarkPagedList.TotalCount = await query.CountAsync();

            return bookmarkPagedList;
        }
    }
}

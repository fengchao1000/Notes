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
    public class BookmarkService : IBookmarkService
    {
        public async Task<ResultData<PagedResultDto<Bookmark>>> GetBookmarks(string skipCount, string maxResultCount, int sorting)
        {
            return await RequestProvider.Current.GetAsync<PagedResultDto<Bookmark>>($"{AppConfig.BookmarkUrl}");
        }
    }
}

using Notes.Models;
using Notes.Models.Bookmarks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces.Bookmarks
{ 
    public interface IBookmarkService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        Task<ResultData<PagedResultDto<Bookmark>>> GetBookmarks(string skipCount, string maxResultCount, int sorting);
    }
}

using Notes.Models;
using Notes.Models.Bookmarks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces.Bookmarks
{ 
    public interface IBookmarkService : IBaseService<Bookmark>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        Task<ResultData<PagedResultDto<Bookmark>>> GetBookmarks(string skipCount, string maxResultCount, int sorting);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="categoryId"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        Task<ResultData<PagedResultDto<Bookmark>>> GetBookmarkPaged(string keyword, Guid? categoryId, int skipCount = 0, int maxResultCount = 0, string sorting = null);

        /// <summary>
        /// 已读
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isRead"></param>
        /// <returns></returns>
        Task<ResultData<Bookmark>> UpdateRead(Guid id, bool isRead);

        /// <summary>
        /// SearchBookmarkFromSqlite
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="categoryId"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        Task<PagedResultDto<Bookmark>> SearchBookmarkFromSqlite(string keyword, Guid? categoryId, int skipCount = 0, int maxResultCount = 0, string sorting = null);
    }
}

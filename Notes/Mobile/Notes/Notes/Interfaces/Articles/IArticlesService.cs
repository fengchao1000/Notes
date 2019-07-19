using Notes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Notes.Models.Articles;

namespace Notes.Interfaces.Articles
{
    public interface IArticlesService
    {
        /// <summary>
        /// 查询消息分页数据
        /// </summary>
        /// <param name="mobileMessageSearchParam"></param>
        /// <returns></returns>
        Task<ResultData<PagerSourceDto<ArticlesDto>>> GetArticlesPage(string title, string content, int pageIndex, int pageSize, string sortBy, bool isAsc);

        /// <summary>
        /// 查询单个消息
        /// </summary>
        /// <param name="messageGetParam"></param>
        /// <returns></returns>
        //Task<ResultData<MobileBaseMessageEntity>> GetMessage(MobileMessageGetParam messageGetParam);

        /// <summary>
        /// 修改消息为不显示
        /// </summary>
        /// <param name="messageGetParam"></param>
        /// <returns></returns>
        //Task<ResultData<bool>> UpdateMessageIsNotDisplay(MobileReadMessageUpdateParam readMessageUpdateParam);

        /// <summary>
        /// 添加已读消息
        /// </summary>
        /// <param name="readMessageEntity"></param>
        /// <returns></returns>
        //Task<ResultData<bool>> AddReadMessage(MobileReadMessageEntity readMessageEntity);

        #region Sqlite 

        /// <summary>
        /// 根据key获取单条Message
        /// </summary>
        /// <param name="pushMessageKey"></param>
        /// <returns></returns>
        Task<ArticlesDto> GetArticlesFromSqlite(int id);

        /// <summary>
        /// 获取分页信息列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IList<ArticlesDto>> GetArticlesFromSqlite(int pageIndex,int pageSize);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="messageList"></param>
        /// <returns></returns>
        Task InsertArticlesListToSqlite(IList<ArticlesDto> messageList);

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="messageList"></param>
        /// <returns></returns>
        Task UpdateArticlesToSqlite(IList<ArticlesDto> messageList);

        /// <summary>
        /// 更新信息
        /// </summary> 
        /// <returns></returns>
        Task<int> UpdateArticlesToSqlite(ArticlesDto message);

        /// <summary>
        /// 删除发货统计数据
        /// </summary> 
        /// <returns></returns>
        Task<int> DeleteArticlesFromSqlite(int id);

        #endregion
    }
}

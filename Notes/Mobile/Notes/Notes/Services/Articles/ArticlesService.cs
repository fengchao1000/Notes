using Notes.Interfaces.Articles;
using Notes.Models;
using Notes.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Articles
{
    public class ArticlesService : IArticlesService
    {
        public Task<int> DeleteArticlesFromSqlite(int messageKey)
        {
            throw new NotImplementedException();
        }

        public Task<ArticlesDto> GetArticlesFromSqlite(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ArticlesDto>> GetArticlesFromSqlite(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ResultData<PagerSourceDto<ArticlesDto>>> GetArticlesPage(string title, string content, int pageIndex, int pageSize, string sortBy, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public Task InsertArticlesListToSqlite(IList<ArticlesDto> messageList)
        {
            throw new NotImplementedException();
        }

        public Task UpdateArticlesToSqlite(IList<ArticlesDto> messageList)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateArticlesToSqlite(ArticlesDto message)
        {
            throw new NotImplementedException();
        }
    }
}

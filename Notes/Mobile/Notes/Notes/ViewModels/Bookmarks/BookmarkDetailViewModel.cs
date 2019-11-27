using Notes.Helpers;
using Notes.Models.Bookmarks;
using Notes.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.ViewModels.Bookmarks
{ 
     public class BookmarkDetailViewModel : BaseViewModel
    {
        #region 属性
         

        bool hasError;
        public bool HasError
        {
            get => hasError;
            set
            {
                hasError = value;
                RaisePropertyChanged(() => HasError);
            }
        }

        Bookmark bookmark;

        #endregion

        #region 方法

        public BookmarkDetailViewModel(Bookmark bookmark)
        {
            this.bookmark = bookmark;
        }

        /// <summary>
        /// 从api刷新数据
        /// </summary>
        /// <returns></returns>
        public async Task<Bookmark> GetBookmarkAsync()
        {
            try
            {
                IsBusy = true;
                HasError = false;
                LoadStatus = LoadMoreStatus.StausLoading;

                if (string.IsNullOrEmpty(bookmark.LinkUrl))
                {
                    return bookmark;
                }

                string linkUrl = bookmark.LinkUrl.Substring(0, bookmark.LinkUrl.LastIndexOf('.'));
                linkUrl = linkUrl.Substring(linkUrl.LastIndexOf('/')+1);

                if (string.IsNullOrEmpty(linkUrl))
                {
                    return bookmark;
                }

                int articleId = 0;

                if (!int.TryParse(linkUrl,out articleId))
                {
                    return bookmark;
                }

                var result = await ServicesManager.ArticlesService.GetArticleBodyAsync(articleId);
                 
                if (!result.IsSuccess)
                {
                    HasError = true;
                    LoadStatus = LoadMoreStatus.StausError;
                    return null;
                }

                if (result.Data == null)
                {
                    LoadStatus = LoadMoreStatus.StausNodata;
                    HasError = true;
                    return bookmark;
                }

                LoadStatus = LoadMoreStatus.StausDefault;

                bookmark.Content = result.Data;

                return bookmark;
            }
            catch (Exception ex)
            {
                HasError = true; 
                return bookmark;
            }
            finally
            {
                IsBusy = false;
            }
        }



        #endregion
    }
}

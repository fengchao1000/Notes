using Notes.Helpers;
using Notes.Models;
using Notes.Models.Bookmarks;
using Notes.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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

                await ServicesManager.BookmarkService.UpdateToSqliteAsync(bookmark);

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

        /// <summary>
        /// 已读
        /// </summary>
        /// <returns></returns>
        public async Task BookmarkReadAsync()
        {
            //TODO: 文章已读未读设置后，列表更新，发布订阅的方式，进度表

            ResultData<Bookmark> result = await ServicesManager.BookmarkService.UpdateRead(bookmark.Id, !bookmark.IsRead);
            if (result.IsSuccess)
            { 
                bookmark.IsRead = true; 

                await ServicesManager.BookmarkService.UpdateToSqliteAsync(bookmark);
                 
                ToastHelper.Current.SendToast("成功!");
            }
            else
            {
                ToastHelper.Current.SendToast("失败!");
            }
        }

        #endregion

            #region 命令 

        public ICommand BookmarkReadCommand => new Command(async () => await BookmarkReadAsync());

        #endregion
    }
}

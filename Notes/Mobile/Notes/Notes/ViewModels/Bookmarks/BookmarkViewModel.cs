using Notes.Helpers;
using Notes.Models.Bookmarks;
using Notes.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels.Bookmarks
{
    public class BookmarkViewModel : BaseViewModel
    {
        #region 属性

        /// <summary>
        /// Category集合
        /// </summary>
        private ObservableCollection<Bookmark> bookmarks = new ObservableCollection<Bookmark>();
        public ObservableCollection<Bookmark> Bookmarks
        {
            get => bookmarks;
            set
            {
                bookmarks = value;
                RaisePropertyChanged(() => Bookmarks);
            }
        }

        /// <summary>
        /// 滑动操作的ItemIndex
        /// </summary>
        internal int ItemIndex
        {
            get;
            set;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化ViewModel
        /// </summary>
        public async void Initialize()
        {
            string refreshKey = nameof(BookmarkViewModel);
            if (DateTime.UtcNow >= RefreshTimeHelper.GetRefreshTime(refreshKey))
            {
                await RefreshDataFromAPIAsync();
            }
            else
            {
                await GetDataFromSqliteAsync();
            }
        }

        /// <summary>
        /// 从Sqlite中获取数据
        /// </summary>
        public async Task GetDataFromSqliteAsync()
        {
            try
            {
                IsBusy = true;
                Stopwatch sw = new Stopwatch();
                sw.Start();

                IList<Bookmark> bookmarkList = await ServicesManager.BookmarkService.GetAllFromSqliteAsync();

                if (bookmarkList != null)
                {
                    Bookmarks.Clear();

                    ObservableCollection<Bookmark> newBookmarkCollection = new ObservableCollection<Bookmark>();

                    foreach (Bookmark item in bookmarkList)
                    {
                        newBookmarkCollection.Add(item);
                    }

                    Bookmarks = newBookmarkCollection;
                }

                sw.Stop();
                LoggerHelper.Current.Debug("EbayMsgMenuViewModel GetDataFromSqliteAsync ElapsedMilliseconds:" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
            }
            finally
            {
                await Task.Delay(ConstanceHelper.RefreshDelay);
                IsBusy = false;
            }
        }

        async Task RefreshDataFromAPIAsync()
        {
            try
            {
                IsBusy = true;

                //查询目录对应数量 
                var result = await ServicesManager.BookmarkService.GetBookmarks("", "", 1);
                if (!result.IsSuccess)
                {
                    return;
                }

                if (result.Data.Items == null)
                {
                    return;
                }

                if (result.Data.Items.Count <= 0)
                {
                    return;
                }

                Bookmarks.Clear();

                foreach (Bookmark item in result.Data.Items)
                {
                    Bookmarks.Add(item);
                }

                await ServicesManager.BookmarkService.BatchUpdateToSqlite(Bookmarks); //保存在sqlite

                LoadStatus = LoadMoreStatus.StausDefault;
                CanLoadMore = true;
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
                LoadStatus = LoadMoreStatus.StausFail;
            }
            finally
            {
                string refreshKey = nameof(BookmarkViewModel);
                RefreshTimeHelper.SetRefreshTime(refreshKey, DateTime.UtcNow.AddMinutes(ConstanceHelper.NextRefreshInterval));//记录刷新时间 
                await Task.Delay(500);
                IsBusy = false;
            }
        }

        #endregion

        #region 命令 

        /// <summary>
        /// 刷新命令
        /// </summary>
        private ICommand refreshCommand;
        public ICommand RefreshCommand => refreshCommand ?? (refreshCommand = new Command(async () => await RefreshDataFromAPIAsync()));

        #endregion
    }
}

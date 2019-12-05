using Notes.Helpers;
using Notes.Models.Bookmarks;
using Notes.Models.Categorys;
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

        string searchText = string.Empty;
        /// <summary>
        /// 查询框输入的文本
        /// </summary> 
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                RaisePropertyChanged(() => SearchText);
            }
        }

        /// <summary>
        /// 当前分类
        /// </summary>
        private Category currentCategory;

        /// <summary>
        /// 滑动操作的ItemIndex
        /// </summary>
        internal int ItemIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        private int currentPage = 1;
        /// <summary>
        /// 每页显示条数
        /// </summary>
        private int pageSize = 10;

        /// <summary>
        /// 总记录数
        /// </summary>
        public long recordCount = 0;


        #endregion

        #region 方法

        public BookmarkViewModel(Category category)
        {
            currentCategory = category;
        }

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

                var result = await ServicesManager.BookmarkService.SearchBookmarkFromSqlite(SearchText, currentCategory.Id, 0, pageSize);

                if (result.Items == null)
                {
                    return;
                }

                if (result.Items.Count <= 0)
                {
                    return;
                }

                Bookmarks.Clear();

                foreach (Bookmark item in result.Items)
                {
                    Bookmarks.Add(item);
                }
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

        public async Task RefreshDataFromAPIAsync()
        {
            try
            {
                IsBusy = true;
                currentPage = 1;

                //查询目录对应数量  
                var result = await ServicesManager.BookmarkService.GetBookmarkPaged(SearchText, currentCategory.Id, 0, pageSize);
                
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
                    item.CategoryIds = GetCategoryIds(item.Categorys);
                    Bookmarks.Add(item); 
                }

                await ServicesManager.BookmarkService.BatchUpdateToSqlite(Bookmarks); //保存在sqlite

                recordCount = result.Data.TotalCount;
                LoadStatus = LoadMoreStatus.StausDefault;
                CanLoadMore = true;
                currentPage++;
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

        /// <summary>
        /// 加载更多
        /// </summary>
        /// <returns></returns>
        public async void LoadMoreDataFromAPIAsync()
        {
            try
            {
                IsBusy = true;

                var result = await ServicesManager.BookmarkService.GetBookmarkPaged(SearchText, currentCategory.Id, (currentPage - 1) * pageSize, pageSize);

                if (!result.IsSuccess)
                {
                    LoadStatus = LoadMoreStatus.StausError;
                    return;
                }

                if (result.Data.Items == null)
                {
                    recordCount = Bookmarks.Count;
                    LoadStatus = LoadMoreStatus.StausEnd;
                    return;
                }

                if (result.Data.Items.Count <= 0)
                {
                    recordCount = Bookmarks.Count;
                    LoadStatus = LoadMoreStatus.StausEnd;
                    return;
                }

                foreach (Bookmark item in result.Data.Items)
                {
                    item.CategoryIds = GetCategoryIds(item.Categorys);
                    Bookmarks.Add(item); 
                }

                recordCount = result.Data.TotalCount;

                if (Bookmarks.Count >= recordCount)
                {
                    LoadStatus = LoadMoreStatus.StausEnd;
                }
                else
                {
                    LoadStatus = LoadMoreStatus.StausDefault;
                }

                await ServicesManager.BookmarkService.BatchUpdateToSqlite(Bookmarks); //保存在sqlite

                currentPage++;

            }
            catch (Exception ex)
            {
                LoadStatus = LoadMoreStatus.StausError;
                LoggerHelper.Current.Error(ex.ToString());
            }
            finally
            {
                IsBusy = false;
                CanLoadMore = true;
            }
        }

        /// <summary>
        /// 能否加载更多
        /// </summary>
        /// <returns></returns>
        private bool CanLoadMoreItems()
        {
            return (Bookmarks.Count < recordCount);
        }

        /// <summary>
        /// 获取CategoryIds字符串
        /// </summary>
        /// <param name="ebayMsgTagList"></param>
        /// <returns></returns>
        private string GetCategoryIds(List<BookmarkCategory> bookmarkCategoryList)
        {
            StringBuilder categoryIds = new StringBuilder();
            foreach (BookmarkCategory item in bookmarkCategoryList)
            {
                categoryIds.Append(item.CategoryId + "&");
            }
            string categoryIdsString = categoryIds.ToString();
            if (categoryIdsString.EndsWith("&"))
            {
                categoryIdsString = categoryIdsString.Substring(0, categoryIdsString.Length - 1);
            }
            return categoryIdsString;
        }

        #endregion

        #region 命令 

        /// <summary>
        /// 刷新命令
        /// </summary> 

        public ICommand RefreshCommand => new Command(async () => await RefreshDataFromAPIAsync());

        public ICommand LoadMoreItemsCommand => new Command(LoadMoreDataFromAPIAsync, CanLoadMoreItems);

        public ICommand SearchCommand => new Command(async () => await RefreshDataFromAPIAsync());
        #endregion
    }
}

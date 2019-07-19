using Microsoft.AppCenter.Crashes; 
using Notes.Helpers; 
using Notes.Models;
using Notes.Models.Articles;
using Notes.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class ArticlesViewModel : BaseViewModel
    {
        #region 属性

        /// <summary>
        /// 消息集合
        /// </summary>
        private ObservableCollection<ArticlesDto> messageCollection = new ObservableCollection<ArticlesDto>();
        public ObservableCollection<ArticlesDto> MessageCollection
        {
            get => messageCollection;
            set
            {
                messageCollection = value;
                RaisePropertyChanged(() => MessageCollection);
            }
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
        /// 允许加载的最大消息条数
        /// </summary>
        private int maxCountAllowedToLoadMore = 100;

        /// <summary>
        /// 滑动操作的ItemIndex
        /// </summary>
        internal int ItemIndex
        {
            get;
            set;
        }

        #endregion

        #region 命令

        /// <summary>
        /// 刷新命令
        /// </summary>
        ICommand refreshCommand;
        public ICommand RefreshCommand =>
            refreshCommand ?? (refreshCommand = new Command(async () =>
            {
                try
                {
                    //记录刷新时间 
                    RefreshTimeHelper.SetRefreshTime(RefreshTimeKeys.MessagePagedListRefreshTimeKey, DateTime.UtcNow.AddMinutes(ConstanceHelper.NextRefreshInterval));

                    IsBusy = true;
                    CanLoadMore = false;
                    currentPage = 1;
                    await ExecuteRefreshCommandAsync();
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    LoadStatus = LoadMoreStatus.StausFail;
                }
                finally
                {
                    await Task.Delay(500);
                    IsBusy = false;
                }
            }));

        /// <summary>
        /// 加载更多命令
        /// </summary>
        ICommand loadMoreCommand;
        public ICommand LoadMoreCommand
            => loadMoreCommand ??
            (
                loadMoreCommand = new Command
                (
                    async () =>
                    {
                        try
                        { 
                            IsBusy = true;
                            CanLoadMore = false; 
                            await ExecuteRefreshCommandAsync();
                        }
                        catch (Exception ex)
                        {
                            Crashes.TrackError(ex);
                            LoadStatus = LoadMoreStatus.StausFail;
                        }
                        finally
                        {
                            IsBusy = false;
                        } 
                    }
                    ,
                    CanLoadMoreItems
                 )
            );
         

        #endregion

        #region 方法

        /// <summary>
        /// 加载消息
        /// </summary>
        public void LoadMessagePageList()
        {
            if (DateTime.UtcNow >= RefreshTimeHelper.GetRefreshTime(RefreshTimeKeys.MessagePagedListRefreshTimeKey))
            {
                RefreshCommand.Execute(null);
            }
            else
            {
                GetClientMessagesAsync();
            }
        }

        /// <summary>
        /// 从Sqlite中获取数据
        /// </summary>
        public async void GetClientMessagesAsync()
        {
            try
            {
                IList<ArticlesDto> messageList = await ServicesManager.ArticlesService.GetArticlesFromSqlite(1,pageSize);
                if (messageList != null && messageList.Count > 0 )
                {
                    MessageCollection.Clear();
                    foreach (ArticlesDto item in messageList)
                    {
                        MessageCollection.Add(item);
                    }
                }
                else
                {
                    RefreshCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(new Exception() { Source = ex.ToString() });
            }
        }

        /// <summary>
        /// 当第一次进入页面或者用户刷新时，CurrentPage是第一页，请求API获取数据，清空MessageCollection历史数据，把从API获取的数据添加到MessageCollection
        /// 当用户加载更多内容时，CurrentPage大于1，不清空MessageCollection历史数据，把从API获取的数据添加到MessageCollection
        /// 当当前MessageCollection中的总条数小于RecordCount时，可以执行载更多命令
        /// </summary>
        /// <returns></returns>
        async Task ExecuteRefreshCommandAsync()
        {
            try
            {   
                var result = await ServicesManager.ArticlesService.GetArticlesPage("","",1,10,"",false); 
                if (!result.IsSuccess)
                {
                    Crashes.TrackError(new Exception() { Source = result.Message });
                    IsBusy = false;
                    LoadStatus = currentPage > 1 ? LoadMoreStatus.StausError : LoadMoreStatus.StausFail;
                    return;
                }
                if (result.Data == null )
                {
                    CanLoadMore = false;
                    IsBusy = false;
                    LoadStatus = currentPage > 1 ? LoadMoreStatus.StausEnd : LoadMoreStatus.StausNodata;
                    return;
                }

                if (currentPage == 1 && MessageCollection.Count > 0)
                {
                    MessageCollection.Clear();
                }
                foreach (ArticlesDto item in result.Data.Data)
                {
                    MessageCollection.Add(item);
                }
                await ServicesManager.ArticlesService.UpdateArticlesToSqlite(result.Data.Data);
                currentPage++;

                if (MessageCollection.Count < result.Data.RecordCount && MessageCollection.Count < maxCountAllowedToLoadMore)
                {
                    LoadStatus = LoadMoreStatus.StausDefault;
                    CanLoadMore = true;
                    IsBusy = false;
                }
                else
                {
                    LoadStatus = LoadMoreStatus.StausEnd;
                    CanLoadMore = false;
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                CanLoadMore = false;
                IsBusy = false;
                Crashes.TrackError(ex);
            }
        }
          
        /// <summary>
        /// 能否加载更多
        /// </summary>
        /// <returns></returns>
        private bool CanLoadMoreItems()
        {
            return CanLoadMore;
        }
          
        #endregion
    }
}

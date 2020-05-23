
using FormsToolkit;
using Notes.Helpers;
using Notes.Models;
using Notes.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {
        #region 属性

        /// <summary>
        /// Task集合
        /// </summary>
        private ObservableCollection<TaskModel> collectionTask = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskModel> CollectionTask
        {
            get => collectionTask;
            set
            {
                collectionTask = value;
                RaisePropertyChanged(() => CollectionTask);
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

        public TaskListViewModel()
        {
            MessagingService.Current.Subscribe<TaskModel>(MessageKeys.TaskListKey, (m, args) =>
            { 
                CollectionTask.Add(args);  
            });
        }

        /// <summary>
        /// 初始化ViewModel
        /// </summary>
        public async void Initialize()
        {
            string refreshKey = this.GetType().Name;//根据实际需求修改
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

                IList<TaskModel> listTask = await ServicesManager.TaskService.GetAllFromSqliteAsync();

                if (listTask != null)
                {
                    CollectionTask.Clear();

                    foreach (TaskModel item in listTask)
                    {
                        CollectionTask.Add(item);
                    }

                    sw.Start();
                }

                sw.Stop();
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task RefreshDataFromAPIAsync()
        {
            try
            {
                IsBusy = true;

                //查询目录对应数量 
                var result = await ServicesManager.TaskService.GetTasks("", "", 1);

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

                CollectionTask.Clear();

                foreach (TaskModel item in result.Data.Items)
                {
                    CollectionTask.Add(item);
                }

                await ServicesManager.TaskService.BatchUpdateToSqlite(CollectionTask); //保存在sqlite
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
                string refreshKey = this.GetType().Name;//根据是实际需求修改,与Initialize方法中的refreshKey保持一致
                RefreshTimeHelper.SetRefreshTime(refreshKey, DateTime.UtcNow.AddMinutes(ConstanceHelper.NextRefreshInterval));//记录刷新时间  
                IsBusy = false;
            }
        }

        #endregion

        #region 命令 

        /// <summary>
        /// 刷新命令
        /// </summary>  
        public ICommand RefreshCommand => new Command(async () => await RefreshDataFromAPIAsync());

        #endregion
    }
}
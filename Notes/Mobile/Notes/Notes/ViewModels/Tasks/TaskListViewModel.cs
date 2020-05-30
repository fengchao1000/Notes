
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
        /// 今日Task集合
        /// </summary>
        private ObservableCollection<TaskModel> collectionTaskToday = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskModel> CollectionTaskToday
        {
            get => collectionTaskToday;
            set
            {
                collectionTaskToday = value;
                RaisePropertyChanged(() => CollectionTaskToday);
            }
        }

        /// <summary>
        /// 本月Task集合
        /// </summary>
        private ObservableCollection<TaskModel> collectionTaskMonth = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskModel> CollectionTaskMonth
        {
            get => collectionTaskMonth;
            set
            {
                collectionTaskMonth = value;
                RaisePropertyChanged(() => CollectionTaskMonth);
            }
        }

        /// <summary>
        /// 本年Task集合
        /// </summary>
        private ObservableCollection<TaskModel> collectionTaskYear = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskModel> CollectionTaskYear
        {
            get => collectionTaskYear;
            set
            {
                collectionTaskYear = value;
                RaisePropertyChanged(() => CollectionTaskYear);
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
            MessagingService.Current.Subscribe<TaskModel>(TaskType.Day.ToString(), (m, args) =>
            {
                CollectionTaskToday.Add(args);
            });

            MessagingService.Current.Subscribe<TaskModel>(TaskType.Month.ToString(), (m, args) =>
            {
                CollectionTaskMonth.Add(args);
            });

            MessagingService.Current.Subscribe<TaskModel>(TaskType.Year.ToString(), (m, args) =>
            {
                CollectionTaskYear.Add(args);
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
                    CollectionTaskToday.Clear();

                    foreach (TaskModel item in listTask)
                    {
                        CollectionTaskToday.Add(item);
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
            await RefreshTodayDataFromAPIAsync();
            await RefreshMonthDataFromAPIAsync();
            await RefreshYearDataFromAPIAsync();
        }

        public async Task RefreshTodayDataFromAPIAsync()
        {
            try
            {
                IsBusy = true;

                //本月
                var result = await ServicesManager.TaskService.GetPagedTasks(DateTime.UtcNow.Date, DateTime.UtcNow, TaskType.Day, "", "", 1);

                if (!result.IsSuccess)
                {
                    return;
                }

                if (result.Data.Items == null)
                {
                    return;
                }

                CollectionTaskToday.Clear(); 

                if (result.Data.Items.Count <= 0)
                {
                    return;
                } 

                foreach (TaskModel item in result.Data.Items)
                {
                    CollectionTaskToday.Add(item);
                }

                await ServicesManager.TaskService.BatchUpdateToSqlite(CollectionTaskToday); //保存在sqlite
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

        public async Task RefreshMonthDataFromAPIAsync()
        {
            try
            {
                IsBusy = true;

                //本月
                var result = await ServicesManager.TaskService.GetPagedTasks(DateTime.UtcNow, null, TaskType.Month, "", "", 1);

                if (!result.IsSuccess)
                {
                    return;
                }

                if (result.Data.Items == null)
                {
                    return;
                }

                CollectionTaskMonth.Clear();

                if (result.Data.Items.Count <= 0)
                {
                    return;
                } 

                foreach (TaskModel item in result.Data.Items)
                {
                    CollectionTaskMonth.Add(item);
                }

                await ServicesManager.TaskService.BatchUpdateToSqlite(CollectionTaskMonth); //保存在sqlite
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

        public async Task RefreshYearDataFromAPIAsync()
        {
            try
            {
                IsBusy = true;

                //本月
                var result = await ServicesManager.TaskService.GetPagedTasks(DateTime.UtcNow, null, TaskType.Year, "", "", 1);

                if (!result.IsSuccess)
                {
                    return;
                }

                if (result.Data.Items == null)
                {
                    return;
                }

                CollectionTaskYear.Clear();

                if (result.Data.Items.Count <= 0)
                {
                    return;
                } 

                foreach (TaskModel item in result.Data.Items)
                {
                    CollectionTaskYear.Add(item);
                }

                await ServicesManager.TaskService.BatchUpdateToSqlite(CollectionTaskYear); //保存在sqlite
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

        ///<summary>
        /// 删除确认
        ///</summary>
        public async void DeleteTask(object obj)
        {
            var taskModel = obj as TaskModel;

            if (taskModel == null)
            {
                ToastHelper.Current.SendToast("删除失败");
                return;
            }

            try
            {
                ResultData<bool> result = await ServicesManager.TaskService.DeleteTask(taskModel.Id);
                if (result.IsSuccess)
                {
                    //await ServicesManager.TaskService.DeleteFromSqliteAsync(taskModel);

                    CollectionTaskToday.Remove(taskModel);

                    CollectionTaskToday = CollectionTaskToday;

                    ToastHelper.Current.SendToast("删除成功");
                }
                else
                {
                    ToastHelper.Current.SendToast("删除失败");
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
                ToastHelper.Current.SendToast("删除失败");
            }
        }

        ///<summary>
        /// 删除确认
        ///</summary>
        public async void DeleteTaskToday(object obj)
        {
            var taskModel = obj as TaskModel;

            if (taskModel == null)
            {
                ToastHelper.Current.SendToast("删除失败");
                return;
            }

            try
            {
                ResultData<bool> result = await ServicesManager.TaskService.DeleteTask(taskModel.Id);
                if (result.IsSuccess)
                {
                    //await ServicesManager.TaskService.DeleteFromSqliteAsync(taskModel);

                    CollectionTaskToday.Remove(taskModel);

                    CollectionTaskToday = CollectionTaskToday;

                    ToastHelper.Current.SendToast("删除成功");
                }
                else
                {
                    ToastHelper.Current.SendToast("删除失败");
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
                ToastHelper.Current.SendToast("删除失败");
            }
        }

        ///<summary>
        /// 删除确认
        ///</summary>
        public async void DeleteTaskMonth(object obj)
        {
            var taskModel = obj as TaskModel;

            if (taskModel == null)
            {
                ToastHelper.Current.SendToast("删除失败");
                return;
            }

            try
            {
                ResultData<bool> result = await ServicesManager.TaskService.DeleteTask(taskModel.Id);
                if (result.IsSuccess)
                {
                    //await ServicesManager.TaskService.DeleteFromSqliteAsync(taskModel);

                    CollectionTaskMonth.Remove(taskModel);

                    CollectionTaskMonth = CollectionTaskMonth;

                    ToastHelper.Current.SendToast("删除成功");
                }
                else
                {
                    ToastHelper.Current.SendToast("删除失败");
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
                ToastHelper.Current.SendToast("删除失败");
            }
        }

        ///<summary>
        /// 删除确认
        ///</summary>
        public async void DeleteTaskYear(object obj)
        {
            var taskModel = obj as TaskModel;

            if (taskModel == null)
            {
                ToastHelper.Current.SendToast("删除失败");
                return;
            }

            try
            {
                ResultData<bool> result = await ServicesManager.TaskService.DeleteTask(taskModel.Id);
                if (result.IsSuccess)
                {
                    //await ServicesManager.TaskService.DeleteFromSqliteAsync(taskModel);

                    CollectionTaskYear.Remove(taskModel);

                    CollectionTaskYear = CollectionTaskYear;

                    ToastHelper.Current.SendToast("删除成功");
                }
                else
                {
                    ToastHelper.Current.SendToast("删除失败");
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
                ToastHelper.Current.SendToast("删除失败");
            }
        }

        #endregion

        #region 命令 

        /// <summary>
        /// 刷新命令
        /// </summary>  
        public ICommand RefreshCommand => new Command(async () => await RefreshDataFromAPIAsync());

        /// <summary>
        /// 删除命令
        /// </summary>      
        public Command<object> DeleteTaskTodayCommand => new Command<object>(DeleteTaskToday);

        /// <summary>
        /// 删除命令
        /// </summary>      
        public Command<object> DeleteTaskMonthCommand => new Command<object>(DeleteTaskMonth);

        /// <summary>
        /// 删除命令
        /// </summary>      
        public Command<object> DeleteTaskYearCommand => new Command<object>(DeleteTaskYear);

        #endregion
    }
}
using FormsToolkit;
using Notes.Helpers;
using Notes.Models;
using Notes.Services;
using Notes.Views.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels.Tasks
{ 
    public class AddUpdateTaskViewModel : BaseViewModel
    {
        #region 属性

        private TaskModel taskModel;
        public TaskModel TaskModel
        {
            get => taskModel;
            set
            {
                taskModel = value;
                RaisePropertyChanged(() => TaskModel);
            }
        }

        public Page CurrentPage { get; set; }


        /// <summary>
        /// Task 优先级集合
        /// </summary>
        private ObservableCollection<object> collectionTaskPriority = new ObservableCollection<object>();
        public ObservableCollection<object> CollectionTaskPriority
        {
            get => collectionTaskPriority;
            set
            {
                collectionTaskPriority = value;
                RaisePropertyChanged(() => CollectionTaskPriority);
            }
        }

        /// <summary>
        /// Task Type 集合
        /// </summary>
        private ObservableCollection<object> collectionTaskType = new ObservableCollection<object>();
        public ObservableCollection<object> CollectionTaskType
        {
            get => collectionTaskType;
            set
            {
                collectionTaskType = value;
                RaisePropertyChanged(() => CollectionTaskType);
            }
        }


        #endregion

        #region 方法

        public AddUpdateTaskViewModel(AddTaskPage page,TaskModel taskModel = null)
        {
            if (taskModel == null)
            {
                TaskModel = new TaskModel();
                TaskModel.StartTime = DateTime.Now;
                TaskModel.EndTime = DateTime.Now.AddHours(8);
            }
            else 
            { 
                TaskModel = taskModel; 
            }

            MessagingService.Current.Subscribe<DateTime>(MessageKeys.AddTaskStartDateKey, (m, date) =>
            {
                TaskModel.StartTime = date;
                TaskModel = TaskModel;
            });

            MessagingService.Current.Subscribe<DateTime>(MessageKeys.AddTaskEndDateKey, (m, date) =>
            {
                TaskModel.EndTime = date;
                TaskModel = TaskModel;
            });

            CurrentPage = page;

            CollectionTaskPriority = new ObservableCollection<object>();
            CollectionTaskPriority.Add(TaskPriority.Lower.ToString());
            CollectionTaskPriority.Add(TaskPriority.Ordinary.ToString());
            CollectionTaskPriority.Add(TaskPriority.emergency.ToString());
            CollectionTaskPriority.Add(TaskPriority.VeryUrgent.ToString());

            CollectionTaskType = new ObservableCollection<object>();
            CollectionTaskType.Add(TaskType.Day.ToString());
            CollectionTaskType.Add(TaskType.Month.ToString());
            CollectionTaskType.Add(TaskType.Year.ToString()); 
        }

        public async Task AddTaskAsync()
        {
            try
            {
                //参数验证
                if (TaskModel == null) 
                {
                    ToastHelper.Current.SendToast("");
                    return;
                }

                //查询目录对应数量 
                var result = await ServicesManager.TaskService.AddTask(TaskModel);

                if (!result.IsSuccess)
                {
                    ToastHelper.Current.SendToast("添加失败");
                    return;
                }

                MessagingService.Current.SendMessage(TaskModel.TaskType.ToString(), TaskModel);

                await CurrentPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString()); 
            } 
        }

        #endregion

        #region 命令 

        /// <summary>
        /// 刷新命令
        /// </summary>  
        public ICommand AddTaskCommand => new Command(async () => await AddTaskAsync());

        #endregion
    }
}

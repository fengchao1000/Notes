using FormsToolkit;
using Notes.Helpers;
using Notes.Models;
using Notes.Services;
using Notes.Views.Tasks;
using System;
using System.Collections.Generic;
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

                MessagingService.Current.SendMessage(MessageKeys.TaskListKey, TaskModel);

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

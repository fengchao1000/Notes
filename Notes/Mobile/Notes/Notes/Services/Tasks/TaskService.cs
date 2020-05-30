using Notes.Helpers;
using Notes.Interfaces.Tasks;
using Notes.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services
{
    public class TaskService : BaseService<TaskModel>, ITaskService
    {
        public async Task<ResultData<TaskModel>> AddTask(TaskModel task)
        {
            return await RequestProvider.Current.PostAsync<TaskModel>($"{AppConfig.AddTaskUrl}", task);
        }

        public async Task<ResultData<PagedResultDto<TaskModel>>> GetTasks(string skipCount, string maxResultCount, int sorting)
        {
            return await RequestProvider.Current.GetAsync<PagedResultDto<TaskModel>>($"{AppConfig.GetTaskUrl}");
        }

        public async Task<ResultData<PagedResultDto<TaskModel>>> GetPagedTasks(DateTime? startTime, DateTime? endTime, TaskType taskType, string skipCount, string maxResultCount, int sorting)
        {
            StringBuilder url = new StringBuilder($"{AppConfig.GetPagedTaskUrl}?taskType={taskType}");
            
            if (startTime.HasValue) 
            {
                url.Append($"&startTime={startTime}");
            }

            if (endTime.HasValue)
            {
                url.Append($"&startTime={endTime}");
            }

            return await RequestProvider.Current.GetAsync<PagedResultDto<TaskModel>>(url.ToString());
        }

        public async Task<ResultData<bool>> DeleteTask(Guid id)
        {
            return await RequestProvider.Current.DeleteAsync<bool>($"{AppConfig.GetTaskUrl}/{id}", false);
        }
    }
}

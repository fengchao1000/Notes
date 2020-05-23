using Notes.Helpers; 
using Notes.Interfaces.Tasks;
using Notes.Models; 
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
    }
}

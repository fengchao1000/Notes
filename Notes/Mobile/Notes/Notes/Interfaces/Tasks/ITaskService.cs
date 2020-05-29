using Notes.Models; 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces.Tasks
{ 
   public interface ITaskService : IBaseService<TaskModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        Task<ResultData<PagedResultDto<TaskModel>>> GetTasks(string skipCount, string maxResultCount, int sorting);

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="Task"></param> 
        /// <returns></returns>
        Task<ResultData<TaskModel>> AddTask(TaskModel task);

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="Task"></param> 
        /// <returns></returns>
        Task<ResultData<bool>> DeleteTask(Guid id);
    }
}

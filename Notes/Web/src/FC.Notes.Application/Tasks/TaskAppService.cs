using FC.Notes.Permissions;
using FC.Notes.Tasks.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using System.Linq;
using System.Collections.Generic;

namespace FC.Notes.Tasks
{

    public class TaskAppService : NotesAppService, ITaskAppService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger _logger;
        protected IDistributedCache<TaskDto> TaskCache { get; }

        public TaskAppService(
           ITaskRepository taskRepository,
           IDistributedCache<TaskDto> taskCache,
            ILogger<BookmarkAppService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
            TaskCache = taskCache;
        }

        public async Task<TaskDto> CreateAsync(CreateUpdateTaskDto input)
        {
            var task = new Task(
               id: GuidGenerator.Create(),
               title: input.Title,
               Remark: input.Remark
           )
            { StartTime = input.StartTime, EndTime = input.EndTime, Priority = input.Priority, Status = 0 ,TaskType = input.TaskType };

            task.TenantId = CurrentTenant.Id;
            task.CreateUserId = CurrentUser.Id ?? Guid.Empty;
            //todo:CurrentUser.Id;

            await _taskRepository.InsertAsync(task);

            return ObjectMapper.Map<Task, TaskDto>(task);
        }


        public async Task<TaskDto> GetAsync(Guid id)
        {
            var cacheKey = $"Bookmark@{id}";

            async Task<TaskDto> GetTaskAsync()
            {
                var bookmark = await _taskRepository.GetAsync(id);

                return ObjectMapper.Map<Task, TaskDto>(bookmark);
            }

            //if (Debugger.IsAttached)
            //{
            //    return await GetBookmarkAsync();
            //}

            return await TaskCache.GetOrAddAsync(
                cacheKey,
                GetTaskAsync,
                () => new DistributedCacheEntryOptions
                {
                    //TODO: Configurable?
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6),
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                }
            );

        }

        public async Task<TaskDto> UpdateAsync(Guid id, CreateUpdateTaskDto input)
        {
            var task = await _taskRepository.GetAsync(id);

            task.Title = input.Title;
            task.Remark = input.Remark;
            task.StartTime = input.StartTime;
            task.EndTime = input.EndTime;

            task = await _taskRepository.UpdateAsync(task);

            return ObjectMapper.Map<Task, TaskDto>(task);
        }

        public async System.Threading.Tasks.Task DeleteAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
        } 

        public async Task<PagedResultDto<TaskDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = _taskRepository.GetAll().WhereIf(true, q => q.IsDeleted == false);

            var tasks = query.PageBy(input).ToList();

            var totalCount = await _taskRepository.GetCountAsync();

            IReadOnlyList<TaskDto> list = new List<TaskDto>(
                            ObjectMapper.Map<List<Task>, List<TaskDto>>(tasks));

            return new PagedResultDto<TaskDto>(totalCount, list);
        }

        public async Task<PagedResultDto<TaskDto>> GetPagedAsync(GetPagedTasksInputDto input)
        {
            var query = _taskRepository.GetAll().Where(q => q.TaskType == input.TaskType && q.IsDeleted == false);

            if (input.TaskType == TaskType.Day)
            {
                query = query.WhereIf(input.StartTime.HasValue,q => q.StartTime == input.StartTime)
                             .WhereIf(input.EndTime.HasValue, q => q.EndTime == input.EndTime);
            }

            if (input.TaskType == TaskType.Month)
            {
                query = query.WhereIf(input.StartTime.HasValue, q => q.StartTime.Year == input.StartTime.Value.Year && q.StartTime.Month == input.StartTime.Value.Month);
            }

            if (input.TaskType == TaskType.Year)
            {
                query = query.WhereIf(input.StartTime.HasValue, q => q.StartTime.Year == input.StartTime.Value.Year );
            }

            query = query.OrderBy(t => t.StartTime);

            var tasks = query.PageBy(input).ToList();

            var totalCount = query.Count();

            IReadOnlyList<TaskDto> list = new List<TaskDto>(
                            ObjectMapper.Map<List<Task>, List<TaskDto>>(tasks));

            return new PagedResultDto<TaskDto>(totalCount, list);
        }
    }


    //[Authorize(NotesPermissions.Notes.Default)]
    //    public class TaskAppService :
    //AsyncCrudAppService<Task, TaskDto, Guid, PagedAndSortedResultRequestDto,
    //    CreateUpdateTaskDto, CreateUpdateTaskDto>,
    //ITaskAppService
    //    {
    //        public TaskAppService(IRepository<Task, Guid> repository)
    //            : base(repository)
    //        {

    //        }
    //    }

}

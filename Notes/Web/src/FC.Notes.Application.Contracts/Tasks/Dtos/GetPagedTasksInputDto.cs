using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FC.Notes.Tasks.Dtos
{
    public class GetPagedTasksInputDto : PagedAndSortedResultRequestDto
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public TaskType TaskType { get; set; } = TaskType.Day;
    }
}

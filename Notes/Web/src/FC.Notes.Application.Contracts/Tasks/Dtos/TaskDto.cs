using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FC.Notes.Tasks.Dtos
{ 
    public class TaskDto : AuditedEntityDto<Guid>
    {
      
        public string Title { get; set; }

        public string Remark { get; set; }

        public TaskPriority Priority { get; set; }

        public int Status { get; set; }

        public Guid? TenantId { get; set; }

        public Guid CreateUserId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}

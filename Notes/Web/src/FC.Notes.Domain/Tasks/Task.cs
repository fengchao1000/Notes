using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace FC.Notes.Tasks
{
    public class Task : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public string Title { get; set; }

        public string Remark { get; set; }

        public TaskPriority Priority { get; set; }

        public TaskType TaskType { get; set; }

        public int Status { get; set; }

        public Guid? TenantId { get; set; }

        public Guid CreateUserId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        protected Task()
        {

        }

        public Task(Guid id, [NotNull] string title, [NotNull] string Remark)
        {
            Id = id;
            Title = Check.NotNullOrWhiteSpace(title, nameof(title));
            Remark = Remark; 
        }

    }
}

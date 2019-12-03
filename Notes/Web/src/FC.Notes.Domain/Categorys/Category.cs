using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace FC.Notes.Categorys
{

    public class Category : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {

        public virtual string Name { get; protected set; } 
        public virtual string Description { get; protected set; } 
        public virtual int ReadCount { get; protected internal set; }
        public virtual int UsageCount { get; protected internal set; }
        public Guid? TenantId { get; set; }
        protected Category()
        {

        }

        public Category([NotNull] string name, int usageCount = 0,int readCount = 0,string description = null)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
            Description = description;
            ReadCount = readCount;
            UsageCount = usageCount;
        }
        public virtual void IncreaseReadCount(int number = 1)
        {
            ReadCount += number;
        }

        public virtual void DecreaseReadCount(int number = 1)
        {
            if (ReadCount <= 0)
            {
                return;
            }

            if (ReadCount - number <= 0)
            {
                ReadCount = 0;
                return;
            }

            ReadCount -= number;
        }

        public virtual void IncreaseUsageCount(int number = 1)
        {
            UsageCount += number;
        }

        public virtual void DecreaseUsageCount(int number = 1)
        {
            if (UsageCount <= 0)
            {
                return;
            }

            if (UsageCount - number <= 0)
            {
                UsageCount = 0;
                return;
            }

            UsageCount -= number;
        }

    }
}

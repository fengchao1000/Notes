using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace FC.Notes.Categorys
{

    public class Category : FullAuditedAggregateRoot<Guid>
    {

        public virtual string Name { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual int UsageCount { get; protected internal set; }

        protected Category()
        {

        }

        public Category([NotNull] string name, int usageCount = 0, string description = null)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
            Description = description;
            UsageCount = usageCount;
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

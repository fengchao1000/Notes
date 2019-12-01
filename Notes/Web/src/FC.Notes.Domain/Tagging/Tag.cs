﻿using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace FC.Notes.Tagging
{
    public class Tag : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid BookmarkId { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual int UsageCount { get; protected internal set; }
        public Guid? TenantId { get; set; }
        protected Tag()
        {

        }

        public Tag(Guid bookmarkId, [NotNull] string name, int usageCount = 0, string description = null)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
            BookmarkId = bookmarkId;
            Description = description;
            UsageCount = usageCount;
        }

        public virtual void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
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

        public virtual void SetDescription(string description)
        {
            Description = description;
        }
    }
}

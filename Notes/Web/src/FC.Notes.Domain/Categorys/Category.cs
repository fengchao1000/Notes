using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace FC.Notes.Categorys
{

    public class Category : FullAuditedAggregateRoot<Guid>
    {

        public virtual string Name { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual int UsageCount { get; protected internal set; }
    }
}

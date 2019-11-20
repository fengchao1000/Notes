using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FC.Notes.Categorys.Dtos
{ 
     public class CategoryDto : AuditedEntityDto<Guid>
    { 
        public virtual string Name { get; protected set; }
         
        public virtual string Description { get; protected set; }

        public virtual int UsageCount { get; protected internal set; }
    }
}

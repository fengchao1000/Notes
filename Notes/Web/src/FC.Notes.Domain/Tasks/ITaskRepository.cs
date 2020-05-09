using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace FC.Notes.Tasks
{ 
    public interface ITaskRepository : IBasicRepository<Task, Guid>
    {
        IQueryable<Task> GetAll();
    }
}

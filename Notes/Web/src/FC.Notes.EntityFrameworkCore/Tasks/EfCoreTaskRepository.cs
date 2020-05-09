using FC.Notes.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace FC.Notes.Tasks
{
    public class EfCoreTaskRepository : EfCoreRepository<INotesDbContext, Task, Guid>, ITaskRepository
    {
        public EfCoreTaskRepository(IDbContextProvider<INotesDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
           
        }

        public IQueryable<Task> GetAll()
        {
            return GetQueryable();
        }
    }
}

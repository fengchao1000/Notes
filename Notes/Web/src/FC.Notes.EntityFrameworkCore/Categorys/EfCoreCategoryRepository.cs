using FC.Notes.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace FC.Notes.Categorys
{
    public class EfCoreCategoryRepository : EfCoreRepository<INotesDbContext, Category, Guid>, ICategoryRepository
    {
        public EfCoreCategoryRepository(IDbContextProvider<INotesDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}

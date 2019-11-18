using FC.Notes.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace FC.Notes.Bookmarks
{ 
    public class EfCoreBookmarkRepository : EfCoreRepository<INotesDbContext, Bookmark, Guid>, IBookmarkRepository
    {
        public EfCoreBookmarkRepository(IDbContextProvider<INotesDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public override IQueryable<Bookmark> WithDetails()
        {
            return GetQueryable().IncludeDetails();
        }
    }
}

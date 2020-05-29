using FC.Notes.Categorys;
using FC.Notes.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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

        public IQueryable<Bookmark> GetAll()
        {
             return GetQueryable().IncludeBookmarkDetails(); 
        }

        public override IQueryable<Bookmark> WithDetails()
        {
            return GetQueryable().IncludeBookmarkDetails();
        }
    }
}

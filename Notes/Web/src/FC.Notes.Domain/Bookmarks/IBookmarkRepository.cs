using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace FC.Notes.Bookmarks
{ 
    public interface IBookmarkRepository : IBasicRepository<Bookmark, Guid>
    { 

    }
}

using FC.Notes.Bookmarks;
using FC.Notes.Bookmarks.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FC.Notes
{ 
    public class BookmarkAppService :
        AsyncCrudAppService<Bookmark, BookmarkDto, Guid, PagedAndSortedResultRequestDto,
            CreateUpdateBookmarkDto, CreateUpdateBookmarkDto>,
        IBookmarkAppService
    {
        public BookmarkAppService(IRepository<Bookmark, Guid> repository)
            : base(repository)
        {

        }
    }
}

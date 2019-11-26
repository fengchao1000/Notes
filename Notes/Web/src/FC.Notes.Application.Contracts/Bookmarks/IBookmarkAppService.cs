using FC.Notes.Bookmarks.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FC.Notes.Bookmarks
{ 
    public interface IBookmarkAppService :
        IAsyncCrudAppService< //Defines CRUD methods
            BookmarkDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting on getting a list of books
            CreateUpdateBookmarkDto, //Used to create a new book
            CreateUpdateBookmarkDto> //Used to update a book
    {
        Task<PagedResultDto<BookmarkDto>> GetPagedAsync(GetPagedBookmarkInputDto input);
    }
}

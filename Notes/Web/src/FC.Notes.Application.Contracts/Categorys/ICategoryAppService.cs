using FC.Notes.Bookmarks.Dtos;
using FC.Notes.Categorys.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FC.Notes.Categorys
{  
    public interface ICategoryAppService :
        IAsyncCrudAppService< //Defines CRUD methods
            CategoryDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting on getting a list of books
            CreateUpdateCategoryDto, //Used to create a new book
            CreateUpdateCategoryDto> //Used to update a book
    {

    }
}

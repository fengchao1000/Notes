using FC.Notes.Tasks.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FC.Notes.Tasks
{ 
    public interface ITaskAppService :
        IAsyncCrudAppService< //Defines CRUD methods
            TaskDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting on getting a list of books
            CreateUpdateTaskDto, //Used to create a new book
            CreateUpdateTaskDto> //Used to update a book
    {

    }
}

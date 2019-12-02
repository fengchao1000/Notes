using FC.Notes.Categorys;
using FC.Notes.Categorys.Dtos;
using FC.Notes.Permissions;
using Microsoft.AspNetCore.Authorization;
using System; 
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FC.Notes
{
    //[Authorize(NotesPermissions.Notes.Default)]
    public class CategoryAppService :
    AsyncCrudAppService<Category, CategoryDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateCategoryDto, CreateUpdateCategoryDto>,
    ICategoryAppService
    {
        public CategoryAppService(IRepository<Category, Guid> repository)
            : base(repository)
        {

        }
    }


}

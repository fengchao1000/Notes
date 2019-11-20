using FC.Notes.Bookmarks;
using FC.Notes.Bookmarks.Dtos;
using FC.Notes.Categorys;
using FC.Notes.Categorys.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FC.Notes
{  
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

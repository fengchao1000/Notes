using Notes.Helpers;
using Notes.Interfaces.Categorys;
using Notes.Models;
using Notes.Models.Categorys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Categorys
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public async Task<ResultData<PagedResultDto<Category>>> GetCategory(string skipCount, string maxResultCount, int sorting)
        {
            return await RequestProvider.Current.GetAsync<PagedResultDto<Category>>($"{AppConfig.GetCategoryUrl}");
        }
    }
}

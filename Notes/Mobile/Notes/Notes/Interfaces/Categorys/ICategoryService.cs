using Notes.Models;
using Notes.Models.Categorys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces.Categorys
{
    public interface ICategoryService : IBaseService<Category>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        Task<ResultData<PagedResultDto<Category>>> GetCategorys(string skipCount, string maxResultCount, int sorting);
    }
}

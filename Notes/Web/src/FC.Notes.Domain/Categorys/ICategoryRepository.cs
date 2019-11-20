using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace FC.Notes.Categorys
{
    public interface ICategoryRepository : IBasicRepository<Category, Guid>
    { 

    }
}

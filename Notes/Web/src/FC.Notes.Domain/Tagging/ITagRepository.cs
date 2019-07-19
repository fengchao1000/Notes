﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace FC.Notes.Tagging
{
    public interface ITagRepository : IBasicRepository<Tag, Guid>
    {
        Task<List<Tag>> GetListAsync(Guid blogId);

        Task<Tag> GetByNameAsync(Guid blogId, string name);

        Task<Tag> FindByNameAsync(Guid blogId, string name);

        Task<List<Tag>> GetListAsync(IEnumerable<Guid> ids);

        void DecreaseUsageCountOfTags(List<Guid> id);
    }
}

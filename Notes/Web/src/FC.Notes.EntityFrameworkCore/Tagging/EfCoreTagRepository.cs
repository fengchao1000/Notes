using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FC.Notes.EntityFrameworkCore;
using FC.Notes.Tagging;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore; 

namespace Volo.Blogging.Tagging
{
    public class EfCoreTagRepository : EfCoreRepository<INotesDbContext, Tag, Guid>, ITagRepository
    {
        public EfCoreTagRepository(IDbContextProvider<INotesDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Tag>> GetListAsync(Guid id)
        {
            return await DbSet.Where(t=>t.BookmarkId == id).ToListAsync();
        }

        public async Task<Tag> GetByNameAsync(Guid id, string name)
        {
            return await DbSet.FirstAsync(t=> t.Id == id && t.Name == name);
        }

        public async Task<Tag> FindByNameAsync(Guid id, string name)
        {
            return await DbSet.FirstOrDefaultAsync(t => t.Id == id && t.Name == name);
        }

        public async Task<List<Tag>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(t => ids.Contains(t.Id)).ToListAsync();
        }

        public void DecreaseUsageCountOfTags(List<Guid> ids)
        {
            var tags = DbSet.Where(t => ids.Any(id => id == t.Id));

            foreach (var tag in tags)
            {
                tag.DecreaseUsageCount();
            }
        }
    }
}

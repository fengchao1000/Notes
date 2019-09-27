using FC.Notes.Tagging;
using FC.Notes.Tagging.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace FC.Notes
{
    public class TagAppService : ApplicationService, ITagAppService
    {
        private readonly ITagRepository _tagRepository;

        public TagAppService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<List<TagDto>> GetPopularTags(Guid blogId, GetPopularTagsInput input)
        {
            var postTags = (await _tagRepository.GetListAsync(blogId)).OrderByDescending(t => t.UsageCount)
                .WhereIf(input.MinimumPostCount != null, t => t.UsageCount >= input.MinimumPostCount)
                .Take(input.ResultCount).ToList();

            return new List<TagDto>(
                ObjectMapper.Map<List<Tag>, List<TagDto>>(postTags));
        }  
    }
}

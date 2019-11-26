using FC.Notes.Bookmarks;
using FC.Notes.Bookmarks.Dtos;
using FC.Notes.Categorys;
using FC.Notes.Tagging;
using FC.Notes.Tagging.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain.Repositories;

namespace FC.Notes
{
    public class BookmarkAppService : NotesAppService, IBookmarkAppService
    {
        private readonly IBookmarkRepository _bookmarkRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BookmarkAppService(IRepository<Bookmark, Guid> repository, IBookmarkRepository bookmarkRepository, ITagRepository tagRepository, ICategoryRepository categoryRepository)
        {
            _bookmarkRepository = bookmarkRepository;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
        }
         
        public async Task<PagedResultDto<BookmarkDto>> GetPagedAsync(GetPagedBookmarkInputDto input)
        {
            //初步过滤
            var query = _bookmarkRepository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrEmpty(), t => t.Content.Contains(input.Keyword))
                .WhereIf(!input.CategoryId.HasValue, t => t.Categorys.Any(c=> c.CategoryId == input.CategoryId)).OrderByDescending(t => t.CreationTime); 
            
            //排序
            //query = !string.IsNullOrEmpty(input.Sorting) ? query.OrderBy(input.Sorting) : query.OrderByDescending(t => t.CreationTime);

            //获取总数
            var totalCount = query.Count();
            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var bookmarks = query.PageBy(input).ToList();

            IReadOnlyList<BookmarkDto> list = new List<BookmarkDto>(
                            ObjectMapper.Map<List<Bookmark>, List<BookmarkDto>>(bookmarks));
              
            return new PagedResultDto<BookmarkDto>(totalCount, list);
        }

        public async Task<PagedResultDto<BookmarkDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var bookmarks = await _bookmarkRepository.GetListAsync();

            var totalCount = await _bookmarkRepository.GetCountAsync();

            IReadOnlyList<BookmarkDto> list = new List<BookmarkDto>(
                            ObjectMapper.Map<List<Bookmark>, List<BookmarkDto>>(bookmarks));

            return new PagedResultDto<BookmarkDto>(totalCount, list);
        }

        public async Task<BookmarkDto> CreateAsync(CreateUpdateBookmarkDto input)
        {
            var bookmark = new Bookmark(
                id: GuidGenerator.Create(),
                title: input.Title,
                linkUrl: input.LinkUrl
            )
            { Content = input.Content, Summary = input.Summary };

            await _bookmarkRepository.InsertAsync(bookmark);

            var tagList = SplitTags(input.Tags);
            await SaveTags(tagList, bookmark);

            var categoryList = SplitCategory(input.Categorys);
            await SaveCategory(categoryList, bookmark);

            return ObjectMapper.Map<Bookmark, BookmarkDto>(bookmark);
        }

        private List<Guid> SplitCategory(string catetorys)
        {
            if (catetorys.IsNullOrWhiteSpace())
            {
                return new List<Guid>();
            }
            List<Guid> catetoryIDs = new List<Guid>();
            foreach (string item in catetorys.Split(",").Select(t => t.Trim()))
            {
                catetoryIDs.Add(new Guid(item));
            }
            return catetoryIDs;
        }

        private async Task SaveCategory(List<Guid> newCategoryIDs, Bookmark bookmark)
        {
            //删除旧的未选择的分类
            foreach (var oldCategory in bookmark.Categorys)
            {
                var category = await _categoryRepository.GetAsync(oldCategory.CategoryId);

                //旧的分类在新分类中是否存在
                var oldCategoryNameInNewCategorys = newCategoryIDs.FirstOrDefault(t => t == category.Id);

                if (oldCategoryNameInNewCategorys == null)
                {
                    bookmark.RemoveCategory(oldCategory.CategoryId);

                    category.DecreaseUsageCount();

                    await _categoryRepository.UpdateAsync(category);
                }
                else
                {
                    newCategoryIDs.Remove(oldCategoryNameInNewCategorys);
                     
                }
            }
            
            //添加新选中的分类
            foreach (var newCategoryID in newCategoryIDs)
            {
                var category = await _categoryRepository.GetAsync(newCategoryID);

                bookmark.AddCategory(newCategoryID);

                category.IncreaseUsageCount();

                await _categoryRepository.UpdateAsync(category);
            }
        }


        private List<string> SplitTags(string tags)
        {
            if (tags.IsNullOrWhiteSpace())
            {
                return new List<string>();
            }
            return new List<string>(tags.Split(",").Select(t => t.Trim()));
        }

        private async Task SaveTags(List<String> newTags, Bookmark bookmark)
        {
            await RemoveOldTags(newTags, bookmark);

            await AddNewTags(newTags, bookmark);
        }


        private async Task RemoveOldTags(List<string> newTags, Bookmark bookmark)
        {
            foreach (var oldTag in bookmark.Tags)
            {
                var tag = await _tagRepository.GetAsync(oldTag.TagId);

                var oldTagNameInNewTags = newTags.FirstOrDefault(t => t == tag.Name);

                if (oldTagNameInNewTags == null)
                {
                    //bookmark.RemoveTag(oldTag.TagId);

                    tag.DecreaseUsageCount();

                    await _tagRepository.UpdateAsync(tag);
                }
                else
                {
                    newTags.Remove(oldTagNameInNewTags);
                }
            }
        }

        private async Task AddNewTags(List<string> newTags, Bookmark bookmark)
        {
            var tags = await _tagRepository.GetListAsync(bookmark.Id);

            foreach (var newTag in newTags)
            {
                var tag = tags.FirstOrDefault(t => t.Name == newTag);

                if (tag == null)
                {
                    tag = await _tagRepository.InsertAsync(new Tag(bookmark.Id, newTag, 1));
                }
                else
                {
                    tag.IncreaseUsageCount();

                    tag = await _tagRepository.UpdateAsync(tag);
                }

                bookmark.AddTag(tag.Id);
            }
        }

        public async Task<BookmarkDto> GetAsync(Guid id)
        {
            var bookmark = await _bookmarkRepository.GetAsync(id);

            return ObjectMapper.Map<Bookmark, BookmarkDto>(bookmark);
        }

        public async Task<BookmarkDto> UpdateAsync(Guid id, CreateUpdateBookmarkDto input)
        {
            var bookmark = await _bookmarkRepository.GetAsync(id);

            //await AuthorizationService.CheckAsync(post, CommonOperations.Update);

            bookmark.Title = input.Title;
            bookmark.LinkUrl = input.LinkUrl;
            bookmark.Content = input.Content;
            bookmark.Summary = input.Summary;

            bookmark = await _bookmarkRepository.UpdateAsync(bookmark);

            var tagList = SplitTags(input.Tags);

            await SaveTags(tagList, bookmark);

            return ObjectMapper.Map<Bookmark, BookmarkDto>(bookmark);
        }

        public async Task DeleteAsync(Guid id)
        {
            var post = await _bookmarkRepository.GetAsync(id);

            var tags = await GetTagsOfPost(id);

            if (tags != null)
            {
                _tagRepository.DecreaseUsageCountOfTags(tags.Select(t => t.Id).ToList());
            }

            await _bookmarkRepository.DeleteAsync(id);
        }

        private async Task<List<TagDto>> GetTagsOfPost(Guid id)
        {
            var tagIds = (await _bookmarkRepository.GetAsync(id)).Tags;

            if (tagIds == null)
            {
                return null;
            }

            var tags = await _tagRepository.GetListAsync(tagIds.Select(t => t.TagId));

            return ObjectMapper.Map<List<Tag>, List<TagDto>>(tags);
        }
    }
}

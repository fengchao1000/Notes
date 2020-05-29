using AutoMapper;
using FC.Notes.Bookmarks;
using FC.Notes.Bookmarks.Dtos;
using FC.Notes.Categorys;
using FC.Notes.Categorys.Dtos;
using FC.Notes.Tagging;
using FC.Notes.Tagging.Dtos;
using FC.Notes.Tasks;
using FC.Notes.Tasks.Dtos; 
using Volo.Abp.AutoMapper;

namespace FC.Notes
{
    public class NotesApplicationAutoMapperProfile : Profile
    {
        public NotesApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Bookmark, BookmarkDto>().Ignore(x => x.Tags);
            CreateMap< BookmarkDto, Bookmark> ().Ignore(x => x.Tags);
            CreateMap<BookmarkCategoryDto, BookmarkCategory>();
            CreateMap<BookmarkCategory, BookmarkCategoryDto>();
            CreateMap<TagDto, Tag>();
            CreateMap<Tag, TagDto>();
            CreateMap<CreateUpdateBookmarkDto, Bookmark>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CreateUpdateCategoryDto, Category>(); 

            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task>();
            CreateMap<CreateUpdateTaskDto, Task>();
        }
    }
}

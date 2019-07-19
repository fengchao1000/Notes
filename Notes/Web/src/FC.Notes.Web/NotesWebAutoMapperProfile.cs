using AutoMapper;
using FC.Notes.Bookmarks.Dtos;

namespace FC.Notes.Web
{
    public class NotesWebAutoMapperProfile : Profile
    {
        public NotesWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.
            CreateMap<BookmarkDto, CreateUpdateBookmarkDto>();
        }
    }
}

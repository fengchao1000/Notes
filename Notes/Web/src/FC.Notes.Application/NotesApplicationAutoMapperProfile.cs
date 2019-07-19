using AutoMapper;
using FC.Notes.Bookmarks;
using FC.Notes.Bookmarks.Dtos;

namespace FC.Notes
{
    public class NotesApplicationAutoMapperProfile : Profile
    {
        public NotesApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Bookmark, BookmarkDto>();
            CreateMap<CreateUpdateBookmarkDto, Bookmark>();
        }
    }
}

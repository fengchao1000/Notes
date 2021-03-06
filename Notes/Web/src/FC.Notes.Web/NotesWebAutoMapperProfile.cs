﻿using AutoMapper;
using FC.Notes.Bookmarks.Dtos;
using FC.Notes.Categorys.Dtos;
using FC.Notes.Web.Pages.Bookmarks;

namespace FC.Notes.Web
{
    public class NotesWebAutoMapperProfile : Profile
    {
        public NotesWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.
            CreateMap<BookmarkDto, CreateUpdateBookmarkDto>();
            CreateMap<CreateModalModel.CreateBookmarkViewModel, CreateUpdateBookmarkDto>();
            CreateMap<EditModalModel.UpdateBookmarkViewModel, CreateUpdateBookmarkDto>();
            CreateMap<BookmarkDto, EditModalModel.UpdateBookmarkViewModel>();

            CreateMap<CategoryDto, CreateUpdateCategoryDto>(); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FC.Notes.Bookmarks;
using FC.Notes.Bookmarks.Dtos;
using FC.Notes.Categorys;
using FC.Notes.Categorys.Dtos;
using FC.Notes.Posts;
using FC.Notes.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace FC.Notes.Web.Pages.Bookmarks
{
    public class EditModalModel : NotesPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdateBookmarkViewModel Bookmark { get; set; }

        private readonly IBookmarkAppService _bookAppService;
        private readonly ICategoryAppService _categoryAppService;
        public List<SelectListItem> CategorysItem { get; set; }
            = new List<SelectListItem>();

        public EditModalModel(IBookmarkAppService bookAppService, ICategoryAppService categoryAppService)
        {
            _bookAppService = bookAppService;
            _categoryAppService = categoryAppService;

            PagedAndSortedResultRequestDto input = new PagedAndSortedResultRequestDto();
            PagedResultDto<CategoryDto> categoryDtoPaged = _categoryAppService.GetListAsync(input).Result;
            foreach (CategoryDto item in categoryDtoPaged.Items)
            {
                CategorysItem.Add(new SelectListItem(item.Name, item.Id.ToString()));
            }
        }

        public async Task OnGetAsync()
        {
            var bookDto = await _bookAppService.GetAsync(Id);
            Bookmark = ObjectMapper.Map<BookmarkDto, UpdateBookmarkViewModel>(bookDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Bookmark != null)
            {
                var dto = GetBookmarkAsDto();
                await _bookAppService.UpdateAsync(Id, dto);
            } 
            return NoContent();
        }

        public CreateUpdateBookmarkDto GetBookmarkAsDto()
        {
            var dto = ObjectMapper.Map<UpdateBookmarkViewModel, CreateUpdateBookmarkDto>(Bookmark);

            return dto;
        }

        public class UpdateBookmarkViewModel
        {
            [Required]
            [StringLength(BookmarkConsts.MaxTitleLength)]
            public string Title { get; set; }
            public string Summary { get; set; }
            [Required]
            [StringLength(BookmarkConsts.MaxUrlLength)]
            public string LinkUrl { get; set; }

            public LinkSourceType LinkSourceType { get; set; }
            [StringLength(BookmarkConsts.MaxContentLength)]
            public string Content { get; set; }
            public string Tags { get; set; }

            [Required]
            [SelectItems(nameof(CategorysItem))]
            public string Categorys { get; set; }
        }
    }
}
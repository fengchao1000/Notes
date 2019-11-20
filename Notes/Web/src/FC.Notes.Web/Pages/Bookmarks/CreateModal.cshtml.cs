using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FC.Notes.Bookmarks;
using FC.Notes.Bookmarks.Dtos;
using FC.Notes.Categorys;
using FC.Notes.Categorys.Dtos;
using FC.Notes.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace FC.Notes.Web.Pages.Bookmarks
{
    public class CreateModalModel : NotesPageModel
    {
        [BindProperty]
        public CreateBookmarkViewModel Bookmark { get; set; }

        private readonly IBookmarkAppService _bookAppService;

        private readonly ICategoryAppService _categoryAppService;

        public List<SelectListItem> CategorysItem { get; set; }
            = new List<SelectListItem> ();

        public CreateModalModel(IBookmarkAppService bookAppService, ICategoryAppService categoryAppService)
        {
            _bookAppService = bookAppService;
            _categoryAppService = categoryAppService;

            PagedAndSortedResultRequestDto input = new PagedAndSortedResultRequestDto();
            PagedResultDto<CategoryDto> categoryDtoPaged = _categoryAppService.GetListAsync(input).Result;
            foreach(CategoryDto item in categoryDtoPaged.Items) 
            {
                CategorysItem.Add(new SelectListItem (item.Name , item.Id.ToString()));
            }
           
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Bookmark != null)
            {
                var dto = GetGithubProjectAsDto();
                await _bookAppService.CreateAsync(dto);
            }
            return NoContent();
        }

        public CreateUpdateBookmarkDto GetGithubProjectAsDto()
        {
            var dto = ObjectMapper.Map<CreateBookmarkViewModel, CreateUpdateBookmarkDto>(Bookmark);

            return dto;
        }

        public class CreateBookmarkViewModel
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
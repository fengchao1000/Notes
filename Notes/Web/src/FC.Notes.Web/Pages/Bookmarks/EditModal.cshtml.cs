using System;
using System.Threading.Tasks;
using FC.Notes.Bookmarks;
using FC.Notes.Bookmarks.Dtos;
using FC.Notes.Web.Pages;
using Microsoft.AspNetCore.Mvc;

namespace FC.Notes.Web.Pages.Bookmarks
{
    public class EditModalModel : NotesPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateBookmarkDto Bookmark { get; set; }

        private readonly IBookmarkAppService _bookAppService;

        public EditModalModel(IBookmarkAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task OnGetAsync()
        {
            var bookDto = await _bookAppService.GetAsync(Id);
            Bookmark = ObjectMapper.Map<BookmarkDto, CreateUpdateBookmarkDto>(bookDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.UpdateAsync(Id, Bookmark);
            return NoContent();
        }
    }
}
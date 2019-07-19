using System.Threading.Tasks;
using FC.Notes.Bookmarks;
using FC.Notes.Bookmarks.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FC.Notes.Web.Pages.Bookmarks
{
    public class CreateModalModel : NotesPageModel
    {
        [BindProperty]
        public CreateUpdateBookmarkDto Bookmark { get; set; }

        private readonly IBookmarkAppService _bookAppService;

        public CreateModalModel(IBookmarkAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.CreateAsync(Bookmark);
            return NoContent();
        }
    }
}
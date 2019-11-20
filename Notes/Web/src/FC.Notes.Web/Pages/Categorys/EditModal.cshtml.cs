using System;
using System.Threading.Tasks;
using FC.Notes.Categorys;
using FC.Notes.Categorys.Dtos;
using FC.Notes.Web.Pages;
using Microsoft.AspNetCore.Mvc;

namespace FC.Notes.Web.Pages.Categorys
{
    public class EditModalModel : NotesPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateCategoryDto Category { get; set; }

        private readonly ICategoryAppService _bookAppService;

        public EditModalModel(ICategoryAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task OnGetAsync()
        {
            var bookDto = await _bookAppService.GetAsync(Id);
            Category = ObjectMapper.Map<CategoryDto, CreateUpdateCategoryDto>(bookDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.UpdateAsync(Id, Category);
            return NoContent();
        }
    }
}
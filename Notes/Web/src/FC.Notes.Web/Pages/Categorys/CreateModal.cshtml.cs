using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FC.Notes.Categorys;
using FC.Notes.Categorys.Dtos;
using FC.Notes.Categorys.Dtos;
using FC.Notes.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace FC.Notes.Web.Pages.Categorys
{
    public class CreateModalModel : NotesPageModel
    {
        [BindProperty]
        public CreateUpdateCategoryDto Category { get; set; }

        private readonly ICategoryAppService _bookAppService;

        public CreateModalModel(ICategoryAppService bookAppService)
        {
            _bookAppService = bookAppService; 
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var test = Category;

            await _bookAppService.CreateAsync(Category);

            return NoContent();
        }
         
      
    }
    
    
}
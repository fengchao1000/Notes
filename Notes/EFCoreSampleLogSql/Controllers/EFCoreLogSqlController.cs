using EFCoreSampleLogSql.DBModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreSampleLogSql.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class EFCoreLogSqlController : ControllerBase
    { 
        [HttpGet]
        public IActionResult Get()
        {
            using (var context = new NotesContext())
            {
                BookmarkCategory bookmarkCategory = new BookmarkCategory();
                bookmarkCategory.CategoryId = new Guid("c32278c5-c7b9-d937-8372-39f1b96918f6");

                Bookmark bookmark = new Bookmark();
                bookmark.Id = Guid.NewGuid();
                bookmark.Title = "批量测试";
                bookmark.LinkUrl = "批量测试";
                bookmark.Summary = "批量测试";
                bookmarkCategory.BookmarkId = bookmark.Id;
                bookmark.Categorys.Add(bookmarkCategory);

                context.Bookmarks.Add(bookmark);

                context.SaveChanges();
            }

            return Ok();
        }
    }
}

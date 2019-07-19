using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace FC.Notes.Pages
{
    public class Index_Tests : NotesWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}

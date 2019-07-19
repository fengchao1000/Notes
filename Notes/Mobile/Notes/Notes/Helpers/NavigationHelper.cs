using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notes.Helpers
{
    public static class NavigationHelper
    {
        static bool navigating;
        public static async Task PushAsync(INavigation navigation, Page page, bool animate = false)
        {
            if (navigating)
                return;

            navigating = true;
            await navigation.PushAsync(page, animate);
            navigating = false;
        }

        public static async Task PushModalAsync(INavigation navigation, Page page, bool animate = false)
        {
            if (navigating)
                return;

            navigating = true;
            await navigation.PushModalAsync(page, animate);
            navigating = false;
        }
    }
}

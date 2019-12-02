using Newtonsoft.Json;
using Notes.Models.Bookmarks;
using Notes.ViewModels.Bookmarks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugin.WebView.Abstractions.Enumerations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views.Bookmarks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookmarkDetailPage : ContentPage
    {
        BookmarkDetailViewModel viewModel;

        Bookmark oldBookmark;

        public BookmarkDetailPage(Bookmark bookmark)
        {
            InitializeComponent();

            BindingContext = viewModel = new BookmarkDetailViewModel(bookmark);

            oldBookmark = bookmark;

            //根据Bookmark来源以不同的形式展示，如果是cnblogs文章则获取body显示
            switch (bookmark.LinkSourceType)
            {
                case Helpers.LinkSourceType.Cnblogs:
                    ShowCnblogsBookmark();
                    break;
                case Helpers.LinkSourceType.Jianshu:
                    break;
                case Helpers.LinkSourceType.CSDN:
                    break;
                case Helpers.LinkSourceType.Other:
                    break;
                default:
                    break;
            }

        }

        private async void ShowCnblogsBookmark() 
        {
            //https://www.cnblogs.com/ZhangZiSheng001/p/11917301.html

            Bookmark newBookmark = await viewModel.GetBookmarkAsync();

            if (newBookmark == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(newBookmark.Content))
            {
                formsWebView.ContentType = WebViewContentType.Internet;
                formsWebView.Source = newBookmark.LinkUrl;
            }
            else 
            {
                formsWebView.ContentType = WebViewContentType.LocalFile;
                formsWebView.Source = "test.html";

                formsWebView.OnContentLoaded += delegate (object sender, EventArgs e)
                {
                    var model = JsonConvert.SerializeObject(newBookmark);
                    formsWebView.InjectJavascriptAsync("updateModel(" + model + ");");
                };
            } 
        }
         

        private void ShowJianshuBookmark(Bookmark bookmark)
        {
            formsWebView.ContentType = WebViewContentType.Internet;
            formsWebView.Source = oldBookmark.LinkUrl;
        }

        private void ShowCSDNBookmark(Bookmark bookmark)
        {
            formsWebView.ContentType = WebViewContentType.Internet;
            formsWebView.Source = oldBookmark.LinkUrl;
        }

        private void ShowOtherBookmark(Bookmark bookmark)
        {
            formsWebView.ContentType = WebViewContentType.Internet;
            formsWebView.Source = oldBookmark.LinkUrl;
        }

    }
}
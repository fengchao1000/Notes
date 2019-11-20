
using Notes.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Notes.Interfaces;
using Notes.Helpers;
using Notes.Interfaces.Articles;
using Notes.Services.Articles;
using Notes.Interfaces.Categorys;
using Notes.Services.Categorys;
using Notes.Services.Bookmarks;
using Notes.Interfaces.Bookmarks;

namespace Notes.Services
{
    public class ServicesManager
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        public static void Initialize()
        {
            // Services   
            if (AppConfig.IsUseTheMockService)
            {
                DependencyService.Register<IArticlesService, ArticlesMockService>();
            }
            else
            {
                DependencyService.Register<IArticlesService, ArticlesMockService>();
                DependencyService.Register<ICategoryService, CategoryService>();
                DependencyService.Register<IBookmarkService, BookmarkService>();
            } 
        }

        static IArticlesService articlesService;
        public static IArticlesService ArticlesService => articlesService ?? (articlesService = DependencyService.Get<IArticlesService>());

        static ICategoryService categoryService;
        public static ICategoryService CategoryService => categoryService ?? (categoryService = DependencyService.Get<ICategoryService>());

        static IBookmarkService bookmarkService;
        public static IBookmarkService BookmarkService => bookmarkService ?? (bookmarkService = DependencyService.Get<IBookmarkService>());


    }
}

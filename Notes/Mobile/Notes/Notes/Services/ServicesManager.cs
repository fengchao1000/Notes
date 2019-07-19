 
using Notes.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Notes.Interfaces;
using Notes.Helpers;
using Notes.Interfaces.Articles;
using Notes.Services.Articles;

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
            }
        }

        static IArticlesService articlesService;
        public static IArticlesService ArticlesService => articlesService ?? (articlesService = DependencyService.Get<IArticlesService>());
           
         
    }
}

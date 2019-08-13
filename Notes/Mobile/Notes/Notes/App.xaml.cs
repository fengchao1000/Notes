using Notes.Controls;
using Notes.Helpers;
using Notes.Services;
using Notes.Views.Article;
using Plugin.SimpleLogger;
using Plugin.SimpleLogger.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzEzNDVAMzEzNjJlMzQyZTMwSUlPcU1RaktJK1BNd3NvQzRBYnByYW9mbVdtcG5qUGQ2aHFZMHAyViswUT0=");

            InitializeComponent();

            //注册服务
            ServicesManager.Initialize();

            //创建或者更新Sqlite数据库表
            Task.Run(() => SqliteHelper.Current.CreateOrUpdateAllTablesAsync());

            CrossSimpleLogger.Current.Configure("mylog", 3, 100, LogLevel.Warning);

            CrossSimpleLogger.Current.Info("Application Started...");

            MainPage = new CustomNavigationPage(new ArticlesPage());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

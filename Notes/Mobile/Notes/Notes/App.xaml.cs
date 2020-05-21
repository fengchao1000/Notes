using Notes.Controls;
using Notes.Helpers;
using Notes.Resources;
using Notes.Services;
using Notes.Views;
using Notes.Views.Article;
using Notes.Views.Categorys;
using Plugin.SimpleLogger;
using Plugin.SimpleLogger.Abstractions;
using System;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes
{
    public partial class App : Application
    {

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjUzMzk0QDMxMzgyZTMxMmUzMGxWL3JDK20vbWV5SGszZTZlcWIzcHF1Um9EeWtNLzlzdmx0RjQxN1JxY2c9");

            InitializeComponent();

            //注册服务
            ServicesManager.Initialize();

            LoggerHelper.Current.Debug("Initialize OK");

            //创建或者更新Sqlite数据库表
            Task.Run(() => SqliteHelper.Current.CreateOrUpdateAllTablesAsync());

            LoggerHelper.Current.Debug("SqliteHelper.Current.CreateOrUpdateAllTablesAsync OK");

            MainPage = new CustomNavigationPage(new FingerprintPage());// new CustomNavigationPage(new MainPage());

            LoggerHelper.Current.Debug("CustomNavigationPage OK");
        }

        public static ResourceManager ResManager
        {
            get
            { 
                return new ResourceManager(typeof(LocalizationResources)); 
            }
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

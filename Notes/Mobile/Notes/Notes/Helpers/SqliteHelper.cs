using Microsoft.AppCenter.Crashes;
using Notes.Models;  
using Notes.Interfaces;  
using SQLite;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;
using Notes.Models.Articles;
using Notes.Models.Categorys;
using Notes.Models.Bookmarks;

namespace Notes.Helpers
{
    public class SqliteHelper
    {
        static SqliteHelper baseSqlite;
        public static SqliteHelper Current
        {
            get { return baseSqlite ?? (baseSqlite = new SqliteHelper()); }
        }
        public SQLiteAsyncConnection db;
        public SqliteHelper()
        {
            //获取注入的ISQLite，在Android和iOS项目已经注入了ISQLite的实现
            if (db == null)
                db = DependencyService.Get<ISQLiteHelper>().GetAsyncConnection();
        }

        /// <summary>
        /// 创建或者更新Sqlite数据库表
        /// 在App启动的时候执行该方法，sqlite-net-pcl会根据实体类创建对应的表，如果实体类有更新，表结构也会更新，如果表结构没变，则不进行操作，sqlite-net-pcl会自动判断
        /// </summary>
        public async void CreateOrUpdateAllTablesAsync()
        { 
            try
            { 
                await db.CreateTablesAsync<ArticlesDto,KeyValueTable,Category,Bookmark, TaskModel>(); 
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// 清空历史数据
        /// </summary>
        public async Task ClearHistoryDataAsync()
        {
            try
            {
                await db.DeleteAllAsync<ArticlesDto>(); 
                await db.DeleteAllAsync<KeyValueTable>(); 
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}

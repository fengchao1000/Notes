using SQLite;
using System;
using System.IO;
using PA.Mobile.Droid.Helpers;
using Notes.Interfaces;
using Notes.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteAndroidHelper))]
namespace PA.Mobile.Droid.Helpers
{
    public class SQLiteAndroidHelper : ISQLiteHelper
    {
        private static string path;

        private static SQLiteAsyncConnection connectionAsync;

        private static readonly object locker = new object();
        private static readonly object pathLocker = new object();

        private static string GetDatabasePath()
        {
            lock (pathLocker)
            {
                if (path == null)
                { 
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Documents folder
                    path = Path.Combine(documentsPath, AppConfig.SqliteFilename);
                }
            }
            return path;
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            lock (locker)
            {
                if (connectionAsync == null)
                {
                    var dbPath = GetDatabasePath();
                    connectionAsync = new SQLiteAsyncConnection(dbPath);
                }
            }
            return connectionAsync;
        }
    }
}
using Newtonsoft.Json;
using Notes.Helpers;
using Notes.Interfaces;
using Notes.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Notes.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object lockObj = new object();

        /// <summary>
        /// 把实体数据保存在对应表中
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertToSqliteAsync(TEntity entity)
        {
            await SqliteHelper.Current.db.InsertAsync(entity);
        }

        /// <summary>
        /// 删除实体到Sqlite
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns></returns>
        public async Task DeleteFromSqliteAsync(object primaryKey)
        {
            await SqliteHelper.Current.db.DeleteAsync<TEntity>(primaryKey);
        }

        /// <summary>
        /// 更新实体到Sqlite
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateToSqliteAsync(TEntity entity)
        {
            await SqliteHelper.Current.db.UpdateAsync(entity);
        }

        /// <summary>
        /// 批量更新实体到Sqlite，如果存在则更新，不存在则添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        //public async Task BatchUpdateToSqlite(IList<TEntity> list)
        //{
        //    //获取主键的 PropertyInfo
        //    PropertyInfo pkProp = typeof(TEntity).GetProperties().Where(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).Length > 0).FirstOrDefault();

        //    foreach (var item in list)
        //    {
        //        //实体类中主键的值
        //        var pk = pkProp.GetValue(item);
        //        lock (lockObj)
        //        {
        //           var entity = GetFromSqliteAsync(pk).ConfigureAwait(true);

        //            int i = 0;

        //            GetFromSqliteAsync(pk).ContinueWith((results) =>
        //            {
        //                try
        //                {
        //                    if (results == null)
        //                    {

        //                         InsertToSqliteAsync(item);

        //                    }
        //                    else
        //                    {
        //                         UpdateToSqliteAsync(item);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    LoggerHelper.Current.Error(ex.ToString());
        //                }
        //            });
        //        }
        //    }
        //}

        public async Task BatchUpdateToSqlite(IList<TEntity> list)
        {
            //获取主键的 PropertyInfo
            PropertyInfo pkProp = typeof(TEntity).GetProperties().Where(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).Length > 0).FirstOrDefault();

            foreach (var item in list)
            {
                //实体类中主键的值
                var pk = pkProp.GetValue(item);
                await GetFromSqliteAsync(pk).ContinueWith(async (results) =>
                {
                    try
                    {
                        if (results.Result == null)
                        {
                            await InsertToSqliteAsync(item);
                        }
                        else
                        {
                            await UpdateToSqliteAsync(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerHelper.Current.Error(ex.ToString());
                    }
                });
            }
        }

        /// <summary>
        /// 先批量删除再批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task BatchDeleteFirstAndAddLaterToSqlite(IList<TEntity> list)
        {
            //清空信息
            await SqliteHelper.Current.db.DeleteAllAsync<TEntity>();
            //添加最新信息
            foreach (var item in list)
            {
                try
                {
                    await InsertToSqliteAsync(item);
                }
                catch (Exception ex)
                {
                    LoggerHelper.Current.Error(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="pk">主键</param>
        /// <returns></returns>
        public async Task<TEntity> GetFromSqliteAsync(object pk)
        {
            return await SqliteHelper.Current.db.FindAsync<TEntity>(pk);
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<IList<TEntity>> GetAllFromSqliteAsync()
        {
            return await SqliteHelper.Current.db.Table<TEntity>().ToListAsync();
        }

        /// <summary>
        /// 查询表数据，返回AsyncTableQuery，可以对AsyncTableQuery进行OrderByDescending(a => a.X).Skip(0).Take(pageSize).ToListAsync();
        /// </summary> 
        /// <returns>AsyncTableQuery</returns>
        public AsyncTableQuery<TEntity> TableQueryFromSqliteAsync()
        {
            return SqliteHelper.Current.db.Table<TEntity>();
        }

        /// <summary>
        /// 把数据添加到Sqlite中
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task InsertToSqliteKeyValueTable(Object data, Object filter)
        {
            await SqliteHelper.Current.db.InsertAsync
                (
                    new KeyValueTable()
                    {
                        Key = EncryptHelper.GetMD5(JsonConvert.SerializeObject(filter)),
                        Value = JsonConvert.SerializeObject(data),
                        CreatedTime = DateTime.UtcNow
                    }
                );
        }

        /// <summary>
        /// 删除Sqlite中的数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<int> DeleteFromSqliteKeyValueTable(Object filter)
        {
            string key = EncryptHelper.GetMD5(JsonConvert.SerializeObject(filter));
            return await SqliteHelper.Current.db.DeleteAsync<KeyValueTable>(key);
        }

        /// <summary>
        /// 把数据更新到Sqlite中
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task UpdateToSqliteKeyValueTable(Object data, Object filter)
        {
            await DeleteFromSqliteKeyValueTable(filter);
            await InsertToSqliteKeyValueTable(data, filter);
        }

        /// <summary>
        /// 从Sqlite中获取数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IList<TCustomEntity>> GetFromSqliteKeyValueTable<TCustomEntity>(Object filter)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string key = EncryptHelper.GetMD5(JsonConvert.SerializeObject(filter));
            KeyValueTable keyValueTable = await SqliteHelper.Current.db.Table<KeyValueTable>().Where(q => q.Key == key).FirstOrDefaultAsync();
            if (null != keyValueTable)
            {
                var data = JsonConvert.DeserializeObject<IList<TCustomEntity>>(keyValueTable.Value);

                sw.Stop();
                LoggerHelper.Current.Debug("GetFromSqliteKeyValueTable ElapsedMilliseconds :" + sw.ElapsedMilliseconds);

                return data;
            }
            else
            {
                return null;
            }


        }
         
    }
}

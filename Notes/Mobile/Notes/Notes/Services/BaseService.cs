using Newtonsoft.Json;
using Notes.Helpers;
using Notes.Interfaces;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services
{
    public class BaseService: IBaseService
    {
        /// <summary>
        /// 把数据添加到Sqlite中
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task InsertDataToSqlite<T>(IList<T> data,Object filter)
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
        public async Task<int> DeleteDataFromSqlite(Object filter)
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
        public async Task UpdateDataToSqlite<T>(IList<T> data, Object filter)
        {
            await DeleteDataFromSqlite(filter);
            await InsertDataToSqlite(data, filter); 
        } 

        /// <summary>
        /// 从Sqlite中获取数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IList<T>> GetDataFromSqlite<T>(Object filter)
        {
            string key = EncryptHelper.GetMD5(JsonConvert.SerializeObject(filter));
            KeyValueTable keyValueTable = await SqliteHelper.Current.db.Table<KeyValueTable>().Where(q => q.Key == key).FirstOrDefaultAsync();
            if (null != keyValueTable)
            {
                return JsonConvert.DeserializeObject<IList<T>>(keyValueTable.Value);
            }
            else
            {
                return null;
            }
        }
    }
}

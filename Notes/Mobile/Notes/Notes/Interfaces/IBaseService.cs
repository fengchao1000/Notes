using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Interfaces
{
    public interface IBaseService
    {
        /// <summary>
        /// 把数据添加到Sqlite中
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task InsertDataToSqlite<T>(IList<T> data, Object filter);

        /// <summary>
        /// 删除Sqlite中的数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<int> DeleteDataFromSqlite(Object filter);

        /// <summary>
        /// 把数据更新到Sqlite中
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task UpdateDataToSqlite<T>(IList<T> data, Object filter);

        /// <summary>
        /// 从Sqlite中获取数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<T>> GetDataFromSqlite<T>(Object filter);
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 把实体数据保存在对应表中
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertToSqliteAsync(TEntity entity);

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task DeleteFromSqliteAsync(object obj);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateToSqliteAsync(TEntity entity);

        /// <summary>
        /// 先批量删除再批量添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task BatchDeleteFirstAndAddLaterToSqlite(IList<TEntity> list);

        /// <summary>
        /// 批量更新实体数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task BatchUpdateToSqlite(IList<TEntity> list);

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<TEntity> GetFromSqliteAsync(object obj);

        /// <summary>
        /// 获取所有实体数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllFromSqliteAsync();

        /// <summary>
        /// 查询表数据，返回AsyncTableQuery，可以对AsyncTableQuery进行OrderByDescending(a => a.X).Skip(0).Take(pageSize).ToListAsync();
        /// </summary> 
        /// <returns>AsyncTableQuery</returns>
        AsyncTableQuery<TEntity> TableQueryFromSqliteAsync();

        /// <summary>
        /// 把数据插入到Sqlite的KeyValueTable中，用于总览、统计等只有单条数据的场景
        /// </summary>
        /// <param name="data">保存的数据</param>
        /// <param name="param">需要转换成key的Param</param>
        /// <returns></returns>
        Task InsertToSqliteKeyValueTable(Object data, Object param);

        /// <summary>
        /// 删除Sqlite的KeyValueTable中的数据，用于总览、统计等只有单条数据的场景
        /// </summary>
        /// <param name="param">需要转换成key的Param</param>
        /// <returns></returns>
        Task<int> DeleteFromSqliteKeyValueTable(Object param);

        /// <summary>
        /// 把数据更新到Sqlite的KeyValueTable中，用于总览、统计等只有单条数据的场景
        /// </summary>
        /// <param name="data">保存的数据</param>
        /// <param name="param">需要转换成key的Param</param>
        /// <returns></returns>
        Task UpdateToSqliteKeyValueTable(Object data, Object param);

        /// <summary>
        /// 从Sqlite的KeyValueTable获取数据，用于总览、统计等只有单条数据的场景
        /// </summary>
        /// <typeparam name="TCustomEntity">转换的类型，从KeyValueTable取出Json数据转换成TCustomEntity</typeparam>
        /// <param name="param">需要转换成key的Param</param>
        /// <returns></returns>
        Task<IList<TCustomEntity>> GetFromSqliteKeyValueTable<TCustomEntity>(Object param);
    }
}

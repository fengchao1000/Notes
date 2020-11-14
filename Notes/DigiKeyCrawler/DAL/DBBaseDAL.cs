using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DigiKeyCrawler.DAL
{
    /// <summary>
    /// Helper class for all dbs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DBBaseDAL<T> where T : class
    {
        public DbContext context { get; set; }

        /// <summary>
        /// Add a entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(T entity)
        {
            context.Add(entity);
            context.Entry<T>(entity).State = EntityState.Added;

            return context.SaveChanges();
        }

        /// <summary>
        /// Update a entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            context.Add(entity);
            context.Entry<T>(entity).State = EntityState.Modified;

            return context.SaveChanges();
        }

        /// <summary>
        /// 部分字段更新，调用该方法时实体中主键必须赋值
        /// </summary>
        /// <param name="entity">需要更新的实体</param>
        /// <param name="updatedProperties">需要更新的字段</param>
        /// <returns></returns>
        public virtual int Update(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {
            var dbEntityEntry = context.Entry(entity);
            if (updatedProperties.Any())
            {
                foreach (var property in updatedProperties)
                {
                    dbEntityEntry.Property(property).IsModified = true;
                }
            }
            else
            {
                foreach (var property in dbEntityEntry.OriginalValues.Properties)
                {
                    var original = dbEntityEntry.OriginalValues.GetValue<object>(property);
                    var current = dbEntityEntry.CurrentValues.GetValue<object>(property);
                    if (original != null && !original.Equals(current))
                    {
                        dbEntityEntry.Property(property.Name).IsModified = true;
                    }
                }
            }
            return context.SaveChanges();
        }

        /// <summary>
        /// Get a list by condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<T> FindList(Expression<Func<T, bool>> where)
        {
            var result = context.Set<T>().Where(where);

            return result;
        }

        /// <summary>
        /// Get a list by condition with ordering
        /// </summary>
        /// <typeparam name="S"><peparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> FindList<S>(Expression<Func<T, bool>> where, Expression<Func<T, S>> orderBy, bool isAsc)
        {
            var list = context.Set<T>().Where(where);
            if (isAsc)
                list = list.OrderBy<T, S>(orderBy);
            else
                list = list.OrderByDescending<T, S>(orderBy);

            return list;
        }


        /// <summary>
        /// remove entity list
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool RemoveList(Expression<Func<T, bool>> where)
        {
            var temp = context.Set<T>().Where(where);

            foreach (var item in temp)
            {
                context.Entry<T>(item).State = EntityState.Deleted;
            }

            int count = context.SaveChanges();
            return true;
        }
    }
}

using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Notes.Helpers;
using Notes.Interfaces.Articles;
using Notes.Models;
using Notes.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Articles
{
    public class ArticlesMockService : IArticlesService
    {
        #region  API Method

        public async Task<ResultData<string>> GetArticleBodyAsync(int id)
        {
            var url = string.Format(AppConfig.ArticleBody, id);
            return await RequestProvider.Current.GetAsync<string>(url, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public async Task<ResultData<PagerSourceDto<ArticlesDto>>> GetArticlesPage(string title, string content, int pageIndex, int pageSize, string sortBy, bool isAsc)
        {
            return new ResultData<PagerSourceDto<ArticlesDto>>()
            {
                IsSuccess = true,
                Data = JsonConvert.DeserializeObject<PagerSourceDto<ArticlesDto>>(mockDataPage)
            };
        } 
         
        #endregion

        #region Sqlite Method

        /// <summary>
        /// 根据key获取单条Message
        /// </summary>
        /// <param name="pushMessageKey"></param>
        /// <returns></returns>
        public async Task<ArticlesDto> GetArticlesFromSqlite(int id)
        {
            return await SqliteHelper.Current.db.Table<ArticlesDto>().Where(q => q.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取分页信息列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IList<ArticlesDto>> GetArticlesFromSqlite(int pageIndex,int pageSize)
        {
            return await SqliteHelper.Current.db.Table<ArticlesDto>().OrderByDescending(a => a.Id).Skip(pageIndex).Take(pageSize).ToListAsync();
        } 
           
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="messageList"></param>
        /// <returns></returns>
        public async Task InsertArticlesListToSqlite(IList<ArticlesDto> messageList)
        {
            foreach (var item in messageList)
            {
                try
                {
                    await SqliteHelper.Current.db.InsertAsync(item);
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            }
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="messageList"></param>
        /// <returns></returns>
        public async Task UpdateArticlesToSqlite(IList<ArticlesDto> messageList)
        {
            foreach (var item in messageList)
            {
                await GetArticlesFromSqlite(item.Id).ContinueWith(async (results) =>
                {
                    if (results.Result == null)
                    {
                        try
                        {
                            await SqliteHelper.Current.db.InsertAsync(item);
                        }
                        catch (Exception ex)
                        {
                            Crashes.TrackError(ex);
                        }
                    }
                    else
                    {
                        await UpdateArticlesToSqlite(item);
                    }
                });
            }
        }

        /// <summary>
        /// 更新信息
        /// </summary> 
        /// <returns></returns>
        public async Task<int> UpdateArticlesToSqlite(ArticlesDto model)
        {
            return await SqliteHelper.Current.db.UpdateAsync(model);
        }

        /// <summary>
        /// 删除发货统计数据
        /// </summary> 
        /// <returns></returns>
        public async Task<int> DeleteArticlesFromSqlite(int id)
        {
            return await SqliteHelper.Current.db.DeleteAsync<ArticlesDto>(id);
        }
         
        #endregion

        #region MockData

        string mockDataPage = @" 
              {
                  ""data"": [
                    {
                      ""id"": 1,
                      ""title"": ""DDD理论学习系列（1）-- 通用语言"",
                      ""summary"": ""在开始之前，我想我们有必要先了解以下DDD的主要参与者。因为毕竟语言是人说的吗，就像我们面向对象编程一样，那通用语言面向的是"",
                      ""url"": ""https://www.ebay.com/itm/254300922947"",
                      ""description"": ""在开始之前，我想我们有必要先了解以下DDD的主要参与者。因为毕竟语言是人说的吗，就像我们面向对象编程一样，那通用语言面向的是"",
                      ""category"": 1,
                      ""body"": ""1"",
                      ""createTime"": ""2019-01-01T12:11:11""
                    },
                    {
                      ""id"": 1,
                      ""title"": ""DDD理论学习系列（2）-- 领域"",
                      ""summary"": ""不管是指国家的主权范围也好还是学术活动范围，都是在讲一个范围，一个界限。比如我们常说的，学术领域、思想领域、技术领域、语言领域、物理领域、医学领域、游戏领域、JAVA领域、.NET领域等等，它们中不管是泛指还是特指某个领域，都是限"",
                      ""url"": ""https://www.jianshu.com/p/8c0d0850ed45"",
                      ""description"": ""在开始之前，我想我们有必要先了解以下DDD的主要参与者。因为毕竟语言是人说的吗，就像我们面向对象编程一样，那通用语言面向的是"",
                      ""category"": 1,
                      ""body"": ""1"",
                      ""createTime"": ""2019-01-01T12:11:11""
                    },
                    {
                      ""id"": 1,
                      ""title"": ""DDD理论学习系列（3）-- 限界上下文"",
                      ""summary"": ""不管是指国家的主权范围也好还是学术活动范围，都是在讲一个范围，一个界限。比如我们常说的，学术领域、思想领域、技术领域、语言领域、物理领域、医学领域、游戏领域、JAVA领域、.NET领域等等，它们中不管是泛指还是特指某个领域，都是限"",
                      ""url"": ""https://www.jianshu.com/p/31e526b3fe33"",
                      ""description"": ""在开始之前，我想我们有必要先了解以下DDD的主要参与者。因为毕竟语言是人说的吗，就像我们面向对象编程一样，那通用语言面向的是"",
                      ""category"": 1,
                      ""body"": ""1"",
                      ""createTime"": ""2019-01-01T12:11:11""
                    }
                    ,
                    {
                      ""id"": 1,
                      ""title"": ""DDD理论学习系列（4）-- 领域模型"",
                      ""summary"": ""不管是指国家的主权范围也好还是学术活动范围，都是在讲一个范围，一个界限。比如我们常说的，学术领域、思想领域、技术领域、语言领域、物理领域、医学领域、游戏领域、JAVA领域、.NET领域等等，它们中不管是泛指还是特指某个领域，都是限"",
                      ""url"": ""https://www.jianshu.com/p/5b2a7e766cdc"",
                      ""description"": ""在开始之前，我想我们有必要先了解以下DDD的主要参与者。因为毕竟语言是人说的吗，就像我们面向对象编程一样，那通用语言面向的是"",
                      ""category"": 1,
                      ""body"": ""1"",
                      ""createTime"": ""2019-01-01T12:11:11""
                    }
                    ,
                    {
                      ""id"": 1,
                      ""title"": ""DDD理论学习系列（5）-- 统一建模语言"",
                      ""summary"": ""不管是指国家的主权范围也好还是学术活动范围，都是在讲一个范围，一个界限。比如我们常说的，学术领域、思想领域、技术领域、语言领域、物理领域、医学领域、游戏领域、JAVA领域、.NET领域等等，它们中不管是泛指还是特指某个领域，都是限"",
                      ""url"": ""https://www.jianshu.com/p/cafadcd3ae1a"",
                      ""description"": ""在开始之前，我想我们有必要先了解以下DDD的主要参与者。因为毕竟语言是人说的吗，就像我们面向对象编程一样，那通用语言面向的是"",
                      ""category"": 1,
                      ""body"": ""1"",
                      ""createTime"": ""2019-01-01T12:11:11""
                    } ,
                    {
                      ""id"": 1,
                      ""title"": ""Android HelloWorld 分析（Activity类基础）"",
                      ""summary"": ""安装环境配置就不在说了,网上有很多,直接来HelloWorld开始变成。"",
                      ""url"": ""https://www.cnblogs.com/chutianshu1981/archive/2013/02/19/2917538.html"",
                      ""description"": ""安装环境配置就不在说了,网上有很多,直接来HelloWorld开始变成。"",
                      ""category"": 1,
                      ""body"": ""1"",
                      ""createTime"": ""2019-01-01T12:11:11""
                    }
                    ,
                    {
                      ""id"": 1,
                      ""title"": ""普通控件使用"",
                      ""summary"": ""现在的ADT越来越好用，已经可以轻松使用工具面板进行可视化开发。这一章中介绍普通控件的使用方法。"",
                      ""url"": ""https://www.cnblogs.com/chutianshu1981/archive/2013/02/23/2920956.html"",
                      ""description"": ""现在的ADT越来越好用，已经可以轻松使用工具面板进行可视化开发。这一章中介绍普通控件的使用方法。"",
                      ""category"": 1,
                      ""body"": ""1"",
                      ""createTime"": ""2019-01-01T12:11:11""
                    }
                    ,
                    {
                      ""id"": 1,
                      ""title"": ""Android学习笔记（一）"",
                      ""summary"": ""Android学习笔记（一）"",
                      ""url"": ""https://blog.csdn.net/flowingflying/article/details/6198713"",
                      ""description"": ""Android学习笔记（一）"",
                      ""category"": 1,
                      ""body"": ""1"",
                      ""createTime"": ""2019-01-01T12:11:11""
                    },
                    {
                      ""id"": 1,
                      ""title"": ""Activity和main.xml文件"",
                      ""summary"": ""Activity和main.xml文件"",
                      ""url"": ""https://blog.csdn.net/flowingflying/article/details/6218475"",
                      ""description"": ""Android学习笔记（一）"",
                      ""category"": 1,
                      ""body"": ""1"",
                      ""createTime"": ""2019-01-01T12:11:11""
                    }
                  ],
                  ""pageIndex"": 1,
                  ""pageSize"": 10,
                  ""recordCount"": 1,
                  ""pageCount"": 1
                }
        ";

        string searchMobileMessageMockDataPage2 = @"
                {
                ""listData"": [
                    {
                        ""messageKey"": ""1055c0c8-0dfc-4a4a-89d4-0f014f81d2c6"",
                        ""messageType"": 1,
                        ""title"": ""消息标题11"",
                        ""content"": ""消息内容消息内容消息内容消息内容消息内容消息内容消息内容消息内容消息内容11"",
                        ""pushType"": 0,
                        ""creator"": ""1055c0c8-0dfc-4a4a-89d4-0f014f81d1c6"",
                        ""creationDate"": ""2019-03-19T08:16:43.31""
                    },
                    {
                        ""messageKey"": ""1055c0c8-0dfc-4a4a-89d4-0f014f81d3c6"",
                        ""messageType"": 1,
                        ""title"": ""消息标题12"",
                        ""content"": ""消息内容消息内容消息内容消息内容消息内容消息内容消息内容消息内容消息内容12"",
                        ""pushType"": 0,
                        ""creator"": ""1055c0c8-0dfc-4a4a-89d4-0f014f81d1c6"",
                        ""creationDate"": ""2019-03-19T08:15:43.31""
                    }
                ],
                ""recordCount"": 12
            }
        ";

        #endregion
    }
}

using DigiKeyCrawler.DAL;
using DigiKeyCrawler.Helpers.Http;
using DigiKeyCrawler.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DigiKeyCrawler.CrawlerManager
{
    public class ProductCategoryCrawler
    {
        /// <summary>
        /// 抓取数据保存到db
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static void Crawler()
        {
            var httpResults = ProductCategoryHTML();
            var productCategoryList = ParsingHTMLToProductCategoryModel(httpResults);
            SaveProductCategoryToDB(productCategoryList);
        }

        /// <summary>
        /// 获取Product详情页html
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static HttpResults ProductCategoryHTML()
        {
            HttpHelpers httpHelpers = new HttpHelpers();
            HttpItems httpItems = new HttpItems();
            httpItems.Url = "https://www.digikey.com/en/products";
            httpItems.UserAgent = "PostmanRuntime/7.26.5";
            return httpHelpers.GetHtml(httpItems);
        }

        /// <summary>
        /// 解析HTMl为ProductCategory模型
        /// </summary>
        /// <param name="httpResults"></param>
        /// <returns></returns>
        public static List<ProductCategory> ParsingHTMLToProductCategoryModel(HttpResults httpResults)
        {
            try
            {
                if (httpResults.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(httpResults.Html);
                var nodes = doc.DocumentNode.SelectNodes("//script[@id='__NEXT_DATA__']");
                var strJson = nodes[0].InnerHtml;
                var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(strJson);
                var props = JsonConvert.SerializeObject(jsonObject["props"]);
                var pageProps = JsonConvert.DeserializeObject<Dictionary<string, object>>(props)["pageProps"];
                var envelope = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(pageProps))["envelope"];
                var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(envelope))["data"];
                var categories = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(data))["categories"];
                var datas = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(categories));
                List<ProductCategory> cates = new List<ProductCategory>();
                foreach (var item in datas)
                {
                    ProductCategory cate = new ProductCategory();
                    cate.CategoryId = Convert.ToInt32(item["id"]);
                    cate.CategoryName = item["label"].ToString().Trim();
                    cate.ParentCategoryId = -1;
                    cate.DetailUrl = item["url"].ToString();
                    cates.Add(cate);
                    var subNodes = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(item["subCategories"]));
                    foreach (var item2 in subNodes)
                    {
                        ProductCategory cate2 = new ProductCategory();
                        cate2.CategoryId = Convert.ToInt32(item2["id"]);
                        cate2.CategoryName = item2["label"].ToString().Trim();
                        cate2.ParentCategoryId = cate.CategoryId;
                        cate2.DetailUrl = item2["url"].ToString();
                        cates.Add(cate2);
                    }
                }

                return cates;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 保存数据到db
        /// </summary>
        /// <param name="httpResults"></param>
        /// <returns></returns>
        public static void SaveProductCategoryToDB(List<ProductCategory> productCategoryList)
        {
            if (productCategoryList == null)
            {
                return;
            }

            if (productCategoryList.Count <= 0)
            {
                return;
            }

            DBBaseDAL<ProductCategory> productCategoryDAL = new DBBaseDAL<ProductCategory>();

            foreach (var item in productCategoryList)
            {
                try
                {
                    productCategoryDAL.Add(item);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}

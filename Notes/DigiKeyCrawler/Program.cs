using DigiKeyCrawler.CrawlerManager;
using DigiKeyCrawler.DAL;
using DigiKeyCrawler.Helpers;
using DigiKeyCrawler.Models;
using System;

namespace DigiKeyCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://github.com/zqlovejyc/EFCoreRepository

            Console.WriteLine("==== Start Crawler=== ");

            Console.WriteLine("==== Runing === ");

            //产品分类抓取
            //ProductCategoryCrawler.Crawler();

            //产品抓取
            ProductCrawler.CrawlerAllProduct();

            //var productHTML = ProductCrawler.GetProductHTML(1000177); 
            //var product = ProductCrawler.ParsingHTMLToProductModel(productHTML); 
            //DBBaseDAL<Product> productDAL = new DBBaseDAL<Product>();
            //productDAL.Add(product);

            Console.WriteLine("==== End Crawler=== ");
        }
    }
}

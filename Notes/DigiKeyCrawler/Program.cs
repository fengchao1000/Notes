using DigiKeyCrawler.CrawlerManager;
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

            var productHTML = ProductCrawler.GetProductHTML(1000173);

            var product = ProductCrawler.ParsingHTMLToProductModel(productHTML);

            Console.WriteLine("==== End Crawler=== ");
        }
    }
}

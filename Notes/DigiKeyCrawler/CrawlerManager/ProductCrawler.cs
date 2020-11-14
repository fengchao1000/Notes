using DigiKeyCrawler.Helpers.Http;
using DigiKeyCrawler.Models;
using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;  
using System.Linq; 
using Newtonsoft.Json;   

namespace DigiKeyCrawler.CrawlerManager
{
    public static class ProductCrawler
    {
        /// <summary>
        /// 获取Product详情页html
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static HttpResults GetProductHTML(int productId) 
        {
            HttpHelpers httpHelpers = new HttpHelpers();
            HttpItems httpItems = new HttpItems();
            httpItems.UserAgent = "PostmanRuntime/7.26.5";
            httpItems.Cookie = @"search=%7B%22usage%22%3A%7B%22dailyCount%22%3A1%2C%22lastRequest%22%3A%222020-11-06T09%3A53%3A57.109Z%22%7D%2C%22version%22%3A1%7D; ai_user=SLNMH|2020-11-06T09:53:57.109Z; ai_session=scCXs|1604656437109|1604656437109; ak_bmsc=0EC8C5810D00709FB93F4C60975BC7D317D536F367500000361DA55FB82BD84F~plwLAMyVqgM0bsdVzFfB6qlODAu0e32lU47q3P7vq3FZz076hP2jCgpExGVWyeMRRbPanjMUXohzuX8TvW+C44BMmzgLtnKznOWNxrN0hMpZkMbwCD+ITPirQLMKFta2a2c9ltk9maXhB11eKqWMylHDjeZMg76vdX3UfVjm8YvEZeZ3tGcfLndL9k41qky2QBjJcMct5hq1dgH9PGQkvr58iNKQuBrU274UgDLcWMYm4=; bm_sz=5B7C9B8E2A96BB2C294CC28FE6A48EA8~YAAQ8zbVF8LwCJV1AQAARBv6nAkb9HEyp4mY40+2ypccTcHMkrGuzOLbP7b/QawLNpGIr0/o88W3Xw3o7W/zQiDJxwYfOSDonepfixLMeCDFebCbjEeg6nIhbKvUKxEXbSKadvZy/5FfYGVe1MqcV9giBX+Y2KLlP/cEhtNplcPIxabFhQ+Zn0bYZxLJfCkhDQ==; _abck=EEAAC7EA1D0AD7EEBFC7B078DAA55A46~-1~YAAQ8zbVF8PwCJV1AQAARRv6nARRpfMbB5kDg6ZZb1pAzuGvd/jp72GGLlljN0m4YbM5YfusqBtW3o1aKj3M3FjIl461ly26xE9ImrE7nDd1CSj95s3PjMf1a+s6JxTa2FQkmLDTqWsKjmTppuLbSeI2xdymwNvVCIr8d2dkQdEoUMnOS8jwHshcpjh8egviczqK+38xm9S1rjGRBkgv2dJQUEuCJJgyv2EwcdWjbW2U2A9ITDIzhGk78GsF1FxxpQF9qhdRXWSvSj+rJyCdoNrH3FTfniedpqPj7+ZB5VRDuE3XcAG3a1nXRQ==~-1~-1~-1; TS017613a9=01460246b66b40620aa30ff6dfb4c861d3f8e1f4b1aa9297148d2269a25c502a0d9633e12bdfc06ea02b5c0a85be34def4669934bc; TS018060f7=01460246b6fc72b719b989a8523ecd16befaad164d0af65dc89ff7ff6e6d04e70509534f6b47a437b4e012069f69b0531e61df1fb2; bm_mi=75F3C83C7A94A12C0C9C413BFFE8078E~yzQFqC2PlSbdoHPZ2pqapMuYyuTZeHc3OyOnvCkv+GMAoCPeWrHtUc6UTdkb5nYm97zfcVa7CPXEQo7uSwl8jgQ0EcYALhQGEQJPufNoo9dYphfy0DT3HTpwc4bPJTqzjEwhXIXFsU5jzKrQsz81oh3yDaGTJdIfkMh4l3ahy+M/xWEquJpSz8LF/UJ6qHDaR5NXbwacVtLRZc4zYj4acpRBX7watLpDTJW7yCutNUBx2Xi9q7Jq9OsPemWsFiNF+5oA1h6DUPoTRsxfWrJ0U0I444aIOrLZMKEzv0kXBOE=; bm_sv=60B0A9C8ADBAA4D454AF67EEAE53EB4F~g1judWgfKn91VPRQ/6aCgsrnEo2eUPRk5NTi9fQcaeleiSlA+oGKonD9pT5zjVdb4pmwiMipl5kJaHsOW099yy+xOjdks4wd4IFmQs9S4MdhnjjmENE35PVkQ+LxkZjnEtyiI3xvPpYQWln+5qtuvrDzDPCPRvhpmtPYLdmuugk=";
            httpItems.Accept = "*/*";
            httpItems.Header.Add("Accept-Encoding", "gzip, deflate, br");
            httpItems.Header.Add("Connection", "keep-alive");
            httpItems.Url = "https://www.digikey.com/en/products/detail/treblab/N8/" + productId;
            Console.WriteLine(httpItems.Url);
            var result = httpHelpers.GetHtml(httpItems);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("==== ProductCrawler fail === "+ httpItems.Url);
                System.IO.File.AppendAllLines("c://CrawlerLog//" + productId + DateTime.Now.ToString("yyyyMMddHHmmssms") + "_error.txt", new string[] { result.Html });
            }
            else 
            {
                Console.WriteLine("==== ProductCrawler success === "+ httpItems.Url);
            }
            result.ID = productId.ToString();
            return result;
        }

        /// <summary>
        /// 解析HTMl为Product模型
        /// </summary>
        /// <param name="httpResults"></param>
        /// <returns></returns>
        public static Product ParsingHTMLToProductModel(HttpResults httpResults)
        {
            if (httpResults.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            Product p = new Product();
            p.ProductKey = Guid.NewGuid();
            p.ProductId = httpResults.ID;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(httpResults.Html);
            var mainImage = doc.DocumentNode.SelectNodes("//div[@class='main-image']");
            if (mainImage != null && mainImage.Count > 0)
            {
                //404
                return null;
            }

            var isNew = doc.DocumentNode.SelectNodes("//div[@class='MuiPaper-root jss93 jss59 MuiPaper-elevation1 MuiPaper-rounded']");
            if (isNew != null && isNew.Count > 0)
            {
                var newText = isNew[0].SelectSingleNode(".//span").InnerText;
                if (newText == "New Product ")
                {
                    p.IsNew = true;
                }
            }

            var imageNode = doc.DocumentNode.SelectNodes("//div[@data-testid='carousel-container']");
            if (imageNode != null)
            {
                var titelImageNode = imageNode[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].Attributes.FirstOrDefault(n => n.Name == "src").Value;
                p.ImageTitle = titelImageNode.Replace("\\", "");
                var otherImageNode = imageNode[0].ChildNodes[1];
                var otherImags = otherImageNode.SelectNodes(".//img");
                List<string> images = new List<string>();
                if (otherImags != null)
                {
                    foreach (var imgItem in otherImags)
                    {
                        images.Add(imgItem.Attributes.FirstOrDefault(n => n.Name == "src").Value);
                    }
                    //p.Images = JsonConvert.SerializeObject(images);
                }
            }
            //分类取第二级
            var cateName = doc.DocumentNode.SelectNodes("//li[@class='MuiBreadcrumbs-li']")[2].ChildNodes[0].InnerText;
            p.CategoryName = cateName;

            //基础信息
            var headerNode = doc.DocumentNode.SelectNodes("//div[@class='MuiGrid-root MuiGrid-item MuiGrid-grid-xs-true']")[0].ChildNodes[0].ChildNodes[0];
            var tbHeader = headerNode.SelectSingleNode("thead");
            var tbBody = headerNode.SelectSingleNode("tbody");
            p.Name = tbHeader.ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText;
            var tr = tbBody.SelectNodes("tr");
            p.DigiKeyPartNumber = tr[0].ChildNodes[1].ChildNodes[0].InnerText;
            p.Manufacturer = tr[1].ChildNodes[1].ChildNodes[0].InnerText;
            p.ManufacturerProductNumber = tr[2].ChildNodes[1].ChildNodes[0].InnerText;
            p.Supplier = tr[3].ChildNodes[1].ChildNodes[0].InnerText;
            p.Description = tr[4].ChildNodes[1].ChildNodes[0].InnerText;
            var text = tr[5].ChildNodes[1].ChildNodes[0].InnerText;
            if (text == "Manufacturer Standard Lead Time")
            {
                p.ManufacturerStandardLeadTime = text;
            }
            else
            {
                p.DetailedDescription = text;
            }
            var tempMedia = doc.DocumentNode.SelectNodes("//div[@data-testid='data-table-Media &amp; Downloads']");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (tempMedia != null && tempMedia.Count > 0)
            {
                var media = doc.DocumentNode.SelectNodes("//div[@data-testid='data-table-Media &amp; Downloads']")[0].SelectSingleNode("table");
                var mediaBody = media.SelectSingleNode("tbody");
                var mediaTrs = mediaBody.SelectNodes("tr");

                foreach (var mediaItem in mediaTrs)
                {

                    var key = mediaItem.ChildNodes[0].InnerText;
                    var aLink = mediaItem.ChildNodes[1].SelectNodes(".//a");
                    if (aLink == null)
                    {
                        continue;
                    }
                    string value = "";
                    foreach (var valueItem in aLink)
                    {
                        value += valueItem.Attributes.FirstOrDefault(x => x.Name == "href").Value + ",";
                    }
                    dic.Add(key, value);
                }
                //p.MediaDownloads = JsonConvert.SerializeObject(dic);
            }

            //感兴趣
            var alsoBe = doc.DocumentNode.SelectNodes("//script[@id='__NEXT_DATA__']");
            var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(alsoBe[0].InnerHtml);
            var props = JsonConvert.SerializeObject(jsonObject["props"]);
            var pageProps = JsonConvert.DeserializeObject<Dictionary<string, object>>(props)["pageProps"];
            var envelope = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(pageProps))["envelope"];
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(envelope))["data"];
            var dataObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(data));
            if (dataObj.ContainsKey("associations"))
            {
                var associations = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(data))["associations"];
                var cardAssociationsObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(associations));
                if (cardAssociationsObj.ContainsKey("cardAssociations"))
                {
                    var cardAssociations = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(associations))["cardAssociations"];
                    var alsoLst = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(cardAssociations));
                    List<Dictionary<string, object>> als = new List<Dictionary<string, object>>();
                    foreach (var alItem in alsoLst)
                    {
                        var josnObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(alItem));
                        var plst = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(josnObj["productCards"]));
                        foreach (var pItem in plst)
                        {
                            als.Add(pItem);
                        }
                    }
                    p.MayAlso = JsonConvert.SerializeObject(als);
                }
            }
            //产品属性
            var productAttributes = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(data))["productAttributes"];
            var attributes = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(productAttributes))["attributes"];
            //p.ProductAttributes = JsonConvert.SerializeObject(attributes);

            //Environmental & Export Classifications
            var environmental = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(data))["environmental"];
            var environmentalObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(environmental));
            if (environmentalObj.ContainsKey("dataRows"))
            {
                //p.EnvironmentalExportClassifications = JsonConvert.SerializeObject(environmentalObj["dataRows"]);
            }

            //additionalResources
            var additionalResources = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(data))["additionalResources"];
            var additionalResourcesObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(environmental));
            if (environmentalObj.ContainsKey("dataRows"))
            {
                //p.AdditionalResources = JsonConvert.SerializeObject(environmentalObj["dataRows"]);
            }

            //priceQuantity
            var priceQuantity = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(data))["priceQuantity"];
            var priceQuantityObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(priceQuantity));
            if (priceQuantityObj.ContainsKey("pricing"))
            {
                var priceObj = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(priceQuantityObj["pricing"]));
                //p.Prices = JsonConvert.SerializeObject(priceObj[0]["pricingTiers"]);
            }
            return p;
        }
    }
}

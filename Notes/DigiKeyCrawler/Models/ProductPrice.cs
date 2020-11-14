using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductPrice
    {
        public Guid ProductPriceKey { get; set; }
        public string ProductId { get; set; }

        public string ProductPriceJson { get; set; }
    }
}

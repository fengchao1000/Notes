using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductPrice
    {
        public Guid ProductPriceKey { get; set; } = Guid.NewGuid();
        public Product Product { get; set; }

        public string ProductPriceJson { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductAttribute
    {
        public Guid ProductAttributeKey { get; set; }
        public string ProductId { get; set; }

        public string ProductAttributeJson { get; set; }
    }
}

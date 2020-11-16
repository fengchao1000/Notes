using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductAttribute
    {
        public Guid ProductAttributeKey { get; set; } = Guid.NewGuid();
        public Product Product { get; set; }

        public string ProductAttributeJson { get; set; }
    }
}

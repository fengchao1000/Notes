using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductAdditionalResource
    {
        public Guid ProductAdditionalResourceKey { get; set; } = Guid.NewGuid();

        public Product Product { get; set; }

        public string ProductAdditionalResourceJson { get; set; }
    }
}

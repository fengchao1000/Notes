using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductAdditionalResource
    {
        public Guid ProductAdditionalResourceKey { get; set; }

        public string ProductId { get; set; }

        public string ProductAdditionalResourceJson { get; set; }
    }
}

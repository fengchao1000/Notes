using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductDocument
    {
        public Guid ProductDocumentKey { get; set; } = Guid.NewGuid();
        public Product Product { get; set; }

        public string ProductDocumentJson { get; set; }
    }
}

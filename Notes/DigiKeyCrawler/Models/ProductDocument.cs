using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductDocument
    {
        public Guid ProductDocumentKey { get; set; }
        public string ProductId { get; set; }

        public string ProductDocumentJson { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductHTML
    {
        public string ProductId { get; set; }

        public Product Product { get; set; }

        /// <summary>
        /// HTML源代码
        /// </summary>
        public string Html { get; set; } = string.Empty;
    }
}

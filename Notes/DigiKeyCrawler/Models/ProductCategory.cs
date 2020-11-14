using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductCategory
    {
        public Guid ProductCategoryKey { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public string DetailUrl { get; set; }
    }
}

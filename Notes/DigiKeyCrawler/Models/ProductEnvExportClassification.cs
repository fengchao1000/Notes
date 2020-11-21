using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductEnvExportClassification
    {
        public Guid ProductEnvExportClassificationKey { get; set; } = Guid.NewGuid();
        public Product Product { get; set; }

        public string ProductEnvExportClassificationJson { get; set; }
    }
}

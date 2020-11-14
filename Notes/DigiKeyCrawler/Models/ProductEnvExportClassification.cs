using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductEnvExportClassification
    {
        public Guid ProductEnvExportClassificationKey { get; set; }
        public string ProductId { get; set; }

        public string ProductEnvExportClassificationJson { get; set; }
    }
}

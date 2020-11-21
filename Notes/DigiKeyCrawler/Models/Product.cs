using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class Product
    { 
        public string ProductId { get; set; }
        public bool IsNew { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        //public string ImageTitle { get; set; } = string.Empty;
        //public string Images { get; set; }

        /// <summary>
        /// 默认是产品的第一张图
        /// </summary>
        public string DefPic { get; set; } = string.Empty;
        /// <summary>
        /// 默认附件
        /// </summary>
        public string DefPdf { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// DigiKey 零件编码
        /// </summary>
        public string DigiKeyPartNumber { get; set; } = string.Empty;
        /// <summary>
        /// 制造商
        /// </summary>
        public string Manufacturer { get; set; } = string.Empty;
        /// <summary>
        /// 制造商产品编号
        /// </summary>
        public string ManufacturerProductNumber { get; set; } = string.Empty;
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 详细描述
        /// </summary>
        public string DetailedDescription { get; set; } = string.Empty;
        /// <summary>
        /// 制造商标准交货时间
        /// </summary>
        public string ManufacturerStandardLeadTime { get; set; } = string.Empty;

        /// <summary>
        /// 媒体下载
        /// </summary>
        //public string MediaDownloads { get; set; }

        /// <summary>
        /// 产品属性
        /// </summary>
        //public string ProductAttributes { get; set; }

        /// <summary>
        /// 出口环境分类
        /// </summary>
        //public string EnvironmentalExportClassifications { get; set; }

        /// <summary>
        /// 额外资源
        /// </summary>
        //public string AdditionalResources { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MayAlso { get; set; } = string.Empty;

        /// <summary>
        /// 产品价格
        /// </summary>
        //public string Prices { get; set; }

        public virtual ICollection<ProductAdditionalResource> ProductAdditionalResources { get; set; }

        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }

        public virtual ICollection<ProductDocument> ProductDocuments { get; set; }

        public virtual ICollection<ProductEnvExportClassification> ProductEnvExportClassifications { get; set; }

        public virtual ICollection<ProductPicture> ProductPictures { get; set; }

        public virtual ICollection<ProductPrice> ProductPrices { get; set; }

        public virtual ProductHTML ProductHTML { get; set; }
    }
}

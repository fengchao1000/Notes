using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{
    public class ProductPicture
    {
        public Guid ProductPictureKey { get; set; } = Guid.NewGuid();
        public Product Product { get; set; }

        public string PictureUrl { get; set; }

    }
}

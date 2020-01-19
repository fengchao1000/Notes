using System;
using System.Collections.Generic;
using System.Text;

namespace FC.Notes.Itinerarys
{
    public class OverheadItem
    {
        public Guid ID { get; private set; }

        //开销内容
        public string Content { get; private set; }

        //开销金额
        public decimal Money { get; private set; }

        public Guid AccountBookID { get; private set; }

        // ctor

        // 开销项目的行为
        public OverheadItem(string content, decimal money) 
        {
            this.Content = content;
            this.Money = money;
        }
    }
}

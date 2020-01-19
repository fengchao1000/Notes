using System;
using System.Collections.Generic;
using System.Text;

namespace FC.Notes.Itinerarys
{
    public class AccountBook
    {
        public Guid ID { get; private set; }

        public List<OverheadItem> OverheadItems {  get; protected set; }

        public Guid ItineraryID { get; set; }

        //ctor

        // 记账薄的行为 
        public void RecordAnAccount( 
         string itemName,
         decimal costMoney)
        { 
             OverheadItems.Add(new OverheadItem(itemName, costMoney));
        }
    }
}

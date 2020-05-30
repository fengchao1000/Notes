using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FC.Notes.Itinerarys
{
    public class Itinerary
    {
        public Guid ID { get; set; }

        //public List<Person> Participants { get; private set; }

        //public List<string> Places { get; private set; }

        public string Note { get; private set; }

        public DateTime TripTime { get; private set; }
        public int Status { get; private set; }

        //将记账薄放置在了旅行中
        public AccountBook AccountBook { get; private set; }

        //ctor

        // 行程的行为
        public void RecordAnAccountInItinerary(
         Guid personID,
         string itemName,
         decimal costMoney)
        {
            //bool hasThisPerson = Participants.Any(Person => Person.ID == personID);

            //if (!hasThisPerson)
            //    throw new Exception();

            AccountBook.RecordAnAccount(itemName, costMoney);
        }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Notes.Models.Articles
{
    public partial class ArticlesDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public string Body { get; set; }
        public DateTime CreateTime { get; set; }


        [Ignore]
        public string Date
        {
            get
            {
                return CreateTime.ToString("yyyy/MM/dd");
            }
        }

        [Ignore]
        public Style MessageTitleLabelStyle
        {
            get
            { 
                return (Style)Application.Current.Resources["MessageTitleReadLabelStyle"]; 
            }
            set { }
        }

        [Ignore]
        public Style MessageContentLabelStyle
        {
            get
            {
                return (Style)Application.Current.Resources["MessageContentLabelStyle"]; 
            }
            set { }
        }

        [Ignore]
        public string NotificationImage
        {
            get
            {
                return "ic_message_read.png";
            } 
        }
    }
}

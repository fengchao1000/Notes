using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Interfaces
{
    public interface INotificationHelper
    {
        void ShowNotification(string strNotificationTitle,
                              string strNotificationSubtitle,
                              string strNotificationDescription,
                              string strNotificationIdItem,
                              string strDateOrInterval,
                              int intervalType,
                              string extraParameters);
    }
}

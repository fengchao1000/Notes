using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Helpers
{
    public class GlobalSetting
    {

        //var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/{guidUser}");


        public const string DefaultEndpoint = "http://YOUR_IP_OR_DNS_NAME"; // i.e.: "http://YOUR_IP" or "http://YOUR_DNS_NAME"

        private string _baseIdentityEndpoint;

        public GlobalSetting()
        {
            BaseIdentityEndpoint = DefaultEndpoint;
        }

        public static GlobalSetting Instance { get; } = new GlobalSetting();

        public string BaseIdentityEndpoint
        {
            get { return _baseIdentityEndpoint; }
            set
            {
                _baseIdentityEndpoint = value;
                UpdateEndpoint(_baseIdentityEndpoint);
            }
        }


        private void UpdateEndpoint(string endpoint)
        {
            BaseIdentityEndpoint = $"{endpoint}/Account/Register";
        }

    }
}

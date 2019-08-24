using Prism.Logging.Syslog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Helpers
{
    public class SyslogOptions : ISyslogOptions
    {
        public string HostNameOrIp { get; set; }

        public int? Port { get; set; }

        public string AppNameOrTag { get; set; }
    }
}

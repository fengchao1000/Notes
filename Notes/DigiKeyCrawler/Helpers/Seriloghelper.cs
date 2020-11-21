using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Helpers
{
    public class Seriloghelper
    {
        public static void Config() 
        { 
            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Error() 
                  .WriteTo.File("logs\\error.txt", rollingInterval: RollingInterval.Day)
                  .CreateLogger();
        }
    }
}

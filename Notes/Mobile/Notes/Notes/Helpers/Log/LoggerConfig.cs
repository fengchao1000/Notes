using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Helpers
{
    public static class LoggerConfig
    {
        public static Level level = Level.ERROR;

        public static LoggerType loggerType = LoggerType.Syslog;
    }
}

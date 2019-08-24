namespace Notes.Helpers
{
    public enum LoggerType
    {
        AppCenter,
        AppInsights,
        Graylog,
        LogglyHttp,
        LogglySyslog,
        Syslog
    }

    public enum Level
    {

        /// <summary>
        /// 记录DEBUG|INFO|WARN|ERROR|FATAL级别的日志
        /// </summary>
        DEBUG,
        /// <summary>
        /// 记录INFO|WARN|ERROR|FATAL级别的日志
        /// </summary>
        INFO,
        /// <summary>
        /// 记录WARN|ERROR|FATAL级别的日志
        /// </summary>
        WARN,
        /// <summary>
        /// 记录ERROR|FATAL级别的日志
        /// </summary>
        ERROR,
        /// <summary>
        /// 记录TrackEvent级别的日志
        /// </summary>
        TrackEvent,
        /// <summary>
        /// 关闭日志功能
        /// </summary>
        OFF,
    }
}

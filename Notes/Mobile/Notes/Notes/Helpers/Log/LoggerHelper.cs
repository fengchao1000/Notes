using Prism.Logging;
using Prism.Logging.Syslog; 
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Notes.Helpers
{
    /// <summary>
    /// 日志生产类
    /// Singleton模式和策略模式和工厂模式
    /// </summary>
    public sealed class LoggerHelper 
    {
        #region Logger有多种实现时,需要使用Singleton模式
        
        /// <summary>
        /// 对外不能创建类的实例
        /// </summary>
        private LoggerHelper()
        {
            switch (LoggerConfig.loggerType)
            {
                case LoggerType.AppCenter:
                    
                    break;
                case LoggerType.AppInsights:

                    break;
                case LoggerType.Graylog:

                    break;
                case LoggerType.LogglyHttp:

                    break;
                case LoggerType.LogglySyslog:

                    break;
                case LoggerType.Syslog:
                    var genOptions = new SyslogOptions
                    {
                        HostNameOrIp = "106.52.218.254",
                        Port = 514,
                        AppNameOrTag = "Notes"
                    };
                    iLogger = new SyslogLogger(genOptions);
                    break; 
                default:
                    throw new ArgumentException("请正确配置LoggerConfig的LoggerType节点（AppCenter，AppInsights，Graylog，LogglyHttp，LogglySyslog，Syslog）"); 
            } 
        }

        /// <summary>
        /// 日志级别,todo:可以从配置获取
        /// </summary>
        private static Level level = Level.DEBUG;
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object lockObj = new object();
        /// <summary>
        /// 日志工厂
        /// </summary>
        private static LoggerHelper current;
        /// <summary>
        /// 日志提供者，只在本类中实例化
        /// </summary>
        private ILogger iLogger;
        /// <summary>
        /// 单例模式的日志工厂对象
        /// </summary>
        public static LoggerHelper Current
        {
            get
            {
                if (current == null)
                {
                    lock (lockObj)
                    {
                        if (current == null)
                        {
                            current = new LoggerHelper();
                        }
                    }
                }
                return current;
            }
        }

        #endregion

        #region ILogger 成员

        public void Debug(string message)
        {
            if (level <= Level.DEBUG)
                iLogger.Debug(message);
        }
        public void Info(string message)
        {
            if (level <= Level.INFO)
                iLogger.Info(message);
        }
        public void Warn(string message)
        {
            if (level <= Level.WARN)
                iLogger.Warn(message);
        }
        public void Error(string exceptionMsg)
        {
            if (level <= Level.ERROR)
                iLogger.Log(exceptionMsg, Category.Exception, Priority.High);
        }
        public void TrackEvent(string message)
        {
            if (level <= Level.TrackEvent)
                iLogger.TrackEvent(message);
        }
        #endregion
    }
}

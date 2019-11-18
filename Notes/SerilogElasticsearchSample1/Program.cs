using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Diagnostics;

namespace SerilogElasticsearchSample1
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://117.48.227.241:9200/"))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
            }).CreateLogger();

            Stopwatch st = new Stopwatch();
            st.Start();
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.WithProperty("Application", "SerilogElasticsearchSample1")
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri("http://117.48.227.241:9200/"))
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                        IndexFormat = "msdemo-log-{0:yyyy.MM}"
                    })
                .CreateLogger();

            for (int i = 0; i < 100000000; i++)
            {
                Log.Information("第{count}条Elasticsearch日志",i);
            }

            st.Stop();

            Log.Information("日志导入结束 over ElapsedMilliseconds:{ElapsedMilliseconds}", st.ElapsedMilliseconds);

            Console.WriteLine("Hello World!");
        }
    }
}

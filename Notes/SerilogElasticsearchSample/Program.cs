using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.MariaDB;
using Serilog.Sinks.MariaDB.Extensions;

namespace SerilogElasticsearchSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var log = new LoggerConfiguration()
    .WriteTo.MariaDB(
        connectionString: @"server=...",
        tableName: "Logs",
        autoCreateTable: true,
        useBulkInsert: false,
        options: new MariaDBSinkOptions()
        )
    .CreateLogger();



            Log.Logger = new LoggerConfiguration()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://117.48.227.241:9200/"))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
            }).CreateLogger();

            Log.Information("第一条Elasticsearch日志");


            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

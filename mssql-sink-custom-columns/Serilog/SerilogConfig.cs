using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using mssql_sink_custom_columns.Serilog;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace serilog_demo.Serilog
{
    public static class SerilogConfig
    {
        public static IApplicationBuilder ConfigureSerilog(this IApplicationBuilder builder, ILoggerFactory loggerFactory, string sqlConnString, string logTable)
        {
            ConfigureFactory(loggerFactory, sqlConnString, logTable);
            return builder;
        }

        private static void ConfigureFactory(ILoggerFactory loggerFactory, string sqlConnString, string logTable)
        {
            var columnOptions = new ColumnOptions()
            {
                AdditionalDataColumns = new List<DataColumn> {
                    new DataColumn {DataType = typeof(string), ColumnName = "Controller"},
                    new DataColumn {DataType = typeof(string), ColumnName = "Action"}
                }
            };

            var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel
            .Debug()
            .WriteTo.MSSqlServer(sqlConnString, logTable, columnOptions: columnOptions)
            .Enrich.With<SerilogEnricher>();

            Log.Logger = loggerConfiguration.CreateLogger();

            loggerFactory.AddSerilog();
        }
    }
}
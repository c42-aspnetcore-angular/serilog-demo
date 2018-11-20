using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace serilog_demo.Logging
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
            var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel
            .Debug()
            .WriteTo.MSSqlServer(sqlConnString, logTable);

            Log.Logger = loggerConfiguration.CreateLogger();

            loggerFactory.AddSerilog();
        }
    }
}
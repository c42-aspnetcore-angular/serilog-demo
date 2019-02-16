using System;
using Serilog.Core;
using Serilog.Events;

namespace mssql_sink_custom_columns.Serilog
{
    public class SerilogEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Properties.ContainsKey("LogData") && logEvent.Properties["LogData"] is StructureValue logData)
            {
                foreach(var property in logData.Properties)
                {
                    logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(property.Name, property.Value.ToString("l", null)));
                }
            }
        }
    }
}
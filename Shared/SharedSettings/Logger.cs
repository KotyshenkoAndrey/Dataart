using Microsoft.AspNetCore.Builder;
using Serilog;

namespace DataArt.Settings
{
    public static class Logger
    {
        public static void AddAppLogger(this WebApplicationBuilder builder)
        {
            var logger = new Serilog.LoggerConfiguration()
                .Enrich.WithCorrelationIdHeader()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog(logger, true);
        }
    }
}

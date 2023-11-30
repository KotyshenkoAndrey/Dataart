using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DataArt.Settings
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddAppCors(this IServiceCollection services)
        {
            services.AddCors(builder =>
            {
                builder.AddDefaultPolicy(pol =>
                {
                    pol.AllowAnyHeader();
                    pol.AllowAnyMethod();
                    pol.AllowAnyOrigin();
                });
            });
            return services;
        }

        public static void UseAppCors(this IApplicationBuilder app)
        {
            app.UseCors();
        }
    }
}

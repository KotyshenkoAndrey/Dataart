using DataArt.Settings;
//using DataArt.Database;
using Microsoft.EntityFrameworkCore;
using System.Runtime;

var builder = WebApplication.CreateBuilder(args);

var mainSettings = Settings.Load<MainSettings>("Main");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var settings = Settings.Load<DbSettings>("Database");

builder.AddAppLogger();

var services = builder.Services;
services.AddAppCors();
services.AddAppVersioning();
services.AddAppSwagger(mainSettings, swaggerSettings);
//services.AddDbContext<MyDbContext>(opt => opt.UseNpgsql(settings.ConnectionString));

services.AddRazorPages();
services.AddControllers().AddNewtonsoftJson();

services
    .AddMainSettings()
    .AddSwaggerSettings()
    .AddApiSpecialSettings()
    ;

var app = builder.Build();
app.UseAppSwagger();

app.MapRazorPages();
app.MapControllers();

app.Run();
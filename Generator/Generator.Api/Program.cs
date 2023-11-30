using DataArt.Settings;


var builder = WebApplication.CreateBuilder(args);

var mainSettings = Settings.Load<MainSettings>("Main");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var processorURL = Settings.Load<MainSettings>("Main");

builder.AddAppLogger();

var services = builder.Services;
services.AddAppCors();
services.AddAppVersioning();
services.AddAppSwagger(mainSettings, swaggerSettings);

services.AddRazorPages();
services.AddControllers().AddNewtonsoftJson();

services
    .AddMainSettings()
    .AddSwaggerSettings()
    .AddApiSpecialSettings()
    ;
services.AddHttpClient("ProcessorClient", client =>
{
    client.BaseAddress = new Uri(processorURL.URLProcessor);
});

var app = builder.Build();
app.UseAppSwagger();

app.MapRazorPages();
app.MapControllers();


app.Run();
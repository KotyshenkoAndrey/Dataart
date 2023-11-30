using Microsoft.Extensions.Configuration;

namespace DataArt.Settings;

public static class SettingsFactory
{
    public static IConfiguration Create(IConfiguration? configuration = null)
    {
        var conf = configuration ?? new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", optional: false)
                                        .AddJsonFile("appsettings.development.json", optional: true)
                                        .AddEnvironmentVariables()
                                        .Build();

        return conf;
    }
}

public abstract class Settings
{
    public static T Load<T>(string key, IConfiguration configuration = null)
    {
        var settings = (T)Activator.CreateInstance(typeof(T));

        SettingsFactory.Create(configuration).GetSection(key).Bind(settings, (x) => { x.BindNonPublicProperties = true; });

        return settings;
    }
}


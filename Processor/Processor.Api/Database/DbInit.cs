using DataArt.Database;

namespace Processor.Api.Database
{
    public static class DbInit
    {
        public static void Execute(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
            ArgumentNullException.ThrowIfNull(scope);

            var context = scope.ServiceProvider.GetRequiredService<MyDbContext>();
            ArgumentNullException.ThrowIfNull(context);

            var ii = context.Database.CanConnect();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}

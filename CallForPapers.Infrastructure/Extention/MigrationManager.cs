using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CallForPapers.Infrastructure.Extention;

public static class MigrationManager
{
    public async static Task<IHost> MigrateDatabase(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>())
            {
                await appContext.Database.MigrateAsync();
            }
        }

        return host;
    }
}
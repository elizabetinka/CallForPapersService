using CallForPapers.Infrastructure.Repositories;
using CallForPapers.Services.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CallForPapers.Infrastructure.Extention;

public static class InfrastructureExtention
{
    public static void ExtentionRepository(this IServiceCollection nCollection)
    {
        nCollection.AddScoped<IApplicationRepository, ApplicationRepository>();
        nCollection.AddScoped<IActivityRepository, ActivityRepository>();
        nCollection.AddDbContextFactory<ApplicationContext>();
    }
}
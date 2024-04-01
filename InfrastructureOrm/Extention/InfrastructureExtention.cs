using InfrastructureOrm.Repositories;
using InfrastructureOrm.Repositories.interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureOrm.Extention;

public static class InfrastructureExtention
{
    public static void ExtentionRepository(this IServiceCollection nCollection)
    {
        nCollection.AddSingleton<IAdminRepository, AdminRepository>();
        nCollection.AddSingleton<IUserRepository, UserRepository>();
        nCollection.AddSingleton<IApplicationRepository, ApplicationRepository>();
    }
}
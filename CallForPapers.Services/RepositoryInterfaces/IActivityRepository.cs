using CallForPapers.InfrastructureServicesDto;

namespace CallForPapers.Services.RepositoryInterfaces;

public interface IActivityRepository
{
    public  IList<ActivityDto> GetAll();
    
    public  bool ExistsItsActivity(String? activity);
}
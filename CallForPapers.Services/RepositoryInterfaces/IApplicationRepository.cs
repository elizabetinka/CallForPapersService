using CallForPapers.InfrastructureServicesDto;

namespace CallForPapers.Services.RepositoryInterfaces;

public interface IApplicationRepository
{
    Task<Guid?> GetId(ApplicationDto applicationDto);
    
    Task<ApplicationDto>Add(ApplicationDto applicationDto);
    
    Task DeleteById(Guid id);
    
    Task Update(Guid id,ApplicationDto applicationDto);
    
    Task<ApplicationDto?> FindById(Guid id);
    
    Task<IList<ApplicationDto>> GetAll();
}
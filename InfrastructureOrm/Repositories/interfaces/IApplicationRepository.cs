using ModelDTO;

namespace InfrastructureOrm.Repositories.interfaces;

public interface IApplicationRepository
{
    Task<Guid?> GetId(ApplicationDTO applicationDto);
    
    Task DeleteById(Guid id);
    
    Task Update(Guid id,ApplicationDTO applicationDto);
    
    Task<ApplicationDTO?> FindById(Guid id);
    
    Task<IList<ApplicationDTO>> GetAll();
}
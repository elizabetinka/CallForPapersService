using System.Diagnostics;
using InfrastructureOrm.Model;
using ModelDTO;

namespace InfrastructureOrm.Repositories.interfaces;

public interface IAdminRepository
{
    Task <AdminDTO> AddAdmin(string login,string password);
    
    Task<Guid?> GetId(string login,string password);
    
    Task DeleteById(Guid id);

    Task Update(Guid id,string newLogin, string newPassword);
    
    Task<AdminDTO?> FindById(Guid id);


    Task<AdminDTO?> FindByLogin(string login);
    
    Task<IList<AdminDTO>> GetAll();
    
}
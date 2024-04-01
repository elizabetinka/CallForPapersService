using ModelDTO;

namespace InfrastructureOrm.Repositories.interfaces;

public interface IUserRepository
{
    Task<UserDTO?> AddUser(string login,string password);
    
    Task AddUser(UserDTO userDto);
    
    Task<Guid?> GetId(string login,string password);
    
    Task DeleteById(Guid id);
    
    Task Update(Guid id,string newLogin, string newPassword);

    Task<ApplicationDTO> AddApplication(Guid id, ApplicationDTO applicationDto);
    
    Task<UserDTO?> FindById(Guid id);
    
    Task<UserDTO?> FindByLogin(string login);
    Task<IList<UserDTO>> GetAll();
}
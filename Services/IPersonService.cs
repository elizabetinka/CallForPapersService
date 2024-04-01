using System.Threading.Tasks;
using ModelDTO;

namespace Services;

public interface IPersonService
{
    Task<IPerson?> GetByLogin(string login);

    Task<UserDTO?> RegistrUser(string username, string password);
    
    Task<AdminDTO?> RegistrAdmin(string username, string password);
}
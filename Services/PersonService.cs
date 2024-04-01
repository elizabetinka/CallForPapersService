using System;
using System.Threading.Tasks;
using InfrastructureOrm.Repositories.interfaces;
using ModelDTO;

namespace Services;

public class PersonService : IPersonService
{
    private IAdminRepository _adminRepository;
    private IUserRepository _userRepository;

    public PersonService(IAdminRepository applicationRepository, IUserRepository userRepository)
    {
        _adminRepository = applicationRepository;
        _userRepository = userRepository;
    }
    public async Task<IPerson?> GetByLogin(string login)
    {
        
        IPerson? person = await _adminRepository.FindByLogin(login);
        if (person != null)
        {
            return person;
        }
        person = await _userRepository.FindByLogin(login);
        return person;
    }

    public  async Task<UserDTO?> RegistrUser(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        UserDTO? list = (await _userRepository.GetAll()).FirstOrDefault(p=>p.Login==username);
        if (list != null)
        {
            return null;
        }
        
        try
        {
            return await _userRepository.AddUser(username,password);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public  async Task<AdminDTO?> RegistrAdmin(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return null;
        }
        AdminDTO? list = (await _adminRepository.GetAll()).FirstOrDefault(p=>p.Login==username);
        if (list != null)
        {
            return null;
        }
        try
        {
            return await _adminRepository.AddAdmin(username,password);
        }
        catch (Exception _)
        {
            return null;
        }
    }
}
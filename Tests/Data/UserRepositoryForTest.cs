using System.Collections;
using InfrastructureOrm.Model;
using InfrastructureOrm.Model.Activity;
using InfrastructureOrm.Repositories;
using InfrastructureOrm.Repositories.interfaces;
using ModelDTO;

namespace RepositoryTests;

public class UserRepositoryForTest : IUserRepository
{
    public IList<User> Users = new List<User>();
    public IApplicationRepository _applicationRepository;

    public UserRepositoryForTest(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public Task<UserDTO?> AddUser(string login, string password)
    {
        User user = new User { id = Guid.NewGuid(),login = login, password = password };
        Users.Add(user);
        return Task.FromResult(user.UserDto);
    }

    public Task AddUser(UserDTO userDto)
    {
        User user = new User { id = Guid.NewGuid(),login = userDto.Login, password = userDto.Password };
        Users.Add(user);
        return Task.FromResult(user.UserDto);
    }

    public Task<Guid?> GetId(string login, string password)
    {
        User? user = Users.FirstOrDefault(p => p.login == login && p.password == password);
        return Task.FromResult(user?.id);
    }

    public Task DeleteById(Guid id)
    {
        Users.Remove(Users.SingleOrDefault(x => x.id ==id) ?? throw new InvalidOperationException());
        return Task.CompletedTask;
    }

    public Task Update(Guid id, string newLogin, string newPassword)
    {
        User? user = Users.FirstOrDefault(p => p.id == id);
        user = user ?? throw new InvalidOperationException();
        user.login = newLogin;
        user.password = newPassword;
        return Task.CompletedTask;
    }

    public Task<ApplicationDTO> AddApplication(Guid id, ApplicationDTO applicationDto)
    {
        User? user = Users.FirstOrDefault(p => p.id == id);
        user = user ?? throw new InvalidOperationException();
        Application application = new Application
        {
            id = Guid.NewGuid(),
            activity = ActivityMapper.GetActivityClassByString(applicationDto.Activity),
            name = applicationDto.Name,
            added_date = DateTime.Now,
            daft = applicationDto.Daft,
            user_id = id,
            description = applicationDto.Description,
            plan = applicationDto.Plan
        };
        user.applications.Add(application);
        ((ApplicationRepositoryForTest)_applicationRepository).Applications.Add(application);
        return Task.FromResult(application.ApplicationDto);
    }
    

    public Task<UserDTO?> FindById(Guid id)
    {
        User? user = Users.FirstOrDefault(p => p.id == id);
        return Task.FromResult(user?.UserDto);
    }

    public Task<UserDTO?> FindByLogin(string login)
    {
        User? user = Users.FirstOrDefault(p => p.login == login);
        return Task.FromResult(user?.UserDto);
    }

    public Task<IList<UserDTO>> GetAll()
    {
        IList<UserDTO> res = new List<UserDTO>();
        foreach (var us in Users)
        {
           res.Add(us.UserDto);
        }

        return Task.FromResult(res);
    }
}
using InfrastructureOrm.Model;
using InfrastructureOrm.Repositories.interfaces;
using ModelDTO;

namespace RepositoryTests;

public class AdminRepositoryForTest : IAdminRepository
{
    public IList<Admin> Admins = new List<Admin>();
    public Task<AdminDTO> AddAdmin(string login, string password)
    {
        Admin admin = new Admin{ id = Guid.NewGuid(),login = login, password = password };
        Admins.Add(admin);
        return Task.FromResult(admin.AdminDto);
    }

    public Task<Guid?> GetId(string login, string password)
    {
        Admin? admin = Admins.FirstOrDefault(p => p.login == login && p.password == password);
        return Task.FromResult(admin?.id);
    }

    public Task DeleteById(Guid id)
    {
        Admins.Remove(Admins.SingleOrDefault(x => x.id ==id) ?? throw new InvalidOperationException());
        return Task.CompletedTask;
    }

    public Task Update(Guid id, string newLogin, string newPassword)
    {
        Admin? user = Admins.FirstOrDefault(p => p.id == id);
        user = user ?? throw new InvalidOperationException();
        user.login = newLogin;
        user.password = newPassword;
        return Task.CompletedTask;
    }

    public Task<AdminDTO?> FindById(Guid id)
    {
        Admin? user = Admins.FirstOrDefault(p => p.id == id);
        return Task.FromResult(user?.AdminDto);
    }

    public Task<AdminDTO?> FindByLogin(string login)
    {
        Admin? user = Admins.FirstOrDefault(p => p.login == login);
        return Task.FromResult(user?.AdminDto);
    }

    public Task<IList<AdminDTO>> GetAll()
    {
        IList<AdminDTO> res = new List<AdminDTO>();
        foreach (var us in Admins)
        {
            res.Add(us.AdminDto);
        }

        return Task.FromResult(res);
    }
}
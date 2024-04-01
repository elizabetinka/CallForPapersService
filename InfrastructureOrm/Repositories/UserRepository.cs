using InfrastructureOrm.Model;
using InfrastructureOrm.Model.Activity;
using InfrastructureOrm.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using ModelDTO;

namespace InfrastructureOrm.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<UserDTO?> AddUser(string userLogin, string userPassword)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var ad = await db.users.FirstOrDefaultAsync(p => p.login == userLogin);
            if (ad != null)
            {
                throw new Exception("We have user with the same login");
            }
            var user = new User
            {
                login = userLogin,
                password = userPassword  // если успею - захеширую ToDo
            };

            await db.users.AddAsync(user);
            await db.SaveChangesAsync();
            return user.UserDto;
        }
    }

    public async Task AddUser(UserDTO userDto)
    {
        await AddUser(userDto.Login,userDto.Password);
        var id = GetId(userDto.Login, userDto.Password).Result;
        if (id == null){throw new ArgumentNullException("id");}
        await AddApplication((Guid)id,userDto.Applications);
    }

    public async Task<Guid?> GetId(string login, string password)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var user = await db.users.FirstOrDefaultAsync(p => (p.login == login && p.password == password));
            return user?.id;
        }
    }

    public async Task DeleteById(Guid id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var user = await db.users.FindAsync(id);

            if (user == null) return;
            db.users.Remove(user);
            await db.SaveChangesAsync();

        }
    }

    public async Task Update(Guid id, string newLogin, string newPassword)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var user = await db.users.FindAsync(id);
            if (user == null)
            {
                await AddUser(newLogin,newPassword);
                return;
            }

            if (user.login != newLogin)
            {
                var ad = await db.users.FirstOrDefaultAsync(p => p.login == newLogin);
                if (ad != null)
                {
                    throw new Exception("We have user with the same login");
                }
            }

            user.login = newLogin;
            user.password = newPassword;
            await db.SaveChangesAsync();
        }
    }

    public async Task<ApplicationDTO?> AddApplication(Guid id, ApplicationDTO  applicationDto)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var findUser = await db.users.FindAsync(id);
            if (findUser == null)
            {
                throw new Exception("Don't have user with this id");
            }
            
            Application application = new Application
            {
                activity = ActivityMapper.GetActivityClassByString(applicationDto.Activity),
                name = applicationDto.Name,
                added_date = DateTime.Now,
                daft = applicationDto.Daft,
                user = findUser,
                
                description = applicationDto.Description,
                plan = applicationDto.Plan
            };
            db.applications.Add(application);
            
            await db.SaveChangesAsync();
            return application.ApplicationDto;
        }
    }
    public async Task AddApplication(Guid id, IList<ApplicationDTO>  applicationDto)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var findUser = await db.users.FindAsync(id);
            if (findUser == null)
            {
                throw new Exception("Don't have user with this id");
            }

            foreach (var app in applicationDto)
            {
                Application application = new Application
                {
                    activity = ActivityMapper.GetActivityClassByString(app.Activity),
                    name = app.Name,
                    added_date = DateTime.Now,
                    daft = app.Daft,
                    user = findUser
                };
                db.applications.Add(application);
            }
            
            
            await db.SaveChangesAsync();
            return;
        }
    }

    public async Task<UserDTO?> FindById(Guid id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var user = await db.users.Include(c => c.applications).SingleOrDefaultAsync(p => p.id == id);
            return user?.UserDto;
        }
    }
    
    public async Task<UserDTO?> FindByLogin(string login)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var user = await db.users.Include(c => c.applications).SingleOrDefaultAsync(p => p.login== login);
            return user?.UserDto;
        }
    }

    public async Task<IList<UserDTO>> GetAll()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            IList<UserDTO> ans;
            var user = await db.users.ToListAsync();
            ans = new List<UserDTO>(user.Select(a => a.UserDto));
            return ans;
        }
    }
}
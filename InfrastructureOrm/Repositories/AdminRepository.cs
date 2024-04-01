using InfrastructureOrm.Model;
using InfrastructureOrm.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using ModelDTO;

namespace InfrastructureOrm.Repositories;

public class AdminRepository : IAdminRepository
{
    public async Task<AdminDTO> AddAdmin(string adminLogin, string adminPassword)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var ad = await db.admins.FirstOrDefaultAsync(p => p.login == adminLogin);
            if (ad != null)
            {
                throw new Exception("We have admin with the same login");
            }
            var admin = new Admin
            {
                login = adminLogin,
                password = adminPassword  // если успею - захеширую ToDo
            };

            await db.admins.AddAsync(admin);
            await db.SaveChangesAsync();
            return admin.AdminDto;
        }
    }

    public async Task<Guid?> GetId(string login, string password)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var admin = await db.admins.FirstOrDefaultAsync(p => (p.login == login && p.password == password));
            return admin?.id;
        }
    }

    public async Task DeleteById(Guid id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var admin = await db.admins.FindAsync(id);

            if (admin == null) return;
            db.admins.Remove(admin);
            await db.SaveChangesAsync();

        }
    }

    public async Task Update(Guid id, string newLogin, string newPassword)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var admin = await db.admins.FindAsync(id);
            if (admin == null)
            {
                AddAdmin(newLogin,newPassword);
                return;
            }

            if (admin.login != newLogin)
            {
                var ad = await db.admins.FirstOrDefaultAsync(p => p.login == newLogin);
                if (ad != null)
                {
                    throw new Exception("We have admin with the same login");
                }
            }

            admin.login = newLogin;
            admin.password = newPassword;
            await db.SaveChangesAsync();
        }
    }

    public async Task<AdminDTO?> FindById(Guid id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var admin = await db.admins.FindAsync(id);
            return admin?.AdminDto;
        }
    }
    
    public async Task<AdminDTO?> FindByLogin(string login)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var admin = await db.admins.FirstOrDefaultAsync(p => p.login== login);
            return admin?.AdminDto;
        }
    }

    public async Task<IList<AdminDTO>> GetAll()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            IList<AdminDTO> ans;
            var admin = await db.admins.ToListAsync();
            ans = new List<AdminDTO>(admin.Select(a => a.AdminDto));
            return ans;
        }
    }

}
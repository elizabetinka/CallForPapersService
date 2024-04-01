using InfrastructureOrm.Model.Activity;
using InfrastructureOrm.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using ModelDTO;


namespace InfrastructureOrm.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    public async Task<Guid?> GetId(ApplicationDTO applicationDto)
    {
        if (applicationDto.Id != null){return applicationDto.Id;}
        using (ApplicationContext db = new ApplicationContext())
        {
   
            var application = await db.applications.FirstOrDefaultAsync(p => (( (p.activity is NullActivity && applicationDto.Activity == null) || (!(p.activity is NullActivity) && p.activity.activity == applicationDto.Activity)) && p.name == applicationDto.Name && p.description == applicationDto.Description && p.plan == applicationDto.Plan && p.daft == applicationDto.Daft));
            return application?.id;
        }
    }

    public async Task DeleteById(Guid id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var application = await db.applications.FindAsync(id);

            if (application== null) return;
            db.applications.Remove(application);
            await db.SaveChangesAsync();

        }
    }

    public async Task Update(Guid id, ApplicationDTO applicationDto)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var application = await db.applications.FindAsync(id);
            if (application == null)
            {
                throw new Exception("We have don't have application with this id");
            }

            application.activity = ActivityMapper.GetActivityClassByString(applicationDto.Activity);
            application.daft = applicationDto.Daft;
            application.description = applicationDto.Description;
            application.name = applicationDto.Name;
            application.plan = applicationDto.Plan;
            if (applicationDto.AddedDate != null)
            {
                application.added_date = (DateTime)applicationDto.AddedDate;
            }
            
            
            await db.SaveChangesAsync();
        }
    }

    public async Task<ApplicationDTO?>FindById(Guid id)
    {
        
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            var application = await db.applications.FindAsync(id);
            return application?.ApplicationDto;
        }
    }

    public async Task<IList<ApplicationDTO>> GetAll()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            IList<ApplicationDTO> ans;
            var applications = await db.applications.ToListAsync();
            ans = new List<ApplicationDTO>(applications.Select(a => a.ApplicationDto));
            return ans;
        }
    }
}
using CallForPapers.Infrastructure.Model;
using CallForPapers.Infrastructure.Model.Activity;
using CallForPapers.InfrastructureServicesDto;
using CallForPapers.Services;
using CallForPapers.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CallForPapers.Infrastructure.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private IDbContextFactory<ApplicationContext> _dbContextFactory;

    public ApplicationRepository(IDbContextFactory<ApplicationContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Guid?> GetId(ApplicationDto applicationDto)
    {
        if (applicationDto.Id != null)
        {
            return applicationDto.Id;
        }

        var db = await _dbContextFactory.CreateDbContextAsync();
        var application = await db.Applications.FirstOrDefaultAsync(p =>
            (((p.Activity == null && applicationDto.Activity == null) ||
              (p.Activity != null && p.Activity.Activity == applicationDto.Activity)) &&
             p.Name == applicationDto.Name && p.Description == applicationDto.Description &&
             p.Plan == applicationDto.Plan));
        return application?.Id;
    }

    public async Task<ApplicationDto> Add(ApplicationDto applicationDto)
    {
        if (applicationDto.UserId == null)
        {
            throw new CallForPaperBackendException("author Id must be not null");
        }

        var db = await _dbContextFactory.CreateDbContextAsync();
        Application application = new Application
        {
            Activity = ActivityMapper.GetActivityClassByString(applicationDto.Activity),
            Name = applicationDto.Name,
            SendDate = null,
            CreateDate = DateTime.Now,
            UserId = (Guid)applicationDto.UserId,
            Description = applicationDto.Description,
            Plan = applicationDto.Plan
        };
        db.Applications.Add(application);

        await db.SaveChangesAsync();
        return application.ApplicationDto;
    }

    public async Task DeleteById(Guid id)
    {
        var db = await _dbContextFactory.CreateDbContextAsync();
        var application = await db.Applications.FindAsync(id);
        if (application == null)
        {
            throw new CallForPaperBackendException("We have don't have application with this Id");
        }

        db.Applications.Remove(application);
        await db.SaveChangesAsync();
    }


    public async Task Update(Guid id, ApplicationDto applicationDto)
    {
        var db = await _dbContextFactory.CreateDbContextAsync();
        var application = await db.Applications.FindAsync(id);
        if (application == null)
        {
            throw new CallForPaperBackendException("We have don't have application with this Id");
        }

        application.Activity = ActivityMapper.GetActivityClassByString(applicationDto.Activity);
        application.Description = applicationDto.Description;
        application.Name = applicationDto.Name;
        application.Plan = applicationDto.Plan;
        application.SendDate = applicationDto.SendDate;

        await db.SaveChangesAsync();
    }

    public async Task<ApplicationDto?> FindById(Guid id)
    {
        var db = await _dbContextFactory.CreateDbContextAsync();
        var application = await db.Applications.FindAsync(id);
        return application?.ApplicationDto;
    }

    public async Task<IList<ApplicationDto>> GetAll()
    {
        var db = await _dbContextFactory.CreateDbContextAsync();
        IList<ApplicationDto> ans;
        var applications = await db.Applications.ToListAsync();
        ans = new List<ApplicationDto>(applications.Select(a => a.ApplicationDto));
        return ans;
    }
}
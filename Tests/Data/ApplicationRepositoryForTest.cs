using CallForPapers.Infrastructure.Model;
using CallForPapers.Infrastructure.Model.Activity;
using CallForPapers.InfrastructureServicesDto;
using CallForPapers.Services.RepositoryInterfaces;


namespace RepositoryTests;

public class ApplicationRepositoryForTest : IApplicationRepository
{
    public IList<Application> Applications = new List<Application>();

    public Task<Guid?> GetId(ApplicationDto applicationDto)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationDto> Add(ApplicationDto applicationDto)
    {
        Application application = new Application
        {
            Id = Guid.NewGuid(),
            Activity = ActivityMapper.GetActivityClassByString(applicationDto.Activity),
            Name = applicationDto.Name,
            SendDate = null,
            CreateDate = DateTime.Now,
            UserId = (Guid)applicationDto.UserId,
            Description = applicationDto.Description,
            Plan = applicationDto.Plan
        };
        Applications.Add(application);
        return  Task.FromResult(application.ApplicationDto);
    }

    public Task DeleteById(Guid id)
    {
        Applications.Remove(Applications.SingleOrDefault(x => x.Id ==id) ?? throw new InvalidOperationException());
        return Task.CompletedTask;
    }

    public Task Update(Guid id, ApplicationDto applicationDto)
    {
        Application? application = Applications.FirstOrDefault(p => p.Id == id);
        application = application ?? throw new InvalidOperationException();
        application.Activity = ActivityMapper.GetActivityClassByString(applicationDto.Activity);
        application.Description = applicationDto.Description;
         application.Plan = applicationDto.Plan;
         application.Name = applicationDto.Name;
         application.CreateDate = (DateTime)applicationDto.CreateDate;
         application.SendDate = applicationDto.SendDate;
        return Task.CompletedTask;
    }

    public Task<ApplicationDto?> FindById(Guid id)
    {
        return Task.FromResult(Applications.FirstOrDefault(x => x.Id ==id)?.ApplicationDto);
    }

    public Task<IList<ApplicationDto>> GetAll()
    {
        IList<ApplicationDto> res = new List<ApplicationDto>();
        foreach (var us in Applications)
        {
            res.Add(us.ApplicationDto);
        }

        return Task.FromResult(res);
    }
}
using InfrastructureOrm.Model;
using InfrastructureOrm.Model.Activity;
using InfrastructureOrm.Repositories.interfaces;
using ModelDTO;

namespace RepositoryTests;

public class ApplicationRepositoryForTest : IApplicationRepository
{
    public IList<Application> Applications = new List<Application>();

    public Task<Guid?> GetId(ApplicationDTO applicationDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteById(Guid id)
    {
        Applications.Remove(Applications.SingleOrDefault(x => x.id ==id) ?? throw new InvalidOperationException());
        return Task.CompletedTask;
    }

    public Task Update(Guid id, ApplicationDTO applicationDto)
    {
        Application? application = Applications.FirstOrDefault(p => p.id == id);
        application = application ?? throw new InvalidOperationException();
        application.activity = ActivityMapper.GetActivityClassByString(applicationDto.Activity);
        application.description = applicationDto.Description;
         application.plan = applicationDto.Plan;
         application.name = applicationDto.Name;
         application.daft = applicationDto.Daft;
        return Task.CompletedTask;
    }

    public Task<ApplicationDTO?> FindById(Guid id)
    {
        return Task.FromResult(Applications.FirstOrDefault(x => x.id ==id)?.ApplicationDto);
    }

    public Task<IList<ApplicationDTO>> GetAll()
    {
        IList<ApplicationDTO> res = new List<ApplicationDTO>();
        foreach (var us in Applications)
        {
            res.Add(us.ApplicationDto);
        }

        return Task.FromResult(res);
    }
}
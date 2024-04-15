using CallForPapers.Infrastructure.Model.Activity;
using CallForPapers.InfrastructureServicesDto;
using CallForPapers.Services.RepositoryInterfaces;

namespace CallForPapers.Infrastructure.Repositories;

public class ActivityRepository : IActivityRepository
{
    public IList<ActivityDto> GetAll()
    {
        return new List<ActivityDto>(new ActivityDto[]
        {
            new ReportActivity().ActivityDto, new Model.Activity.DiscussionActivity().ActivityDto,
            new MasterClassActivity().ActivityDto
        });
    }

    public bool ExistsItsActivity(string? activity)
    {
        return (ActivityMapper.GetActivityClassByString(activity) != null);
    }
}
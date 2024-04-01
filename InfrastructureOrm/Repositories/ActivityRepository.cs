using InfrastructureOrm.Model;
using InfrastructureOrm.Model.Activity;
using InfrastructureOrm.Repositories.interfaces;
using ModelDTO;
using DiscussionActivity = InfrastructureOrm.Model.Activity.DiscussionActivity;

namespace InfrastructureOrm.Repositories;

public static class ActivityRepository
{
    public static IList<ActivityDTO> GetAll()
    {
        return new List<ActivityDTO>(new ActivityDTO[]
            { new ReportActivity().ActivityDto, new DiscussionActivity().ActivityDto, new MasterClassActivity().ActivityDto });
    }
}
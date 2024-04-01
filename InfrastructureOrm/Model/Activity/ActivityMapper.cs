using System.Diagnostics;
using InfrastructureOrm.Repositories;
using ModelDTO;

namespace InfrastructureOrm.Model.Activity;

public static class ActivityMapper
{
    public static ActivityClass GetActivityClassByString(string? name)
    {
        if (name == null)
        {
            return new NullActivity();}
        var all = new List<ActivityClass>(new ActivityClass[]
            { new ReportActivity(), new DiscussionActivity(), new MasterClassActivity() });
        return all.FirstOrDefault(active => active.activity == name) ?? new NullActivity();
    }
    
    
}
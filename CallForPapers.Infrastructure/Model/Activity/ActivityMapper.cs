using System.Diagnostics;
using CallForPapers.Infrastructure.Repositories;
using CallForPapers.Services.RepositoryInterfaces;

namespace CallForPapers.Infrastructure.Model.Activity;

public static class ActivityMapper
{
    public static ActivityClass? GetActivityClassByString(string? name)
    {
        if (name == null)
        {
            return null;
            
        }
        var all = new List<ActivityClass>(new ActivityClass[]
            { new ReportActivity(), new DiscussionActivity(), new MasterClassActivity() });
        return all.FirstOrDefault(active => active.Activity == name);
    }
    
    
}
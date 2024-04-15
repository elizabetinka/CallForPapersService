namespace CallForPapers.InfrastructureServicesDto;

public class ActivityDto
{
    public string Activity  { get; set; }
    
    public string Description  { get; set; }

    public ActivityDto(string activity, string description)
    {
        Activity = activity;
        Description = description;
    }
}
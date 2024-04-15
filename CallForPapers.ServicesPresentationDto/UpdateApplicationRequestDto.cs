namespace CallForPapers.ServicesPresentationDto;

public class UpdateApplicationRequestDto
{
    public string? Activity { get;set; }
    
    public string? Name  { get; set; }
    
    public string? Description  { get; set;}
    
    public string? Outline  { get; set; }

    public UpdateApplicationRequestDto(string? activity, string? name, string? description, string? outline)
    {
        Activity = activity;
        Name = name;
        Description = description;
        Outline = outline;
    }
}
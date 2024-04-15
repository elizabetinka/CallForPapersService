namespace CallForPapers.ServicesPresentationDto;

public class CreateApplicationRequestDto
{
    public Guid? Author { get;set; }
    
    public string? Activity { get;set; }
    
    public string? Name  { get; set; }
    
    public string? Description  { get; set;}
    
    public string? Outline  { get; set; }

    public CreateApplicationRequestDto(Guid? author, string? activity, string? name, string? description, string? outline)
    {
        Author = author;
        Activity = activity;
        Name = name;
        Description = description;
        Outline = outline;
    }
}
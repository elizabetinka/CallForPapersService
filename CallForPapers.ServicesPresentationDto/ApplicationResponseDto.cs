namespace CallForPapers.ServicesPresentationDto;

public class ApplicationResponseDto
{
    public Guid? Id { get;set; }
    
    public Guid? Author { get;set; }
    
    public string? Activity { get;set; }
    
    public string? Name  { get; set; }
    
    public string? Description  { get; set;}
    
    public string? Outline  { get; set; }

    public ApplicationResponseDto(Guid id, Guid author, string? activity, string? name, string? description, string? outline)
    {
        Id = id;
        Author = author;
        Activity = activity;
        Name = name;
        Description = description;
        Outline = outline;
    }

    public ApplicationResponseDto()
    {
    }
}
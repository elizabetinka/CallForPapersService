namespace CallForPapers.InfrastructureServicesDto;

public class ApplicationDto
{
    public Guid? Id { get; }
    
    public string? Activity { get;set; }
    
    public string? Name  { get; set; }
    
    public string? Description  { get; set;}
    
    public string? Plan  { get; set; }
    public DateTime? CreateDate { get; set; } 
    
    public DateTime? SendDate { get; set; } 
    
    public Guid? UserId { get; set; }

    public ApplicationDto(Guid? id, string? activity, string? name, string? description, string? plan, DateTime? createDate,DateTime? sendDate, Guid? userId)
    {
        Id = id;
        Activity = activity;
        Name = name;
        Description = description;
        Plan = plan;
        CreateDate = createDate;
        SendDate = sendDate;
        UserId = userId;
    }

    public ApplicationDto(){}
}
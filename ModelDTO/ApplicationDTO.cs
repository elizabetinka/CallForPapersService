using System;

namespace ModelDTO;



public class ApplicationDTO
{
    public Guid? Id { get; }
    
    public string? Activity { get;set; }
    
    public string? Name  { get; set; }
    
    public string? Description  { get; set;}
    
    public string? Plan  { get; set; }
    public DateTime? AddedDate { get; set; } 
    
    public Guid? UserId { get; set; }

    public Boolean Daft { get; set; } = true;

    public ApplicationDTO(Guid? id, string? activity, string? name, string? description, string? plan, DateTime addedDate, Guid? userId, Boolean daft)
    {
        Id = id;
        Activity = activity;
        Name = name;
        Description = description;
        Plan = plan;
        AddedDate = addedDate;
        UserId = userId;
        Daft = daft;
    }

    public ApplicationDTO(){}
}
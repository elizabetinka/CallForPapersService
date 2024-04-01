using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModelDTO;

namespace InfrastructureOrm.Model.Activity;

[Owned]
public class ActivityClass
{
    [Required]
    public string? activity  { get; set; }
    
    [NotMapped]
    public string description  { get; set; } = String.Empty;
    
    [NotMapped]
    public ActivityDTO ActivityDto
    {
        get => new ActivityDTO(activity, description);
    }
}
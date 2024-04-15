using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CallForPapers.InfrastructureServicesDto;
using Microsoft.EntityFrameworkCore;

namespace CallForPapers.Infrastructure.Model.Activity;

[Owned]
public class ActivityClass
{
    [Required]
    [Column("activity")]
    public string? Activity  { get; set; }
    
    [NotMapped]
    public string Description  { get; set; } = String.Empty;
    
    [NotMapped]
    public ActivityDto ActivityDto
    {
        get => new ActivityDto(Activity, Description);
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CallForPapers.Infrastructure.Model.Activity;
using CallForPapers.InfrastructureServicesDto;

namespace CallForPapers.Infrastructure.Model;

public class Application
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("activity")]
    public ActivityClass? Activity { get; set; } = null;

  
    [MaxLength(100), Column("name",TypeName = "varchar(100)")]
    public string? Name { get; set; }

    [MaxLength(300), Column("description",TypeName = "varchar(300)")]
    public string? Description { get; set; }

    [MaxLength(1000), Column("plan",TypeName = "varchar(1000)")]
    public string? Plan { get; set; }

    [Column("create_data")]
    public DateTime CreateDate { get; set; } = DateTime.Now;

    [Column("send_data")]
    public DateTime? SendDate { get; set; } = null;

    [Column("user_id")]
    public Guid UserId { get; set; }


    [NotMapped]
    public ApplicationDto ApplicationDto
    {
        get
        {
            if (Activity != null)
            {
                return new ApplicationDto(Id, Activity.Activity, Name, Description, Plan, CreateDate, SendDate,
                    UserId);
            }

            return new ApplicationDto(Id, null, Name, Description, Plan, CreateDate, SendDate, UserId);
        }
    }
}
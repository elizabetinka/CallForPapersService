using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InfrastructureOrm.Model.Activity;
using ModelDTO;
using Activity = System.Diagnostics.Activity;

namespace InfrastructureOrm.Model;

// зависимая сущность
public class Application
{
    public Guid id { get; set; }

    public ActivityClass activity { get; set; } = new NullActivity();
    
    [MaxLength(100), Column(TypeName = "varchar(100)")]
    public string? name  { get; set; }
    
    [MaxLength(300), Column(TypeName = "varchar(300)")]
    public string? description  { get; set; }
    
    [MaxLength(1000), Column(TypeName = "varchar(1000)")]
    public string? plan  { get; set; }
    public DateTime added_date { get; set; } = DateTime.Now;  // если daft=false, то показывает дату отправления

    public Boolean daft { get; set; } = true;
    
    public Guid user_id { get; set; } // внешний ключ
    public User user { get; set; } // навигационное свойство
    
    [NotMapped]
    public ApplicationDTO ApplicationDto
    {
        get => new ApplicationDTO(id, activity.activity, name, description, plan, added_date, user_id,daft);
    }
}
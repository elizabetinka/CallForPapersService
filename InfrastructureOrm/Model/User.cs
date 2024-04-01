using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModelDTO;

namespace InfrastructureOrm.Model;

// главная сущность
public class User
{
    public Guid id { get; set; }
    
    [Required]
    public string? password { get; set; }
    
    [Required]
    public string? login { get; set; }

    public IList<Application> applications { get; set; } = new List<Application>(); // навигационное свойство


    [NotMapped]
    public UserDTO UserDto
    {
        get => new UserDTO(id, password, login, new List<ApplicationDTO>(applications.Select(p => p.ApplicationDto)));
    }
}

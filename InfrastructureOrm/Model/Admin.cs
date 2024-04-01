using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModelDTO;

namespace InfrastructureOrm.Model;

public class Admin
{
    public Guid id { get; set; }
    
    [Required]
    public string? password { get; set; }
    
    [Required]
    public string? login { get; set; }
    
    [NotMapped]
    public AdminDTO AdminDto
    {
        get => new AdminDTO(id, password, login);
    }
}
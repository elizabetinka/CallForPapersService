using System.Threading.Tasks;
using InfrastructureOrm.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelDTO;
using Services;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApplication1.Controllers;


[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private IPersonService _service;

    public AccountController(IPersonService service)
    {
        _service = service;
    }
    
    [HttpPost("user/")]
    [SwaggerOperation("registr user")]
    public  async Task<UserDTO?> RegistrUser(string username,string password)
    {
        return await _service.RegistrUser(username, password);
    }
    [Authorize(Roles = "admin")]
    [HttpPost("admin/")]
    [SwaggerOperation("registr admin")]
    public async Task<AdminDTO?> RegistrAdmin(string username,string password)
    {
        return await _service.RegistrAdmin(username, password);
    }
    
}
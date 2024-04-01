using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelDTO;
using Services;
using Swashbuckle.AspNetCore.Annotations;
using WebApplication1.Forms;

namespace WebApplication1.Controllers;


[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private ICallForPaperService _service;

    public UsersController(ICallForPaperService service)
    {
        _service = service;
    }
    
    [HttpGet("{id:guid}/currentapplication")]
    [SwaggerOperation("get not submitted application by user id")]
    public async Task<ApplicationSeniorForm> GetApplicationById(Guid id)
    {
        return Mapper.ToApplicationSeniorForm(await _service.GetApplicationByUserId(id));
    }
    

    
}
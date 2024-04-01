using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ModelDTO;
using Services;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApplication1.Controllers;


[ApiController]
[Route("[controller]")]
public class ActivitiesController : ControllerBase
{
    private ICallForPaperService _service;

    public ActivitiesController(ICallForPaperService service)
    {
        _service = service;
    }
    [HttpGet]
    [SwaggerOperation("get availible activities")]
    public IList<ActivityDTO> GetAvailibleActivities()
    {
        return _service.GetAvailibleActivities();
    }
}
using System;
using System.Collections.Generic;
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
public class ApplicationsController: ControllerBase
{
    private ICallForPaperService _service;

    public ApplicationsController(ICallForPaperService service)
    {
        _service = service;
    }
    [HttpGet]
    [SwaggerOperation("applications with the specified date")]
    public async Task<IList<ApplicationSeniorForm>?> GetApplicationByTime(
        [FromQuery(Name = "submittedAfter")] string? submittedAfter,
        [FromQuery(Name = "unsubmittedOlder")] string? unsubmittedOlder)
    {
        if ((submittedAfter == null) == (unsubmittedOlder == null))
        {
            return null;
        }
        if (!DateTime.TryParse((submittedAfter ?? unsubmittedOlder)?.Trim('"'),CultureInfo.InvariantCulture,DateTimeStyles.None,out var dateTime))
        {
            return null;
        }
        
        
        if (submittedAfter != null)
        {
            return  Mapper.ToApplicationSeniorForm(await _service.GetApplicationSubmittedAfter(dateTime));
        }
      
        return  Mapper.ToApplicationSeniorForm(await _service.GetApplicationunsubmittedOlder(dateTime));
        
    }
    
    [HttpGet("{id:guid}")]
    [SwaggerOperation("applications by id")]
    public async Task<ApplicationSeniorForm> GetApplicationById(Guid id)
    {
        ApplicationDTO? applicationDto = await _service.GetApplicationById(id);
        return Mapper.ToApplicationSeniorForm(applicationDto);
    }
    
    [HttpPost("{id:guid}/submit")]
    [SwaggerOperation("send applications by id")]
    public async Task<ActionResult> SendApplication(Guid id)
    {
        if (await _service.SendApplication(id))
        {
            return Ok();
        }

        return BadRequest();
    }
    
    [HttpDelete("{id:guid}")]
    [SwaggerOperation("delete applications by id")]
    public async Task<ActionResult> DeleteApplication(Guid id)
    {
        if (await _service.DeleteApplication(id))
        {
            return Ok();
        }

        return BadRequest();
    }
    
    [HttpPut("{id:guid}")]
    [SwaggerOperation("update applications by id")]
    public async Task<ApplicationSeniorForm> UpdateApplication(Guid id,ApplicationBaseForm applicationDto)
    {
        if (string.IsNullOrEmpty(applicationDto.Description) || string.IsNullOrWhiteSpace(applicationDto.Description))
        {
            applicationDto.Description = null;
        }
        if (string.IsNullOrEmpty(applicationDto.Activity) || string.IsNullOrWhiteSpace(applicationDto.Activity))
        {
            applicationDto.Activity = null;
        }
        if (string.IsNullOrEmpty(applicationDto.Name) || string.IsNullOrWhiteSpace(applicationDto.Name))
        {
            applicationDto.Name = null;
        }
        if (string.IsNullOrEmpty(applicationDto.Outline) || string.IsNullOrWhiteSpace(applicationDto.Outline))
        {
            applicationDto.Outline = null;
        }
        return Mapper.ToApplicationSeniorForm(await _service.UpdateApplication(id, new ApplicationDTO(null,applicationDto.Activity,applicationDto.Name,applicationDto.Description,applicationDto.Outline,DateTime.Now, null,true)));
    }
    
    [HttpPost()]
    [SwaggerOperation("create application")]
    public async Task<ApplicationSeniorForm> CreateApplication(ApplicationMiddleForm applicationDto)
    {
        if (string.IsNullOrEmpty(applicationDto.Description) || string.IsNullOrWhiteSpace(applicationDto.Description))
        {
            applicationDto.Description = null;
        }
        if (string.IsNullOrEmpty(applicationDto.Activity) || string.IsNullOrWhiteSpace(applicationDto.Activity))
        {
            applicationDto.Activity = null;
        }
        if (string.IsNullOrEmpty(applicationDto.Name) || string.IsNullOrWhiteSpace(applicationDto.Name))
        {
            applicationDto.Name = null;
        }
        if (string.IsNullOrEmpty(applicationDto.Outline) || string.IsNullOrWhiteSpace(applicationDto.Outline))
        {
            applicationDto.Outline = null;
        }
        
        return Mapper.ToApplicationSeniorForm( await _service.CreateApplication(new ApplicationDTO(null,applicationDto.Activity,applicationDto.Name,applicationDto.Description,applicationDto.Outline,DateTime.Now, applicationDto.Author,true)));
    }
    
    
}
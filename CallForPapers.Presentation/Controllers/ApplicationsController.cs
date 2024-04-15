using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using CallForPapers.Services;
using CallForPapers.ServicesPresentationDto;
using Swashbuckle.AspNetCore.Annotations;


namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationsController : ControllerBase
{
    private ICallForPaperService _service;

    public ApplicationsController(ICallForPaperService service)
    {
        _service = service;
    }

    [HttpGet("{Id:guid}/currentapplication")]
    [SwaggerOperation("get not submitted application by user Id")]
    public async Task<ApplicationResponseDto?> GetApplicationByUserId(Guid id)
    {
        return await _service.GetApplicationByUserId(id);
    }

    [HttpGet]
    [SwaggerOperation("applications with the specified date")]
    public async Task<IList<ApplicationResponseDto>?> GetApplicationByTime(
        [FromQuery(Name = "submittedAfter")] string? submittedAfter,
        [FromQuery(Name = "unsubmittedOlder")] string? unsubmittedOlder)
    {
        if ((submittedAfter == null) == (unsubmittedOlder == null))
        {
            throw new ArgumentException("you can't request submittedAfter and unsubmittedOlder in the same time");
        }

        if (!DateTime.TryParse((submittedAfter ?? unsubmittedOlder)?.Trim('"'), CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dateTime))
        {
            throw new ArgumentException("data format no valid");
        }

        if (submittedAfter != null)
        {
            return await _service.GetApplicationSendAfter(dateTime);
        }

        return await _service.GetApplicationsNotSendOlder(dateTime);
    }
    
    [HttpGet("{Id:guid}")]
    [SwaggerOperation("applications by Id")]
    public async Task<ApplicationResponseDto?> GetApplicationById(Guid id)
    {
        return await _service.GetApplicationById(id);
    }

    [HttpPost("{Id:guid}/submit")]
    [SwaggerOperation("send applications by Id")]
    public async Task SendApplication(Guid id)
    {
        await _service.SendApplication(id);
    }

    [HttpDelete("{Id:guid}")]
    [SwaggerOperation("delete applications by Id")]
    public async Task DeleteApplication(Guid id)
    {
        await _service.DeleteApplication(id);
    }

    [HttpPut("{Id:guid}")]
    [SwaggerOperation("update applications by Id")]
    public async Task<ApplicationResponseDto> UpdateApplication(Guid id, UpdateApplicationRequestDto applicationDto)
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

        return await _service.UpdateApplication(id, applicationDto);
    }

    [HttpPost()]
    [SwaggerOperation("create application")]
    public async Task<ApplicationResponseDto> CreateApplication(CreateApplicationRequestDto applicationDto)
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

        return await _service.CreateApplication(applicationDto);
    }
}
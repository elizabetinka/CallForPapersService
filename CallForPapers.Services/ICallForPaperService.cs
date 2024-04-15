using CallForPapers.InfrastructureServicesDto;
using CallForPapers.ServicesPresentationDto;


namespace CallForPapers.Services;

public interface ICallForPaperService
{
    IList<ActivityDto> GetAvailibleActivities();

    Task<ApplicationResponseDto?> GetApplicationByUserId(Guid id);

    Task<IList<ApplicationResponseDto>> GetApplicationSendAfter(DateTime dateTime);

    Task<ApplicationResponseDto?> GetApplicationById(Guid id);

    Task<IList<ApplicationResponseDto>> GetApplicationsNotSendOlder(DateTime dateTime);
    
    Task SendApplication(Guid id);

    Task DeleteApplication(Guid id);

    Task<ApplicationResponseDto> UpdateApplication(Guid id, UpdateApplicationRequestDto applicationDto);

    Task<ApplicationResponseDto> CreateApplication(CreateApplicationRequestDto applicationDto);
}
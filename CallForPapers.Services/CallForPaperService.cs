using CallForPapers.InfrastructureServicesDto;
using CallForPapers.Services.RepositoryInterfaces;
using CallForPapers.ServicesPresentationDto;


namespace CallForPapers.Services;

public class CallForPaperService : ICallForPaperService
{
    private IApplicationRepository _applicationRepository;
    private IActivityRepository _activityRepository;

    public CallForPaperService(IApplicationRepository applicationRepository, IActivityRepository activityRepository)
    {
        _applicationRepository = applicationRepository;
        _activityRepository = activityRepository;
    }

    public IList<ActivityDto> GetAvailibleActivities()
    {
        return _activityRepository.GetAll();
    }

    public async Task<ApplicationResponseDto?> GetApplicationByUserId(Guid id)
    {
        var applicationDtos = await _applicationRepository.GetAll();

        return Mapper.ToApplicationResponse(
            applicationDtos.FirstOrDefault(p => (p.SendDate == null && p.UserId == id)));
    }

    public async Task<IList<ApplicationResponseDto>> GetApplicationSendAfter(DateTime dateTime)
    {
        var applicationDtos = await _applicationRepository.GetAll();
        applicationDtos =
            new List<ApplicationDto>(applicationDtos.Where(p => (p.SendDate != null && p.SendDate > dateTime)));
        return Mapper.ToApplicationResponses(applicationDtos);
    }

    public async Task<ApplicationResponseDto?> GetApplicationById(Guid id)
    {
        return Mapper.ToApplicationResponse(await _applicationRepository.FindById(id));
    }

    public async Task<IList<ApplicationResponseDto>> GetApplicationsNotSendOlder(DateTime dateTime)
    {
        var applicationDtos = await _applicationRepository.GetAll();
        applicationDtos =
            new List<ApplicationDto>(applicationDtos.Where(p => (p.SendDate == null && p.CreateDate < dateTime)));
        return Mapper.ToApplicationResponses(applicationDtos);
    }

    public async Task SendApplication(Guid id)
    {
        ApplicationDto? applicationDto = await _applicationRepository.FindById(id);
        if (applicationDto == null)
        {
            throw new CallForPaperBackendException("we know nothing about application with this id");
        }

        if (applicationDto.SendDate != null)
        {
            throw new CallForPaperBackendException("you can't send sent applications");
        }


        if (applicationDto.UserId == null || !_activityRepository.ExistsItsActivity(applicationDto.Activity) ||
            applicationDto.Name == null ||
            applicationDto.Plan == null)
        {
            throw new CallForPaperBackendException("you can't send applications without required fields");
        }

        applicationDto.SendDate = DateTime.Now;
        await _applicationRepository.Update(id, applicationDto);
    }

    public async Task DeleteApplication(Guid id)
    {
        var applicationDto = await _applicationRepository.FindById(id);
        if (applicationDto == null)
        {
            throw new CallForPaperBackendException("we know nothing about application with this id");
        }

        if (applicationDto.SendDate != null)
        {
            throw new CallForPaperBackendException("you can't delete sent applications");
        }

        await _applicationRepository.DeleteById(id);
    }

    public async Task<ApplicationResponseDto> UpdateApplication(Guid id, UpdateApplicationRequestDto newApplicationDto)
    {
        ApplicationDto? applicationDto = await _applicationRepository.FindById(id);
        if (applicationDto == null)
        {
            throw new CallForPaperBackendException("we know nothing about application with this id");
        }

        if (applicationDto.SendDate != null)
        {
            throw new CallForPaperBackendException("you can't edit sent applications");
        }

        if (newApplicationDto.Activity != null && !_activityRepository.ExistsItsActivity(newApplicationDto.Activity))
        {
            throw new ArgumentException("Activity class is not supported");
        }

        if (string.IsNullOrWhiteSpace(newApplicationDto.Name) &&
            string.IsNullOrWhiteSpace(newApplicationDto.Description) &&
            string.IsNullOrWhiteSpace(newApplicationDto.Outline) && newApplicationDto.Activity == null)
        {
            throw new ArgumentException("At least one field besides user's id must be not null");
        }

        applicationDto.Name = newApplicationDto.Name;
        applicationDto.Description = newApplicationDto.Description;
        applicationDto.Plan = newApplicationDto.Outline;
        applicationDto.Activity = newApplicationDto.Activity;

        await _applicationRepository.Update(id, applicationDto);
        ApplicationDto? result = await _applicationRepository.FindById(id);
        if (result == null)
        {
            throw new Exception("application after update has disappeared");
        }

        return Mapper.ToApplicationResponse(result);
    }

    public async Task<ApplicationResponseDto> CreateApplication(CreateApplicationRequestDto applicationDto)
    {
        if (applicationDto.Author == null)
        {
            throw new ArgumentException("user's id must be not null");
        }

        if (applicationDto.Activity != null && !_activityRepository.ExistsItsActivity(applicationDto.Activity))
        {
            throw new ArgumentException("Activity class is not supported");
        }

        if (string.IsNullOrWhiteSpace(applicationDto.Name) && string.IsNullOrWhiteSpace(applicationDto.Description) &&
            string.IsNullOrWhiteSpace(applicationDto.Outline) && applicationDto.Activity == null)
        {
            throw new ArgumentException("At least one field besides user's id must be not null");
        }

        if (await GetApplicationByUserId((Guid)applicationDto.Author) != null)
        {
            throw new CallForPaperBackendException("user can have the only one not sent application");
        }

        return Mapper.ToApplicationResponse(await _applicationRepository.Add(new ApplicationDto(null,
            applicationDto.Activity, applicationDto.Name, applicationDto.Description, applicationDto.Outline,
            DateTime.Now, null, applicationDto.Author)));
    }
}
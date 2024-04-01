using InfrastructureOrm.Model.Activity;
using InfrastructureOrm.Repositories;
using ModelDTO;
using InfrastructureOrm.Repositories.interfaces;

namespace Services;

public class CallForPaperService : ICallForPaperService
{
    private IApplicationRepository _applicationRepository;
    private IUserRepository _userRepository;

    public CallForPaperService(IApplicationRepository applicationRepository, IUserRepository userRepository)
    {
        _applicationRepository = applicationRepository;
        _userRepository = userRepository;
    }

    public IList<ActivityDTO> GetAvailibleActivities()
    {
        return ActivityRepository.GetAll();
    }

    public async Task<ApplicationDTO?> GetApplicationByUserId(Guid id)
    {
        var user = await _userRepository.FindById(id);
        return user?.Applications.FirstOrDefault(p=>p.Daft);
    }

    public async Task<IList<ApplicationDTO>> GetApplicationSubmittedAfter(DateTime dateTime)
    {
        var applicationDtos = await _applicationRepository.GetAll();
        return new List<ApplicationDTO>(applicationDtos.Where(p => (!p.Daft && p.AddedDate > dateTime)));
    }

    public async Task<ApplicationDTO?> GetApplicationById(Guid id)
    {
        return await _applicationRepository.FindById(id);
    }

    public async Task<IList<ApplicationDTO>> GetApplicationunsubmittedOlder(DateTime dateTime)
    {
        var applicationDtos = await _applicationRepository.GetAll();
        return new List<ApplicationDTO>(applicationDtos.Where(p => (p.Daft && p.AddedDate > dateTime)));
    }

    public async Task<Boolean> SendApplication(Guid id)
    {
        ApplicationDTO? applicationDto= await _applicationRepository.FindById(id);
        if (applicationDto == null)
        {
            return false;
        }

        if (!applicationDto.Daft)
        {
            return false;
        }
        

        if (applicationDto.UserId == null || ActivityMapper.GetActivityClassByString(applicationDto.Activity) is NullActivity || applicationDto.Name == null ||
            applicationDto.Plan == null)
        {
            return false;
        }

        applicationDto.Daft = false;
        applicationDto.AddedDate=DateTime.Now;
        try
        {
            await _applicationRepository.Update(id,applicationDto);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<Boolean> DeleteApplication(Guid id)
    {
        ApplicationDTO? applicationDto= await _applicationRepository.FindById(id);
        if (applicationDto == null)
        {
            return false;
        }
        if (!applicationDto.Daft)
        {
            return false;
        }
        try
        {
            await _applicationRepository.DeleteById(id);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        
    }

    public async Task<ApplicationDTO?> UpdateApplication(Guid id, ApplicationDTO new_applicationDto)
    {
        ApplicationDTO? applicationDto= await _applicationRepository.FindById(id);
        if (applicationDto == null)
        {
            return null;
        }

        if (!applicationDto.Daft)
        {
            return applicationDto;
        }
        
        if (ActivityMapper.GetActivityClassByString(new_applicationDto.Activity) is NullActivity)
        {
            new_applicationDto.Activity = null;
        }
        if (string.IsNullOrWhiteSpace(new_applicationDto.Name) && string.IsNullOrWhiteSpace(new_applicationDto.Description) && string.IsNullOrWhiteSpace(new_applicationDto.Plan) && new_applicationDto.Activity==null)
        {
            return applicationDto;
        }

        applicationDto.Name = new_applicationDto.Name;
        applicationDto.Description = new_applicationDto.Description;
        applicationDto.Plan = new_applicationDto.Plan;
        applicationDto.Activity = new_applicationDto.Activity;
        applicationDto.Daft = new_applicationDto.Daft;
        
        try
        {
            await _applicationRepository.Update(id,applicationDto);
            return await _applicationRepository.FindById(id);
        }
        catch (Exception)
        {
            return await _applicationRepository.FindById(id);
        }
    }

    public async Task<ApplicationDTO?> CreateApplication(ApplicationDTO applicationDto)
    {
        if ( applicationDto.UserId == null)
        {
            return null;
        }

        if (ActivityMapper.GetActivityClassByString(applicationDto.Activity) is NullActivity)
        {
            applicationDto.Activity = null;
        }
        
        if (string.IsNullOrWhiteSpace(applicationDto.Name) && string.IsNullOrWhiteSpace(applicationDto.Description) && string.IsNullOrWhiteSpace(applicationDto.Plan) && applicationDto.Activity == null)
        {
            return null;
        }
        var user = await _userRepository.FindById((Guid)applicationDto.UserId);
        if (user == null)
        {
            return null;
        }
        var draftpAplications = user.Applications.FirstOrDefault(p=>p.Daft);
        if (draftpAplications != null)
        {
            return null;
        }
        
        applicationDto.Daft = true;
        applicationDto.AddedDate = DateTime.Now;
        try
        {
            return await _userRepository.AddApplication((Guid)applicationDto.UserId,  applicationDto );
        }
        catch (Exception)
        {
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelDTO;

namespace Services;

public interface ICallForPaperService
{
    IList<ActivityDTO> GetAvailibleActivities();

    Task<ApplicationDTO?> GetApplicationByUserId(Guid id);

    Task<IList<ApplicationDTO>> GetApplicationSubmittedAfter(DateTime dateTime);

    Task<ApplicationDTO?> GetApplicationById(Guid id);
    
    Task<IList<ApplicationDTO>> GetApplicationunsubmittedOlder(DateTime dateTime);


    Task<Boolean> SendApplication(Guid id);

    Task<Boolean> DeleteApplication(Guid id);
    
    Task<ApplicationDTO?> UpdateApplication(Guid id,ApplicationDTO applicationDto); //если нельзя так поменять-ничего не делает

    Task<ApplicationDTO?> CreateApplication(ApplicationDTO applicationDto); //если нельзя так создать - вернет null
}
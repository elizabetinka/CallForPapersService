using CallForPapers.Infrastructure.Model.Activity;
using CallForPapers.Infrastructure.Repositories;
using CallForPapers.Services;
using CallForPapers.ServicesPresentationDto;
using Xunit;

namespace RepositoryTests;

public class OtherFunctionTest
{
    private ApplicationRepositoryForTest _applicationRepository;
    private ActivityRepository _activityRepository;
    private ICallForPaperService _callForPaperService;

    public OtherFunctionTest()
    {
        _applicationRepository = new ApplicationRepositoryForTest();
        _activityRepository = new ActivityRepository();
        _callForPaperService = new CallForPaperService(_applicationRepository,_activityRepository);
    }


    
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task UpdateApplicationSuccess(CreateApplicationRequestDto application)
    {
        CreateApplicationRequestDto applicationDto =
            new CreateApplicationRequestDto(Guid.NewGuid(), null, "name", "description", "plan");
        
        ApplicationResponseDto added = await _callForPaperService.CreateApplication(applicationDto);

        ApplicationResponseDto added2 = await _callForPaperService.UpdateApplication((Guid)added.Id, new UpdateApplicationRequestDto(application.Activity,application.Name,application.Description,application.Outline));

        Assert.NotNull(added2);
        Assert.Equal(added2.Activity,ActivityMapper.GetActivityClassByString(application.Activity)?.Activity);
        Assert.Equal(added2.Name,application.Name);
        Assert.Equal(added2.Description,application.Description);
        Assert.Equal(added2.Outline,application.Outline);
    }
    
    [Theory]
    [ClassData(typeof(BadApplications))]
    public async Task UpdateApplicationFailFewInfo(CreateApplicationRequestDto application)
    {
        CreateApplicationRequestDto applicationDto =
            new CreateApplicationRequestDto(Guid.NewGuid(), null, "name", "description", "plan");
        
        ApplicationResponseDto added = await _callForPaperService.CreateApplication(applicationDto);

        var exception = await Record.ExceptionAsync(() =>  _callForPaperService.UpdateApplication((Guid)added.Id, new UpdateApplicationRequestDto(application.Activity,application.Name,application.Description,application.Outline)));

        Assert.NotNull(exception);
    }
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task UpdateApplicationFailBecouseNotDraft(CreateApplicationRequestDto application)
    {
        CreateApplicationRequestDto applicationDto =
            new CreateApplicationRequestDto(Guid.NewGuid(), "Report", "name", "description", "plan");
        
        
        ApplicationResponseDto added = await _callForPaperService.CreateApplication(applicationDto);
        await _callForPaperService.SendApplication((Guid)added.Id);

        var exception = await Record.ExceptionAsync(() => _callForPaperService.UpdateApplication((Guid)added.Id, new UpdateApplicationRequestDto(application.Activity,application.Name,application.Description,application.Outline)));
        Assert.NotNull(exception);
       
    }
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task DeleteApplicationSuccess(CreateApplicationRequestDto application)
    {
        ApplicationResponseDto added = await _callForPaperService.CreateApplication(application);
        
        var exception = await Record.ExceptionAsync(() =>  _callForPaperService.DeleteApplication((Guid)added.Id));
        Assert.Null(exception);
        
        Assert.Null(await _callForPaperService.GetApplicationById((Guid)added.Id));
        Assert.Empty(_applicationRepository.Applications);
    }
    
    [Fact]
    public async Task DeleteApplicationFailBecuseDraft()
    {
        CreateApplicationRequestDto application = new CreateApplicationRequestDto(Guid.NewGuid(), "Report", "name", null, "plan");
      
        
        ApplicationResponseDto added = await _callForPaperService.CreateApplication(application);
        await _callForPaperService.SendApplication((Guid)added.Id);
        
        var exception = await Record.ExceptionAsync(() =>  _callForPaperService.DeleteApplication((Guid)added.Id));
        Assert.NotNull(exception);
        Assert.Single(_applicationRepository.Applications);
    }
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task DeleteApplicationFailBecuseDonHave(CreateApplicationRequestDto application)
    {
        var exception = await Record.ExceptionAsync(() =>_callForPaperService.DeleteApplication((Guid)application.Author)); // типа рандомные Id
        Assert.NotNull(exception);
    }
}
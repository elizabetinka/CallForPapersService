using CallForPapers.Infrastructure.Repositories;
using CallForPapers.Services;
using CallForPapers.ServicesPresentationDto;
using Xunit;

namespace RepositoryTests;

public class CreateTest
{
    private ApplicationRepositoryForTest _applicationRepository;
    private ActivityRepository _activityRepository;
    private ICallForPaperService _callForPaperService;

    public CreateTest()
    {
        _applicationRepository = new ApplicationRepositoryForTest();
        _activityRepository = new ActivityRepository();
        _callForPaperService = new CallForPaperService(_applicationRepository,_activityRepository);
    }
    
    
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task CreateApplicationTestSuccess(CreateApplicationRequestDto application)
    {
        ApplicationResponseDto added = await _callForPaperService.CreateApplication(application);

        Assert.NotNull(added);
        Assert.NotNull(added.Id);
        Assert.Single(_applicationRepository.Applications);
        Assert.Equal(_applicationRepository.FindById((Guid)added.Id).Result.UserId,application.Author);
    }
    
    
    [Theory]
    [ClassData(typeof(BadApplications))]
    public async Task CreateApplicationTestFailFewInfo(CreateApplicationRequestDto application)
    {
        var exception = await Record.ExceptionAsync(() => _callForPaperService.CreateApplication(application));

        Assert.NotNull(exception);
        Assert.Empty(_applicationRepository.Applications);
    }
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task CreateApplicationTestFailAddManyTimeDaft(CreateApplicationRequestDto application)
    {
        ApplicationResponseDto added = await _callForPaperService.CreateApplication(application);
        for (int i = 0; i < 10; ++i)
        {
            var exception = await Record.ExceptionAsync(() => _callForPaperService.CreateApplication(application));
            Assert.NotNull(exception);
        }
       
        Assert.Single(_applicationRepository.Applications);
    }
    
    [Fact]
    public async Task CreateApplicationTestSuccessAddManyTimeDaft()
    {
        CreateApplicationRequestDto application = new CreateApplicationRequestDto(Guid.NewGuid(), "Report", "name", null, "plan");
        
        ApplicationResponseDto added = await _callForPaperService.CreateApplication(application);
        _callForPaperService.SendApplication((Guid)added.Id);
        for (int i = 0; i < 10; ++i)
        {
            added = await _callForPaperService.CreateApplication(application);
            Assert.NotNull(added);
            _callForPaperService.SendApplication((Guid)added.Id);
        }
       
        Assert.Equal(_applicationRepository.Applications.Count,11);
    }
    
}
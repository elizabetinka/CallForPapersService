using InfrastructureOrm.Model;
using InfrastructureOrm.Model.Activity;
using InfrastructureOrm.Repositories;
using InfrastructureOrm.Repositories.interfaces;
using ModelDTO;
using Services;
using Xunit;

namespace RepositoryTests;

public class OtherFunctionTest
{
    private UserRepositoryForTest _userRepository;
    private AdminRepositoryForTest _adminRepository;
    private ApplicationRepositoryForTest _applicationRepository;
    private IPersonService _personService;
    private ICallForPaperService _callForPaperService;

    public OtherFunctionTest()
    {
        _adminRepository = new AdminRepositoryForTest();
        _applicationRepository = new ApplicationRepositoryForTest();
        _userRepository = new UserRepositoryForTest(_applicationRepository);
        _personService = new PersonService(_adminRepository, _userRepository);
        _callForPaperService = new CallForPaperService(_applicationRepository, _userRepository);
    }
    
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task UpdateApplicationSuccess(ApplicationDTO application)
    {
        ApplicationDTO applicationDto =
            new ApplicationDTO(null, null, "name", "description", "plan", DateTime.Now, Guid.NewGuid(), true);
        UserDTO? userDto = await _personService.RegistrUser("login", "password");
        applicationDto.UserId = userDto.Id;
        
        ApplicationDTO? added = await _callForPaperService.CreateApplication(applicationDto);

        ApplicationDTO? added2 = await _callForPaperService.UpdateApplication((Guid)added.Id, application);

        Assert.NotNull(added2);
        Assert.Equal(added2.Activity,ActivityMapper.GetActivityClassByString(application.Activity).activity);
        Assert.Equal(added2.Name,application.Name);
        Assert.Equal(added2.Description,application.Description);
        Assert.Equal(added2.Plan,application.Plan);
    }
    
    [Theory]
    [ClassData(typeof(BadApplications))]
    public async Task UpdateApplicationFailFewInfo(ApplicationDTO application)
    {
        ApplicationDTO applicationDto =
            new ApplicationDTO(null, null, "name", "description", "plan", DateTime.Now, Guid.NewGuid(), true);
        UserDTO? userDto = await _personService.RegistrUser("login", "password");
        applicationDto.UserId = userDto.Id;
        
        ApplicationDTO? added = await _callForPaperService.CreateApplication(applicationDto);

        ApplicationDTO? added2 = await _callForPaperService.UpdateApplication((Guid)added.Id, application);
        
        Assert.Equal(added2.Activity,added.Activity);
        Assert.Equal(added2.Name,added.Name);
        Assert.Equal(added2.Description,added.Description);
        Assert.Equal(added2.Plan,added.Plan);
    }
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task UpdateApplicationFailBecouseNotDraft(ApplicationDTO application)
    {
        ApplicationDTO applicationDto =
            new ApplicationDTO(null, "Report", "name", "description", "plan", DateTime.Now, Guid.NewGuid(), true);
        UserDTO? userDto = await _personService.RegistrUser("login", "password");
        applicationDto.UserId = userDto.Id;
        
        ApplicationDTO? added = await _callForPaperService.CreateApplication(applicationDto);
        await _callForPaperService.SendApplication((Guid)added.Id);

        ApplicationDTO? added2 = await _callForPaperService.UpdateApplication((Guid)added.Id, application);
        
        Assert.Equal(added2.Activity,added.Activity);
        Assert.Equal(added2.Name,added.Name);
        Assert.Equal(added2.Description,added.Description);
        Assert.Equal(added2.Plan,added.Plan);
    }
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task DeleteApplicationSuccess(ApplicationDTO application)
    {
        UserDTO? userDto = await _personService.RegistrUser("login", "password");
        application.UserId = userDto.Id;
        
        ApplicationDTO? added = await _callForPaperService.CreateApplication(application);

        Assert.True(await _callForPaperService.DeleteApplication((Guid)added.Id));
        Assert.Null(await _callForPaperService.GetApplicationById((Guid)added.Id));
        Assert.Empty(_applicationRepository.Applications);
    }
    
    [Fact]
    public async Task DeleteApplicationFailBecuseDraft()
    {
        ApplicationDTO application = new ApplicationDTO(null, "Report", "name", null, "plan", DateTime.Now,
            Guid.NewGuid(), true);
        UserDTO? userDto = await _personService.RegistrUser("login", "password");
        application.UserId = userDto.Id;
        
        ApplicationDTO? added = await _callForPaperService.CreateApplication(application);
        await _callForPaperService.SendApplication((Guid)added.Id);
        
        Assert.False(await _callForPaperService.DeleteApplication((Guid)added.Id));
        Assert.Single(_applicationRepository.Applications);
    }
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task DeleteApplicationFailBecuseDonHave(ApplicationDTO application)
    {
        Assert.False(await _callForPaperService.DeleteApplication((Guid)application.Id));
    }
}
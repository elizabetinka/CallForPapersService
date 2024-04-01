using InfrastructureOrm.Model;
using InfrastructureOrm.Repositories;
using InfrastructureOrm.Repositories.interfaces;
using ModelDTO;
using Services;
using Xunit;

namespace RepositoryTests;

public class CreateTest
{
    private UserRepositoryForTest _userRepository;
    private AdminRepositoryForTest _adminRepository;
    private ApplicationRepositoryForTest _applicationRepository;
    private IPersonService _personService;
    private ICallForPaperService _callForPaperService;

    public CreateTest()
    {
        _adminRepository = new AdminRepositoryForTest();
        _applicationRepository = new ApplicationRepositoryForTest();
        _userRepository = new UserRepositoryForTest(_applicationRepository);
        _personService = new PersonService(_adminRepository, _userRepository);
        _callForPaperService = new CallForPaperService(_applicationRepository, _userRepository);
    }
    
    
    [Theory]
    [ClassData(typeof(GoodUsers))]
    public async Task CreateUserTestSuccess(string login, string password)
    {
        UserDTO? userDto = await _personService.RegistrUser(login, password);

        Assert.NotNull(userDto);
        Assert.Single(_userRepository.Users);
        Assert.Equal(_userRepository.FindByLogin(login).Result.Id,userDto.Id);
    }
    
    
    [Theory]
    [ClassData(typeof(BadUsers))]
    public async Task CreateUserTestFail(string login, string password)
    {
        UserDTO? userDto = await _personService.RegistrUser(login, password);

        Assert.Null(userDto);
        Assert.Empty(_userRepository.Users);
    }
    
    
    
    [Theory]
    [ClassData(typeof(GoodUsers))]
    public async Task CreateAdminTestSuccess(string login, string password)
    {
        AdminDTO? userDto = await _personService.RegistrAdmin(login, password);

        Assert.NotNull(userDto);
        Assert.Single(_adminRepository.Admins);
        Assert.Equal(_adminRepository.FindByLogin(login).Result.Id,userDto.Id);
    }
    
    
    [Theory]
    [ClassData(typeof(BadUsers))]
    public async Task CreateAdminTestFail(string login, string password)
    {
        AdminDTO? userDto = await _personService.RegistrAdmin(login, password);

        Assert.Null(userDto);
        Assert.Empty(_adminRepository.Admins);
    }
    
    
    [Theory]
    [ClassData(typeof(GoodUsers))]
    public async Task CreatePersonFailLoginReapeted(string login, string password)
    {
        UserDTO? userDto = await _personService.RegistrUser(login, password);
        for (int i = 0; i < 10; ++i)
        {
            userDto = await _personService.RegistrUser(login, password);
            Assert.Null(userDto);
        }
        Assert.Single(_userRepository.Users);
    }
    
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task CreateApplicationTestSuccess(ApplicationDTO application)
    {
        UserDTO? userDto = await _personService.RegistrUser("login", "password");
        application.UserId = userDto.Id;
        
        ApplicationDTO? added = await _callForPaperService.CreateApplication(application);

        Assert.NotNull(added);
        Assert.NotNull(added.Id);
        Assert.True(added.Daft);
        Assert.Single(_applicationRepository.Applications);
        Assert.Equal(_applicationRepository.FindById((Guid)added.Id).Result.UserId,application.UserId);
        Assert.Equal(_userRepository.FindById(userDto.Id).Result.Applications[0].Id,added.Id);
    }
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task CreateApplicationTestFailDontHaveSuchUser(ApplicationDTO application)
    {
        ApplicationDTO? added = await _callForPaperService.CreateApplication(application);

        Assert.Null(added);
        Assert.Empty(_applicationRepository.Applications);
    }
    
    [Theory]
    [ClassData(typeof(BadApplications))]
    public async Task CreateApplicationTestFailFewInfo(ApplicationDTO application)
    {
        UserDTO? userDto = await _personService.RegistrUser("login", "password");
        application.UserId = userDto.Id;
        
        ApplicationDTO? added = await _callForPaperService.CreateApplication(application);

        Assert.Null(added);
        Assert.Empty(_applicationRepository.Applications);
    }
    
    [Theory]
    [ClassData(typeof(GoodApplications))]
    public async Task CreateApplicationTestFailAddManyTimeDaft(ApplicationDTO application)
    {
        UserDTO? userDto = await _personService.RegistrUser("login", "password");
        application.UserId = userDto.Id;
        
        ApplicationDTO? added = await _callForPaperService.CreateApplication(application);
        for (int i = 0; i < 10; ++i)
        {
            added = await _callForPaperService.CreateApplication(application);
            Assert.Null(added);
        }
       
        Assert.Single(_applicationRepository.Applications);
    }
    
    [Fact]
    public async Task CreateApplicationTestSuccessAddManyTimeDaft()
    {
        ApplicationDTO application = new ApplicationDTO(null, "Report", "name", null, "plan", DateTime.Now,
            Guid.NewGuid(), true);
        
        UserDTO? userDto = await _personService.RegistrUser("login", "password");
        application.UserId = userDto.Id;
        
        ApplicationDTO? added = await _callForPaperService.CreateApplication(application);
        _callForPaperService.SendApplication((Guid)added.Id);
        for (int i = 0; i < 10; ++i)
        {
            added = await _callForPaperService.CreateApplication(application);
            Assert.NotNull(added);
            _callForPaperService.SendApplication((Guid)added.Id);
        }
       
        Assert.Equal(_applicationRepository.Applications.Count,11);
        Assert.Equal(_userRepository.FindById(userDto.Id).Result.Applications.Count(),11);
    }
    
}
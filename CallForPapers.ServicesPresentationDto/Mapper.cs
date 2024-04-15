using CallForPapers.InfrastructureServicesDto;
namespace CallForPapers.ServicesPresentationDto;

public static class Mapper
{
    public static ApplicationResponseDto? ToApplicationResponse (ApplicationDto? application)
    {
        if (application == null)
        {
            return null;
        }

        return new ApplicationResponseDto {
            Id = application.Id,
            Author = application.UserId, 
            Activity = application.Activity,
            Name = application.Name, 
            Description = application.Description, 
            Outline = application.Plan
    };
        
        
}

    public static IList<ApplicationResponseDto> ToApplicationResponses(IList<ApplicationDto> application)
    {
        return application.Select(apl => ToApplicationResponse(apl)).ToList();
    }
}
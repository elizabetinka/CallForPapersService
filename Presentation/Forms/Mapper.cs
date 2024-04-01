using ModelDTO;

namespace WebApplication1.Forms;

static class Mapper
{
    public static ApplicationSeniorForm ToApplicationSeniorForm (ApplicationDTO? applicationDto)
    {
        if (applicationDto == null)
        {
            return new ApplicationSeniorForm {
                Id = null,
                Author = null, 
                Activity = null,
                Name = null, 
                Description = null, 
                Outline = null
            };
        }

        return new ApplicationSeniorForm {
            Id = applicationDto.Id,
            Author = applicationDto.UserId, 
            Activity = applicationDto.Activity,
            Name = applicationDto.Name, 
            Description = applicationDto.Description, 
            Outline = applicationDto.Plan
    };
}
    public static IList<ApplicationSeniorForm> ToApplicationSeniorForm (IList<ApplicationDTO> applicationDto)
    {
        IList<ApplicationSeniorForm> result = new List<ApplicationSeniorForm>();
        foreach (var app in applicationDto)
        {
            result.Add( new ApplicationSeniorForm {
                Id = app.Id,
                Author = app.UserId, 
                Activity = app.Activity,
                Name = app.Name, 
                Description = app.Description, 
                Outline = app.Plan
            });
        }

        return result;
    }
    
}
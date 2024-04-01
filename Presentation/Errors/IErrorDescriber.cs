using Microsoft.AspNetCore.Http;

namespace WebApplication1.Errors;

public interface IErrorDescriber
{
    public void HaventBeenWrittenData(HttpContext context);

    public void HaventBeenAuthorizated(HttpContext context);

    public void PersonNotFounded(HttpContext context);

    public void WrongPassword(HttpContext context);

    public void NoAccessToData(HttpContext context);
}
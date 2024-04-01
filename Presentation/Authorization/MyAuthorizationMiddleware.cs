using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using ModelDTO;
using Services;
using WebApplication1.Errors;

namespace WebApplication1.Authorization;

public class MyAuthorizationMiddleware : IAuthorizationMiddlewareResultHandler
{
    private IPersonService _service;
    private IErrorDescriber _errorDescriber;

    public MyAuthorizationMiddleware(IPersonService service, IErrorDescriber errorDescriber)
    {
        _service = service;
        _errorDescriber = errorDescriber;
    }
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        next = next ?? throw new ArgumentNullException(nameof(next));
        context = context ?? throw new ArgumentNullException(nameof(context));
        policy = policy ?? throw new ArgumentNullException(nameof(policy));
        
        KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> authHeader = context.Request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization", StringComparison.Ordinal));

        if (authHeader.Key is null)
        {
            _errorDescriber.HaventBeenAuthorizated(context);
            return;
        }

        var value = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
        string? login = string.Empty;
        string? password = string.Empty;
        if (value.Parameter != null)
        {
            string[] strs = Encoding.UTF8.GetString(Convert.FromBase64String(value.Parameter)).Split(':');
            login = strs.FirstOrDefault();
            password = strs.LastOrDefault();
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                _errorDescriber.HaventBeenWrittenData(context);
                IHttpResponseFeature? feature = context.Features.Get<IHttpResponseFeature>();
                if (feature is not null)
                {
                    feature.ReasonPhrase = "HaventBeenWrittenData";
                }

                AuthorizationFailure.Failed(policy.Requirements);
                return;
            }

            IPerson? person = await _service.GetByLogin(login);
            if (person is null)
            {
                _errorDescriber.PersonNotFounded(context);
                return;
            }
            if (!person.Password.Equals(password, StringComparison.Ordinal))
            {
                _errorDescriber.WrongPassword(context);
                return;
            }

            string role = "user";
            if (person is AdminDTO)
            {
                role = "admin";
            }
            foreach (IAuthorizationRequirement requirement in policy.Requirements)
            {
                if (requirement is not RolesAuthorizationRequirement rolesAuthorizationRequirement)
                {
                    continue;
                }

                if (rolesAuthorizationRequirement.AllowedRoles.Contains(role))
                {
                    continue;
                }

                _errorDescriber.NoAccessToData(context);
                return;
            }

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, login),
                new Claim(ClaimTypes.Role, role),
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            context.User = principal;
            await next(context);
        }
    }
}
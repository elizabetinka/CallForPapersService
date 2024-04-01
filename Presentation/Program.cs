using System;
using InfrastructureOrm.Extention;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Services;
using WebApplication1.Authorization;
using WebApplication1.Errors;


var builder = WebApplication.CreateBuilder(args);

// добавление сервисов
builder.Services.AddControllers();

builder.Services.AddSingleton<IErrorDescriber, ErrorDescriber>(); // обработчики ошибок
builder.Services.AddSingleton<IPersonService, PersonService>(); 
builder.Services.ExtentionRepository(); // репозитории
builder.Services.AddSingleton<ICallForPaperService, CallForPaperService>(); 

builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, MyAuthorizationMiddleware>();



builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Set Title and version
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CallForPapersService", Version = "v1", Description = "My c# Test task" });

    options.EnableAnnotations();
    options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Write your data.",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic",
                },
            },
            Array.Empty<string>()
        },
    });
});

builder.Services.AddAuthentication("BasicAuthentication").Services.AddSingleton<
    IAuthorizationMiddlewareResultHandler, MyAuthorizationMiddleware>();
builder.Services.AddAuthorization();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
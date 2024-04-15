using CallForPapers.Infrastructure.Extention;
using Microsoft.OpenApi.Models;
using CallForPapers.Services;
using WebApplication1.Middleware;

var builder = WebApplication.CreateBuilder(args);

// добавление сервисов
builder.Services.AddControllers();

builder.Services.ExtentionRepository(); // репозитории
builder.Services.AddScoped<ICallForPaperService, CallForPaperService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Set Title and version
    options.SwaggerDoc("v1",
        new OpenApiInfo { Title = "CallForPapersService", Version = "v1", Description = "My c# Test task" });

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


WebApplication app = builder.Build();

await app.MigrateDatabase();
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlerMiddleware>(); // обработка ошибок
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
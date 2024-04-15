FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CallForPapers.Presentation/CallForPapers.Presentation.csproj", "CallForPapers.Presentation/"]
COPY ["CallForPapers.InfrastructureServicesDto/CallForPapers.InfrastructureServicesDto.csproj", "CallForPapers.InfrastructureServicesDto/"]
COPY ["CallForPapers.Services/CallForPapers.Services.csproj", "CallForPapers.Services/"]
COPY ["CallForPapers.ServicesPresentationDto/CallForPapers.ServicesPresentationDto.csproj", "CallForPapers.ServicesPresentationDto/"]
COPY ["CallForPapers.Infrastructure/CallForPapers.Infrastructure.csproj", "CallForPapers.Infrastructure/"]
RUN dotnet restore "CallForPapers.Presentation/CallForPapers.Presentation.csproj"
COPY . .
WORKDIR "/src/CallForPapers.Presentation"
RUN dotnet build "CallForPapers.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CallForPapers.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CallForPapers.Presentation.dll"]

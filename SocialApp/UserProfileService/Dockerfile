FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5002

ENV ASPNETCORE_URLS http://*:5002

# set this for swagger
#ENV ASPNETCORE_ENVIRONMENT Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserProfileService/UserProfileService.csproj", "UserProfileService/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "UserProfileService/UserProfileService.csproj"
COPY . .
WORKDIR "/src/UserProfileService"
RUN dotnet build "UserProfileService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserProfileService.csproj" -c Release -o /app/publish -r linux-x64 --self-contained

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./UserProfileService"]

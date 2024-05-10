FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS http://*:5001

# set this for swagger
#ENV ASPNETCORE_ENVIRONMENT Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserTimelineService/UserTimelineService.csproj", "UserTimelineService/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "UserTimelineService/UserTimelineService.csproj"
COPY . .
WORKDIR "/src/UserTimelineService"
RUN dotnet build "UserTimelineService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserTimelineService.csproj" -c Release -o /app/publish -r linux-x64 --self-contained

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./UserTimelineService"]

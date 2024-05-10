FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5003

ENV ASPNETCORE_URLS http://*:5003

# set this for swagger
#ENV ASPNETCORE_ENVIRONMENT Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FollowService/FollowService.csproj", "FollowService/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "FollowService/FollowService.csproj"
COPY . .
WORKDIR "/src/FollowService"
RUN dotnet build "FollowService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FollowService.csproj" -c Release -o /app/publish -r linux-x64 --self-contained

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./FollowService"]

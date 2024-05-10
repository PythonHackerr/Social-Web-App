FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Kakaw/Kakaw.csproj", "Kakaw/"]
RUN dotnet restore "Kakaw/Kakaw.csproj"
COPY . .
WORKDIR "/src/Kakaw"
RUN dotnet build "Kakaw.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kakaw.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kakaw.dll"]

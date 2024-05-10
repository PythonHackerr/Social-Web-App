FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY ./BlazorClient/ ./BlazorClient/
COPY ./Shared/ ./Shared/
WORKDIR "/app/BlazorClient"
RUN dotnet publish -c Release -o output -r linux-x64 --self-contained

FROM nginx:alpine
WORKDIR /var/www/web
COPY --from=build /app/BlazorClient/output/wwwroot .
COPY ./BlazorClient/nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
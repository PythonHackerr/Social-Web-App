using MySqlConnector;
using UserTimelineService.Config;
using UserTimelineService.Repository;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("Default") ?? string.Empty;

Console.WriteLine($"Is development? {builder.Environment.IsDevelopment()}");

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddOptions();
services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));

services.AddTransient<MySqlConnection>(_ => new MySqlConnection(connectionString));
services.AddScoped<IPostRepository, MySqlPostRepository>();
services.AddScoped<ICommentRepository, MySqlCommentRepository>();
services.AddScoped<IUCLRepository, MySqlUCLRepository>();
services.AddScoped<IUPLRepository, MySqlUPLRepository>();

services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
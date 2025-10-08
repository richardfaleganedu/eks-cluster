var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var mission = Environment.GetEnvironmentVariable("APP_MISSION")
        ?? "CentralReach: commits to empowering people with autism and related intellectual and developmental disabilities (IDDs) and supporting those who serve them";

    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CentralReach Demo API",
        Version = "v1",
        Description = $"Minimal API demo running on EKS\n\nMission: {mission}"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DocumentTitle = "CentralReach Demo – Swagger";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CentralReach Demo API v1");
});

app.MapGet("/", () => Results.Redirect("/swagger"));

var appMessage = Environment.GetEnvironmentVariable("APP_MESSAGE") ?? "Working as Designed";
var appVersion = Environment.GetEnvironmentVariable("APP_VERSION") ?? "1.0.0";
var appMission = Environment.GetEnvironmentVariable("APP_MISSION")
    ?? "CentralReach: commits to empowering people with autism and related intellectual and developmental disabilities (IDDs) and supporting those who serve them";

app.MapGet("/health", () => new
{
    status = appMessage,
    mission = appMission,
    timestamp = DateTime.UtcNow,
    hostname = Environment.MachineName,
    version = appVersion
});

app.MapGet("/api/info", () => new
{
    application = $"CentralReach Demo – {appMessage}",
    mission = appMission,
    version = appVersion,
    timestamp = DateTime.UtcNow
});

app.Run();

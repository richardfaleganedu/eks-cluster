// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// app.UseSwagger();
// app.UseSwaggerUI();

// app.MapGet("/", () => Results.Redirect("/swagger"));

// app.MapGet("/health", () => new
// {
//     status = "healthy",
//     timestamp = DateTime.UtcNow,
//     hostname = Environment.MachineName,
//     version = "1.0.0"
// });

// app.MapGet("/api/info", () => new
// {
//     application = "CentralReach Demo",
//     version = "1.0.0",
//     timestamp = DateTime.UtcNow
// });

// app.Run();


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CentralReach Demo API",                 // shows in Swagger header
        Version = "v1",
        Description = "Minimal API demo running on EKS"  // shows under title
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

var appMessage = Environment.GetEnvironmentVariable("APP_MESSAGE") 
                 ?? "Working as Designed";

var appVersion = Environment.GetEnvironmentVariable("APP_VERSION") 
                 ?? "1.0.0";

app.MapGet("/health", () => new
{
    status = appMessage,
    timestamp = DateTime.UtcNow,
    hostname = Environment.MachineName,
    version = appVersion
});

app.MapGet("/api/info", () => new
{
    application = $"CentralReach Demo – {appMessage}",
    version = appVersion,
    timestamp = DateTime.UtcNow
});

app.Run();

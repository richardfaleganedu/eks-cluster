var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapGet("/health", () => new
{
    status = "healthy",
    timestamp = DateTime.UtcNow,
    hostname = Environment.MachineName,
    version = "1.0.0"
});

app.MapGet("/api/info", () => new
{
    application = "CentralReach Demo",
    version = "1.0.0",
    timestamp = DateTime.UtcNow
});

app.Run();
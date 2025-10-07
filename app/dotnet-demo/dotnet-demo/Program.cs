var builder = WebApplication.CreateBuilder(args);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI();

// Root - redirect to Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// Health endpoint
app.MapGet("/health", () => new
{
    status = "healthy",
    timestamp = DateTime.UtcNow,
    hostname = Environment.MachineName,
    version = "1.0.0",
    message = "CentralReach: empowering people with autism and related intellectual and developmental disabilities and supporting those who serve them"
});

// Info endpoint
app.MapGet("/api/info", () => new
{
    application = "CentralReach Demo API",
    version = "1.0.0",
    dotnetVersion = Environment.Version.ToString(),
    timestamp = DateTime.UtcNow
});

app.Run();
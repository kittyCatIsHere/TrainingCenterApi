
var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();

// Build application
var app = builder.Build();

// Map controller endpoints
app.MapControllers();

// Start application
app.Run();

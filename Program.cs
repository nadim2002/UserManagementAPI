using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ ORDER IS VERY IMPORTANT

// 1. Error handling FIRST
app.UseMiddleware<ErrorHandlingMiddleware>();

// 2. Authentication SECOND
app.UseMiddleware<AuthMiddleware>();

// 3. Logging LAST
app.UseMiddleware<LoggingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
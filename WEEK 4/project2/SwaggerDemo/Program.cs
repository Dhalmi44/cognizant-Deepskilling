using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register required services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee API",
        Version = "v1",
        Description = "A simple example ASP.NET Core Web API"
    });
});

var app = builder.Build();

// Enable middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

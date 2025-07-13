using HHH_EmployeeManagement.API.Data;
using HHH_EmployeeManagement.API.Services;
using Microsoft.EntityFrameworkCore;
using HHH_EmployeeManagement.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("EmployeeDb"));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

SeedData(app);
app.UseCors("AllowAngularClient");



// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
app.Run();

void SeedData(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Employees.Any())
    {
        context.Employees.AddRange(
            new Employee { FullName = "Alice", Email = "alice@example.com", Department = "HR", Salary = 5000 },
            new Employee { FullName = "Bob", Email = "bob@example.com", Department = "IT", Salary = 8000 }
        );
        context.SaveChanges();
    }
}



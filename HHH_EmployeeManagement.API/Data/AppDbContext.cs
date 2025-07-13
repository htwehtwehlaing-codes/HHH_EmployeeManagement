using Microsoft.EntityFrameworkCore;
using HHH_EmployeeManagement.API.Models;

namespace HHH_EmployeeManagement.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } // Add more DbSets for other models
    }
}

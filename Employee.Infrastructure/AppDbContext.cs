using Employee.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        
        public DbSet<EmployeeInformation> Employees { get; set; }
    }
}

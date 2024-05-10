using Api.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

        }
    }
}

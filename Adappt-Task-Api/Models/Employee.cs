using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
}

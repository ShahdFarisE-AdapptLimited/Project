using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO
{
    public class EmploeeDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        public int EmployeeCode { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}

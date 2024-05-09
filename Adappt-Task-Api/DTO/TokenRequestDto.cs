using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WebApplication1.DTO
{
    public class TokenRequestDto
    {
        [EmailAddress]
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
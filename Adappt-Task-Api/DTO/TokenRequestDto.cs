using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Api.DTO
{
    public class TokenRequestDto
    {
        [EmailAddress]
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
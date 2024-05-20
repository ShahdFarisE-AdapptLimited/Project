using Api.DTO;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Api.Models;


namespace Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        public async Task<string> RegisterAsync(EmployeeDto model)
        {
            var userExist = DoesUserExist(model.EmailAddress);

            if (userExist)
                return null!;

            var employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                //EmployeeCode = model.EmployeeCode,
                EmailAddress = model.EmailAddress
            };

            // hashing password 
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            employee.PasswordHash = hashedPassword;

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();


            var tokenRequestDto = new TokenRequestDto { Email = model.EmailAddress, Password = model.Password };
            var token = GenerateJwtToken(tokenRequestDto);
            return token;
        }


        public async Task<string> GetTokenAsync(TokenRequestDto model)
        {
            var user = await _context.Employees.FirstOrDefaultAsync(x => x.EmailAddress == model.Email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return null!;
            }

            var token = GenerateJwtToken(model);
            return token;
        }

        private string GenerateJwtToken(TokenRequestDto user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credientials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.Email, user.Email)
            };

            var expires = DateTime.Now.AddMinutes(60);
           
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: expires,
                signingCredentials: credientials );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool DoesUserExist(string Email)
        {
            return _context.Employees.Any(x => x.EmailAddress == Email);
        }
    }
}

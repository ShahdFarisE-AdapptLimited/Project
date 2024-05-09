using WebApplication1.DTO;


namespace WebApplication1.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(EmploeeDto model);
        Task<string> GetTokenAsync(TokenRequestDto model);
        //Task<string> AddRoleAsync(AddRoleModel model);
    }
}

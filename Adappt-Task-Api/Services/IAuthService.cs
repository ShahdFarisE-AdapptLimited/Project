using Api.DTO;


namespace Api.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(EmploeeDto model);
        Task<string> GetTokenAsync(TokenRequestDto model);
        //Task<string> AddRoleAsync(AddRoleModel model);
    }
}

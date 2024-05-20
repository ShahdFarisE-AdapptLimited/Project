using Api.DTO;


namespace Api.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(EmployeeDto model);
        Task<string> GetTokenAsync(TokenRequestDto model);
        //Task<string> AddRoleAsync(AddRoleModel model);
    }
}

using Client.Models;

namespace Client.Services
{
    public interface IEmployeeService
    {
        Task<bool> DeleteEmployeeAsync(int id);
        Task<List<Employee>?> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<bool> InsertEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employee);
    }
}
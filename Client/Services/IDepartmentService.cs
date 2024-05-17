using Client.Models;

namespace Client.Services
{
    public interface IDepartmentService
    {
        Task<bool> DeleteDeparmentAsync(int Id);
        Task<List<Department>?> GetAllDepartmetsAsync();
        Task<Department?> GetDepartmetByIdAsync(int id);
        Task<bool> InsertDepartmentAsync(Department Dpartment);
        Task<bool> UpdateDepartmentAsync(Department Department);

        Task<bool> AddEmployee(int DepartmentId, Employee employee);
    }
}

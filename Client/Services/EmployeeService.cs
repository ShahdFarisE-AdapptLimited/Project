using Client.Models;
using System.Net.Http.Json;

namespace Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Employee>?> GetAllEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("employees/Get");
            response.EnsureSuccessStatusCode(); // Check for successful response
            return await response.Content.ReadFromJsonAsync<List<Employee>>();
        }


        //Add  Employees
        public async Task<bool> InsertEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync("employees", employee);
            response.EnsureSuccessStatusCode(); // Check for successful response
            return response.IsSuccessStatusCode; // Return true on successful creation
        }

        //Get Employee By Id  
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"employees/GetById/{id}");
            response.EnsureSuccessStatusCode(); // Check for successful response
            return await response.Content.ReadFromJsonAsync<Employee>();
        }


        //Update Employee
        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"employees/Update/{employee.EmployeeId}", employee);
            response.EnsureSuccessStatusCode(); // Check for successful response
            return response.IsSuccessStatusCode; // Return true on successful update
        }


        //Delete Employee
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"employees/Delete/{id}");
            response.EnsureSuccessStatusCode(); // Check for successful response
            return response.IsSuccessStatusCode; // Return true on successful deletion
        }
    }
}

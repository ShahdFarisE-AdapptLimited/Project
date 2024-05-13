using Client.Models;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Client.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Department>?> GetAllDepartmetsAsync()
        {
            var response = await _httpClient.GetAsync("Departments/GetAll");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Department>>();
        }    

        public async Task<Department?> GetDepartmetByIdAsync(int id)
        {
             var reponse = await _httpClient.GetAsync($"Departments/GetById/{id}");
             reponse.EnsureSuccessStatusCode();
            return await reponse.Content.ReadFromJsonAsync<Department>();
        }

        public async Task<bool> InsertDepartmentAsync(Department Department)
        {
            var response = await _httpClient.PostAsJsonAsync("Departments/create", Department);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDepartmentAsync(Department Department)
        {
            var response = await _httpClient.PutAsJsonAsync($"Departments/Update/{Department.DepartmentId}", Department);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDeparmentAsync(int DepartmentId)
        {
            var response = await _httpClient.DeleteAsync($"Departments/Delete/{DepartmentId}");
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
    }
}

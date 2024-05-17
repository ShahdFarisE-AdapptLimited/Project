using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.DTO;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmploeeDto Dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var employee = new Employee()
            {
                FirstName = Dto.FirstName,
                LastName = Dto.LastName,
                Designation = Dto.Designation,
                EmailAddress = Dto.EmailAddress
            };

            
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Dto.Password);
            employee.PasswordHash = hashedPassword;

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == Id);
            return Ok(employee);
        }

        [HttpPut("Update/{EmployeeId}")]
        public async Task<IActionResult> Update(int EmployeeId, EmploeeDto NewEmploee)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == EmployeeId);

            if (employee is null)
            {
                return NotFound();
            }

            employee.FirstName = NewEmploee.FirstName;
            employee.LastName = NewEmploee.LastName;
            employee.EmailAddress = NewEmploee.EmailAddress;
            employee.Designation = NewEmploee.Designation;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(NewEmploee.Password);
            employee.PasswordHash = hashedPassword;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("Delete/{EmployeeId}")]
        public async Task<IActionResult> DeleteAsync(int EmployeeId)
        {

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == EmployeeId);

            if (employee is null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
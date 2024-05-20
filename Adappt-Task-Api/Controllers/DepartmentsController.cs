using Api.DTO;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetDepartmemnts()
        {
            var departments = await _context.Departments
                                       // .Include(d => d.Employees)
                                        .ToListAsync();
            return Ok(departments);
        }

        [HttpGet("GetById/{DepartmentId}")]
        public async Task<IActionResult> GetDepartmentById(int DepartmentId)
        {
            var department = await _context.Departments
               // .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.DepartmentId == DepartmentId);

            return Ok(department);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(DepartmentDto DepartmentDto)
        {
            var depart = new Department()
            {
                DepartmentName = DepartmentDto.DepartmentName
            };

            await _context.Departments.AddAsync(depart);
            await _context.SaveChangesAsync();

            return Ok(depart);
        }

        [HttpPut("AddEmployee/{DepartmentId}")]
        public async Task<IActionResult> AddEmployee(int DepartmentId, Employee employee)
        {
            var depart = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == DepartmentId);

            if (depart is null)
            {
                return NotFound();
            }


            employee?.Departments?.Add(depart);
            _context.Employees.Update(employee!);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("AddEmployees/{DepartmentId}")]
        public async Task<IActionResult> AddEmployees(int DepartmentId, List<EmployeeWithID> employees)
        {
            var depart = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == DepartmentId);
            if (depart is null)
            {
                return NotFound();
            }

            
            foreach (var employee in employees)
            {
                var emp = await _context.Employees.Include(e => e.Departments).FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
              
                if (emp?.Departments is null)
                {
                    emp.Departments = new List<Department>();
                }
                emp.Departments.Add(depart);
                _context.Employees.Update(emp);

               // depart.Employees.Add(employee);
               // _context.Update(depart);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("RemoveEmployee/{DepartmentId}")]
        public async Task<IActionResult> RemoveEmployee(int DepartmentId, EmployeeWithID employee)
        {
            var depart = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == DepartmentId);

            if (depart is null)
            {
                return NotFound();
            }
            var emp = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            emp?.Departments?.Remove(depart);
            _context.Employees.Update(emp);

           // depart.Employees.Remove(employee);
            _context.Update(depart);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("RemoveEmployees/{DepartmentId}")]
        public async Task<IActionResult> RemoveEmployees(int departmentId, List<EmployeeWithID> employees)
        {
            // Find the department by ID
            var department = await _context.Departments
              .Include(d => d.Employees) // Eager loading for employees
              .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);

            if (department == null)
            {
                return NotFound("Department not found");
            }

            // Get a list of employee IDs to remove
            var employeeIdsToRemove = employees.Select(e => e.EmployeeId).ToList();

            // Filter employees in the department based on IDs to remove
            var employeesToRemove = department.Employees.Where(e => employeeIdsToRemove.Contains(e.EmployeeId)).ToList();

            foreach (var emp in employeesToRemove)
            {
                emp.Departments?.Remove(department);
                department.Employees?.Remove(emp);

                _context.Employees.Update(emp);
            }


            _context.Departments.Update(department);
            // Update the department in the database
            await _context.SaveChangesAsync();
            return Ok(department);
        }
       

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int DepartmentId, Department NewDepartment)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == DepartmentId);
            if (department == null)
            {
                return NotFound();
            }
            department.DepartmentName = NewDepartment.DepartmentName;
            _context.Departments.Update(NewDepartment);
            await _context.SaveChangesAsync();
            return Ok(NewDepartment);
        }

        [HttpDelete("Delete/{DeparmentId}")]
        public async Task<IActionResult> Delete(int DeparmentId)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == DeparmentId);
            if (department is null)
            {
                return NotFound();
            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return Ok(department);
        }
    }
}


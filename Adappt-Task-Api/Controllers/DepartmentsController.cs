﻿using Api.DTO;
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
            var departments = await _context.Departments.ToListAsync();
            return Ok(departments);
        }

        [HttpGet("GetById/{DepartmentId}")]
        public async Task<IActionResult> GetDepartmentById(int DepartmentId)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == DepartmentId);
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

        //ToDo:

        //[HttpPut("AddEmployee/{DepartmentId}")]
        //public async Task<IActionResult> AddEmployee(int DepartmentId, Employee employee)
        //{
        //    var depart =await _context.Departments.FirstOrDefaultAsync(d =>d.DepartmentId == DepartmentId);
            
        //    if(depart is null)
        //    {
        //        return NotFound();
        //    }
          
        //    if (employee.DepartmentId != 0)
        //    {
        //        var oldDepartment = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == employee.DepartmentId);
        //        oldDepartment?.Employees.Remove(employee);
        //        await _context.SaveChangesAsync();
        //    }

        //    employee.DepartmentId = DepartmentId;
        //    _context.Update(employee);

        //    depart.Employees.Add(employee);
        //    _context.Update(depart);
        //    await _context.SaveChangesAsync();
            
        //    return Ok(depart);
        //}

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int DepartmentId,Department NewDepartment)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == DepartmentId); 
            if (department == null)
            {
                return NotFound();
            }
            department.DepartmentName = NewDepartment.DepartmentName;
            department.Employees = NewDepartment.Employees;

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
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return Ok(department);
        }
    }
}

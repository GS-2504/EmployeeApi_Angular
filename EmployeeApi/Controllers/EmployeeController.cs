using AutoMapper;
using EmployeeApi.Data;
using EmployeeApi.Models;
using EmployeeApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public EmployeeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var employeeDataFromDb = from employee in _context.Employees
                             join department in _context.Departments
                             on employee.EmployeeId equals department.DepartmentId
                             select new 
                             {
                              EmployeeId= employee.EmployeeId,
                              EmployeeName = employee.EmployeeName,
                              Address = employee.EmployeeAddress,
                              DesignationId = employee.DesignationId,
                              DepartmentsName = department.DepartmentName
                             };
                     return Ok(employeeDataFromDb);
        }
        [HttpPost]
        public IActionResult AddEmployee(employeeDto employeeDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            Employee employee = new Employee()
            {
                EmployeeId = employeeDto.EmployeeId,
                EmployeeName = employeeDto.EmployeeName,
                EmployeeAddress = employeeDto.EmployeeAddress,
                DesignationId = employeeDto.DesignationId,
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
           
            
            List<EmployeeDepartment> departmentEmployees = new List<EmployeeDepartment>();
            foreach (var empDep in employeeDto.DepartmentsIds)
            {
                EmployeeDepartment EmployeeDepartment = new EmployeeDepartment()
                {
                    EmployeeId = employee.EmployeeId,
                    DepartmentId = empDep
                };
                departmentEmployees.Add(EmployeeDepartment);

            }
            _context.EmployeeDepartments.AddRange(departmentEmployees);
            _context.SaveChanges();
            return Ok();
        }
    }
}

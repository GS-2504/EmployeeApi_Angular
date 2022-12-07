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

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}")]
        public IActionResult GetEmployee(int id)
        {
            var employeeFromDb = _context.Employees.Find(id);
            if (employeeFromDb == null) return NotFound();
            var departmentFromDb = _context.EmployeeDepartments.
                Where(emp => emp.EmployeeId == id).Select(dep => dep.DepartmentId);
            List<string> departmentsNameList = new List<string>();
            var departments = _context.Departments.ToList();
            foreach (var depId in departmentFromDb)
            {
               var departmentName=departments.Where(dep => dep.DepartmentId == depId).
                    Select(dep => dep.DepartmentName).FirstOrDefault();
                departmentsNameList.Add(departmentName);
            }
            EmployeeDto employeeDto = new EmployeeDto()
            {
                EmployeeId = employeeFromDb.EmployeeId,
                EmployeeName = employeeFromDb.EmployeeName,
                DesignationId= employeeFromDb.DesignationId,
                EmployeeAddress = employeeFromDb.EmployeeAddress,
                DepartmentsNames = departmentsNameList
            };
            return Ok(employeeDto);
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employeeDataFromDb = from employee in _context.Employees
                                     join employeeDepartment in _context.EmployeeDepartments
                                     on employee.EmployeeId equals employeeDepartment.EmployeeId
                                     select new 
                                     {
                                         EmployeeId = employee.EmployeeId,
                                         EmployeeName = employee.EmployeeName,
                                         EmployeeAddress = employee.EmployeeAddress,
                                         DesignationId = employee.DesignationId,
                                         Departments =     employeeDepartment.Department
                                     };
            return Ok(employeeDataFromDb);
        }
        [HttpPost]
        public IActionResult AddEmployee(EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid && employeeDto==null) return BadRequest();
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
         [HttpPut]
         public IActionResult UpdateEmployee(EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            Employee employee = new Employee()
            {
                EmployeeId = employeeDto.EmployeeId,
                EmployeeName = employeeDto.EmployeeName,
                EmployeeAddress = employeeDto.EmployeeAddress,
                DesignationId = employeeDto.DesignationId
            };
              _context.Employees.Update(employee);
                 _context.SaveChanges();
              var employeeExistingDepartments = _context.EmployeeDepartments.Where(dep => dep.EmployeeId == employee.EmployeeId).ToList();
                  _context.RemoveRange(employeeExistingDepartments);
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

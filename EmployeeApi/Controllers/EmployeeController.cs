using AutoMapper;
using EmployeeApi.Data;
using EmployeeApi.Models;
using EmployeeApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
            var employeeFromDb = _context.Employees.Include(deg=>deg.Designation).FirstOrDefault(empId=>empId.EmployeeId==id);
            if (employeeFromDb == null) return NotFound();
            var departmentFromDb = _context.EmployeeDepartments.
                Where(emp => emp.EmployeeId == id).Select(dep => dep.Department);
             List<Department> departmentsNames = new List<Department>();
            foreach (var dep in departmentFromDb)
            {
                departmentsNames.Add(dep);
            }
            EmployeeListDto employeeListDto = new EmployeeListDto()
            {
                EmployeeId = employeeFromDb.EmployeeId,
                EmployeeName = employeeFromDb.EmployeeName,
                DesignationId = employeeFromDb.Designation.DesignationId,
                EmployeeAddress = employeeFromDb.EmployeeAddress,
                Departments = departmentsNames
            };
            return Ok(employeeListDto);
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        { 
            var employeeDataFromDb = from employee in _context.Employees.Include(deg => deg.Designation)
                                     join employeeDepartment in _context.EmployeeDepartments
                                     on employee.EmployeeId equals employeeDepartment.EmployeeId
                                     select new EmployeeListDto
                                     {
                                         EmployeeId = employee.EmployeeId,
                                         EmployeeName = employee.EmployeeName,
                                         EmployeeAddress = employee.EmployeeAddress,
                                         EmployeeDesignationName = employee.Designation.DesignationName,
                                         DepartmentName = _context.EmployeeDepartments.Where(emp => emp.EmployeeId == employee.EmployeeId).
                                         Select(dep => dep.Department.DepartmentName).ToList()
                                     };
                  List<EmployeeListDto> employeeList = new List<EmployeeListDto>();
            foreach (var employee in employeeDataFromDb)
            {
                if (employeeList.FirstOrDefault(empInList => empInList.EmployeeId ==employee.EmployeeId) == null)
                {
                     employeeList.Add(employee);
                }
            }
                       return  Ok(employeeList);
        }
        [HttpPost]
        public IActionResult AddEmployee(EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid && employeeDto==null) return BadRequest();
            Employee employee = new Employee
            {
                    EmployeeId=employeeDto.EmployeeId,
                    EmployeeName=employeeDto.EmployeeName,
                    EmployeeAddress=employeeDto.EmployeeAddress,
                    DesignationId=employeeDto.DesignationId,
            };
                  _context.Employees.Add(employee);
                       _context.SaveChanges();
             List<EmployeeDepartment> departmentEmployees = new List<EmployeeDepartment>();
             foreach (var empDepId in  employeeDto.DepartmentsIds)
            {
                EmployeeDepartment EmployeeDepartment = new EmployeeDepartment()
                {
                    EmployeeId = employee.EmployeeId,
                    DepartmentId = empDepId
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
            if (!ModelState.IsValid && employeeDto==null) return BadRequest();
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
            List<EmployeeDepartment> employeeDepartments = new List<EmployeeDepartment>();
            foreach (var empDep in employeeDto.DepartmentsIds)
            {
                EmployeeDepartment employeeDepartment = new EmployeeDepartment()
                {
                    EmployeeId = employee.EmployeeId,
                    DepartmentId = empDep
                };
                employeeDepartments.Add(employeeDepartment);
            }
            _context.EmployeeDepartments.AddRange(employeeDepartments);
                  _context.SaveChanges();
            return Ok();
        }
        //[HttpPatch]
        //public IActionResult PatchEmployee([FromBody]JsonPatchDocument<Employee> patchEmployee,int id)
        //{
        //    var employeeInDb =_context.Employees.Find(id);
        //    if (employeeInDb == null)  return BadRequest(error: "Employee does not exist");
        //   patchEmployee.ApplyTo(employeeInDb);
        //      _context.SaveChanges();
        //           return Ok();
        //}

        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employeeInDb = _context.Employees.Find(id);
            if (employeeInDb == null) return BadRequest();
            _context.Remove(employeeInDb);
            _context.SaveChanges();
                   return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Models.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
       [Required]
        public string  EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public int DesignationId { get; set; }
        public List<int> DepartmentsIds { get; set; }
        public List<string> DepartmentsName { get; set; }
        public string  Departments { get; set; }
        public int DepartmentId { get; set; }
       

    }
}

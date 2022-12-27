using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Models.DTOs
{
    public class EmployeeListDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public int DesignationId { get; set; }
        public List<String> DepartmentName { get; set; }
        public List<int> DepartmentsIds { get; set;}
        public List<Department> Departments { get; set; }
        public string EmployeeDesignationName { get; set; }
    }
}

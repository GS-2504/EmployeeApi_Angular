using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Models.DTOs
{
    public class employeeDto
    {
        public int EmployeeId { get; set; }
        public string  EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public int DesignationId { get; set; }
        public List<int> DepartmentsIds { get; set; }
       // public ICollection<De> MyProperty { get; set; }

    }
}

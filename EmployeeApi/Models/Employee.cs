using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeAddress { get; set; }
        [Required]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
        public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
    }
}

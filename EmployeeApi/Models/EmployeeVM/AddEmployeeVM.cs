using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Models.EmployeeVM
{
    public class AddEmployeeVM
    {
        public Employee employee { get; set; }
        public IEnumerable<SelectListItem> Designations { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
    }
}

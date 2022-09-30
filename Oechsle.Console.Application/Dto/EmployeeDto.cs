using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oechsle.Console.Application.Dto
{
    public class EmployeeDto
    {
        public string status { get; set; }
        public dynamic data { get; set; }
        public List<Employee> Employees { get; set; }
    }
    
    public class Employee
    {
        public string employee_name { get; set; }
        public decimal employee_salary { get; set; }
        public int employee_age { get; set; }
        public string profile_image { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeInfo.Models
{
    public class SalaryDetails: EmployeeData
    {
        public float DearnessAllowance { get; set; }
        public float ConveyanceAllowance { get; set; }
        public float HouseRentAllowance { get; set; }
        public float GrossSalary { get; set; }
        public float PT { get; set; }
        public float TotalSalary { get; set; }

    }
}
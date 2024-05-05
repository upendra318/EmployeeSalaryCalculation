using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeInfo.Models
{
    public class ViewModelData
    {
        public List<EmployeeData> EmployeesData { get; set; }
        public List<SalaryDetails> EmployeesSalaryData { get; set; }
    }
}
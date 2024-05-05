using EmployeeInfo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeInfo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        
        public ActionResult Index()
        {
            db_firstEntities db_FirstEntities = new db_firstEntities();
            
            List<SalaryDetails> empData = calculateTotalSalary(db_FirstEntities.EmployeeDatas.ToList());

            return View(empData);
        }

        private List<SalaryDetails> calculateTotalSalary(List<EmployeeData> employeeDatas)
        {
            List<SalaryDetails> salaryDetailsList = new List<SalaryDetails>();
            foreach(var employee in employeeDatas)
            {
                SalaryDetails salaryDetails = new SalaryDetails();
                salaryDetails.EmployeeCode= employee.EmployeeCode;
                salaryDetails.EmployeeName= employee.EmployeeName;
                salaryDetails.Department= employee.Department;
                salaryDetails.Designation = employee.Designation;
                salaryDetails.BasicSalary = employee.BasicSalary;
                salaryDetails.DearnessAllowance = (float)(employee.BasicSalary * (40 / 100));
                salaryDetails.ConveyanceAllowance = (float)((employee.BasicSalary * (10 / 100) < 250)? 250 : employee.BasicSalary * (10 / 100));
                salaryDetails.HouseRentAllowance = (float)((employee.BasicSalary * (25 / 100) > 1500) ? 1500 : employee.BasicSalary * (25 / 100));
                salaryDetails.GrossSalary = (float)(employee.BasicSalary + salaryDetails.DearnessAllowance + salaryDetails.ConveyanceAllowance + salaryDetails.HouseRentAllowance);
                if(salaryDetails.GrossSalary <= 3000)
                {
                    salaryDetails.PT = 100;
                }
                else if(salaryDetails.GrossSalary > 3000 && salaryDetails.GrossSalary <= 6000)
                {
                    salaryDetails.PT = 150;
                }
                else
                {
                    salaryDetails.PT = 200;
                }

                salaryDetails.TotalSalary = salaryDetails.GrossSalary - salaryDetails.PT;
                
                salaryDetailsList.Add(salaryDetails);
            }
            return salaryDetailsList;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("create")]
        public ActionResult Create2()
        {

            db_firstEntities db_FirstEntities= new db_firstEntities();
            EmployeeData employee = new EmployeeData();
            //UpdateModel(employee); // UpdateModel() fn will inspects all HttpRequest input
            // and populate the employee obj
            TryUpdateModel(employee); // TryUpdateModel will not Throw an Error
            if (ModelState.IsValid)
            {
                db_FirstEntities.EmployeeDatas.Add(employee);
                db_FirstEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            db_firstEntities db_FirstEntities = new db_firstEntities();
            EmployeeData emp = db_FirstEntities.EmployeeDatas.Single(person => person.EmployeeCode == id);

            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeData employee)
        {
            if (ModelState.IsValid)
            {
                db_firstEntities db_FirstEntities = new db_firstEntities();
                db_FirstEntities.EmployeeDatas.AddOrUpdate(employee);
                db_FirstEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            db_firstEntities db_FirstEntities = new db_firstEntities();
            EmployeeData employee= db_FirstEntities.EmployeeDatas.Find(id);
            db_FirstEntities.EmployeeDatas.Remove(employee);
            db_FirstEntities.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            db_firstEntities db_FirstEntities = new db_firstEntities();
            EmployeeData emp = db_FirstEntities.EmployeeDatas.Single(e => e.EmployeeCode == id);
            return View(emp);

        }
    }
}
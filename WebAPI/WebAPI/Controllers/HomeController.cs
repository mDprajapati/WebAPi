using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI.Models;
using WebAPI.ViewModel;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private CFDB db = new CFDB();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Create()
        {
            EmployeeVM employeeVM = new EmployeeVM();
            ViewBag.Designation = new SelectList(db.Desigantions, "ID", "DesignationName");
            return View(employeeVM);
        }

        public ActionResult Update(int ID)
        {           
            var emp = db.Employees.Find(ID);
            if (emp == null)
            {
                return HttpNotFound();
            }
            else
            {
                EmployeeVM employee = new EmployeeVM();
                {
                    employee.ID = emp.ID;
                    employee.Name = emp.Name;
                    employee.Gender = emp.Gender;
                    employee.DesignationID = emp.DesignationID;
                    employee.Email = emp.Email;
                    employee.PhoneNumber = emp.PhoneNumber;
                    employee.Password = emp.Password;
                    employee.Profile = emp.Profile;
                }
                ViewBag.Designation = new SelectList(db.Desigantions, "ID", "DesignationName", employee.DesignationID);
                return View(employee);
            }
        }

        public ActionResult loginEmployee()
        {
            return View();
        }
    }
}

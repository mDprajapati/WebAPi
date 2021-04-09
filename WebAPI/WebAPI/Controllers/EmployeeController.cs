using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.ViewModel;
using System.Security.Claims;
using WebAPI.JWT;

namespace WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        private CFDB db = new CFDB();

        [HttpGet]
        [Route("api/getemployee")]

        public IHttpActionResult GetEmployee()
        {
            List<EmployeeVM> employeeVM = new List<EmployeeVM>();
            var getemployee = db.Employees.ToList();

            foreach (var item in getemployee)
            {
                EmployeeVM employee = new EmployeeVM();
                employee.ID = item.ID;
                employee.Name = item.Name;
                employee.Gender = item.Gender;
                employee.Email = item.Email;
                employee.DesignationID = item.DesignationID;
                employee.DesignationName = db.Desigantions.Find(item.DesignationID).DesignationName; 
                employee.PhoneNumber = item.PhoneNumber;
                employee.Profile = item.Profile;
                employeeVM.Add(employee);
            }
            return Ok(employeeVM);
        }
        [HttpPost]
        [Route("api/addemployee")]
        public IHttpActionResult AddEmployee(EmployeeVM employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.ID == 0)
                {
                    Employee employee1 = new Employee
                    {
                        ID = employee.ID,
                        Name = employee.Name,
                        Email = employee.Email,
                        Gender = employee.Gender,
                        DesignationID = employee.DesignationID,
                        PhoneNumber = employee.PhoneNumber,
                        Password = employee.Password,
                        Profile = employee.Profile
                    };
                    db.Employees.Add(employee1);
                    db.SaveChanges();
                }
                else
                {
                    Employee employee1 = new Employee
                    {
                        ID = employee.ID,
                        Name = employee.Name,
                        Email = employee.Email,
                        Gender = employee.Gender,
                        DesignationID = employee.DesignationID,
                        PhoneNumber = employee.PhoneNumber,
                        Password = employee.Password,
                        Profile = employee.Profile
                    };
                    db.Entry(employee1).State = EntityState.Modified;
                    
                    db.SaveChanges();
                }
                
            }
            return Ok(employee);
        }

        //[HttpDelete]
        //[Route("api/deleteemployee")]
        //public IHttpActionResult DeleteEmployee(int ID)
        //{
        //    var employe = db.Employees.Find(ID);
        //    if (employe != null)
        //    {
        //        db.Employees.Remove(employe);
        //        db.SaveChanges();
        //    }
        //    return Ok(employe);
        //}

        [HttpGet]
        [Route("api/GetemployeeByID")]
        public IHttpActionResult GetEmployeeByID(int ID)
        {
            var employe = db.Employees.Find(ID);
            if (employe == null)
            {
                return BadRequest();
            }
            else
            {
                EmployeeVM employee = new EmployeeVM
                {
                    Name = employe.Name,
                    Gender = employe.Gender,
                    Email = employe.Email,
                    DesignationID = employe.DesignationID,
                    PhoneNumber = employe.PhoneNumber,
                    Password = employe.Password,
                    Profile = employe.Profile,
                };
        }
            return Ok(employe);
    }

        [HttpPost]
        [Route("api/loginemployee")]
        public IHttpActionResult loginEmployee(EmployeeVM employee)
        {
            var login = db.Employees.Where(x => x.Email == employee.Email && x.Password == employee.Password).FirstOrDefault();
            if (login!=null)
            {
                employee.AccessToken = new Token().GenerateToken(login);
                return Ok(employee);
            }
            return BadRequest("Error"); 
        }
    }
}

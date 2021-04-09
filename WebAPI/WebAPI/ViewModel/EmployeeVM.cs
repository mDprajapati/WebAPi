using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.ViewModel
{
    public class EmployeeVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int DesignationID { get; set; }
        public string DesignationName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Profile { get; set; }        
        public string AccessToken { get; set; }
    }
}
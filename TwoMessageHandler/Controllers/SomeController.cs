using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TwoMessageHandler.Models;

namespace TwoMessageHandler.Controllers
{
    public class SomeController : ApiController
    {
        private static List<Models.Employee> employees = new List<Models.Employee>
           {
               new Models.Employee{Eid = 101,EName ="Tony",EDepartment="IronMan"},
               new Models.Employee{Eid = 102,EName ="Cpt. America",EDepartment="Shield"},
               new Models.Employee{Eid = 101,EName ="Hulk",EDepartment="Green"},
               new Models.Employee{Eid = 101,EName ="Scarlet witch",EDepartment="ether"}
           };
        [HttpGet]
        public HttpResponseMessage GetEmployees()
        {

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,employees);
            return response;
        }
        [HttpPost]
        public HttpResponseMessage PostEmployees(Employee employee)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,employee);
            return response;
        }
        [HttpPut]
        public HttpResponseMessage UpdateEmployee(int id)
        {
            Employee emp = new Employee
            {
                Eid = id,
                EName = id.ToString() + "Tudu",
                EDepartment = id.ToString() + "Railways"
            };
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,emp);
            return response;
        }
    }
}

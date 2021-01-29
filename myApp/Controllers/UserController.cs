using myApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace myApp.Controllers
{
    public class UserController : ApiController
    {
        Response response = new Response();
        EmployeedbEntities dalobj = new EmployeedbEntities();

        UserController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }


        // GET: api/User
        public Response Get()
        {
            List<T_Employee> list = dalobj.T_Employee.ToList();
            response.Data = list;
            response.Status = "success";
            response.Err = null;
            return response;
        }

        // GET: api/User/id
        [HttpGet]
        public Response Get1(int id)
        {
            var emptobefind = dalobj.T_Employee.Find(id);

            if(emptobefind != null)
            {
                response.Data = emptobefind;
                response.Status = "success";
                response.Err = null;
                return response;
            }
            else
            {
                response.Data = null;
                response.Status = "Not Found";
                response.Err = null;
                return response;
            }
        }

        // GET: api/User/SecondMaxSalay      //user with max salary
        [HttpGet]
        [Route("api/user/SecondMaxSalay")]
        public Response Get2()
        {
            List<T_Employee> list = dalobj.T_Employee.ToList();

            var employee = list.OrderByDescending(e => e.salary).Skip(1).FirstOrDefault();
            response.Data = employee;
            response.Status = "success";
            return response;
        }

        // POST: api/User
        public Response Post([FromBody]T_Employee employee)
        {
            if (employee != null)
            {
                dalobj.T_Employee.Add(employee);
                dalobj.SaveChanges();
                response.Status = "success";
                return response;
            }
            else
            {
                response.Status = "Failed";
                return response;
            }
        }

        // PUT: api/User/5
        public Response Put(int id, [FromBody]T_Employee employee)
        {
            if (employee != null)
            {

                T_Employee employeeToBeUpdated = dalobj.T_Employee.Find(id);

                if (employee.name != null)
                    employeeToBeUpdated.name = employee.name;
                if (employee.address != null)
                    employeeToBeUpdated.address = employee.address;
                if (employee.phone_number != null)
                    employeeToBeUpdated.phone_number = employee.phone_number;
                if (employee.email != null)
                    employeeToBeUpdated.email = employee.email;
                if (employee.salary != null)
                    employeeToBeUpdated.salary = employee.salary;
                
                dalobj.SaveChanges();

                response.Status = "success";
                response.Err = null;
                return response;
            }
            else
            {
                response.Status = "Failed";
                return response;
            }
        }

        // DELETE: api/User/5
        public Response Delete(int id)
        {
            T_Employee employeeToBeDeleted = dalobj.T_Employee.Find(id);
            dalobj.T_Employee.Remove(employeeToBeDeleted);
            dalobj.SaveChanges();
            response.Status = "success";

            return response;
        }
    }
}

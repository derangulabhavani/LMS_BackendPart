
using LMS_WEB_API_NetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly LMSDbContext context; 
        public EmployeeController(LMSDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await context.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [Route("Register")]
        [HttpPost]
        public object RegisterEmployee(Employee employee)
        {
            try
            {

                context.Employees.Add(employee);
                context.SaveChanges();
                return new Response { Status = "Success", Message = "Record SuccessFully Saved." };
            }
            catch (Exception)
            {

                return new Response { Status = "Error", Message = "Error occured." };
            }
        }

        [Route("Login")]
        [HttpPost]
        public Response EmployeeLogin(Login login)
        {
            var eid = int.Parse(login.EmployeeId);
            var log = context.Employees.Find(eid);



            if (log == null)
            {
                return new Response { Status = "Invalid", Message = "No user exist." };
            }
            else if (log.Password != login.Password)
                return new Response { Status = "Invalid", Message = "Incorrect password" };
            else
                return new Response { Status = "Success", Message = "Login Successful" };
        }

        [Route("Managerdetails")]
        [HttpGet]
        public Employee GetManagerDetails(int id)
        {
            var managername = context.Employees.Find(id).ManagerName;
            var manager = context.Employees.FirstOrDefault(m => m.Name == managername);
            return manager;
        }
        [Route("EmployeebyLeaveId")]
        [HttpGet]
        public Employee GetEmployeebyLeaveid(int lid)
        {
            var eid = context.ApplyLeave.Find(lid).EmployeeId;
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == int.Parse(eid));
            return employee;
        }
    }
}


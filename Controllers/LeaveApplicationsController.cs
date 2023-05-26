using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS_WEB_API_NetCore.Models;

namespace LMS_WEB_API_NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveApplicationsController : ControllerBase
    {
        private readonly LMSDbContext _context, _context2, _context3;

        public LeaveApplicationsController(LMSDbContext context)
        {
            _context = _context2 = _context3 = context;
        }

        // GET: api/LeaveApplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> GetApplyLeave()
        {
            return await _context.ApplyLeave.ToListAsync();
        }

        [Route("Leavedetailsbyemployee")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> GetLeaveDetails(string eid)
        {
            var result = await _context.ApplyLeave.Where(e => e.EmployeeId == eid).ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [Route("GetPendingLeaveapplications")]
        [HttpPost]
        public IEnumerable<LeaveApplication> GetPendingLeaveapplications(string eid)//eid is manager's id
        {
            var manager = _context.Employees.Find(int.Parse(eid)).Name;
            var employees = _context2.Employees.AsEnumerable().Where(e => e.ManagerName == manager).ToList();
            var pendingleaves = _context3.ApplyLeave.AsEnumerable().Where(l => l.Status == "Pending").ToList();
            var result = pendingleaves.Where(l => employees.Any(e => int.Parse(l.EmployeeId) == e.EmployeeId)).ToList();

            return result;
        }
        [Route("Actionbymanager")]
        [HttpPatch]
        public string ActionByManager(int lid, string status, string comment)
        {
            var leave = _context.ApplyLeave.FirstOrDefault(l => l.LeaveApplicationId == lid);
            leave.Status = status;
            leave.ManagerComments = comment;
            _context.SaveChanges();
            return "Success";
        }

        // GET: api/LeaveApplications/5
        [Route("Leavedetailsbyid")]
        [HttpGet]
        public async Task<ActionResult<LeaveApplication>> GetLeaveDetailsbyId(int id)
        {
            var leaveDetails = await _context.ApplyLeave.FindAsync(id);

            if (leaveDetails == null)
            {
                return NotFound();
            }

            return leaveDetails;
        }
        [Route("overlappingDates")]
        [HttpGet]
        public Response OverlapDates(string startDate, string endDate)
        {

            var leave = _context.ApplyLeave.Where(l => l.StartDate.Equals(DateTime.Parse(startDate)) && l.EndDate.Equals(DateTime.Parse(endDate)));
            if (leave.Count() == 0)
            {
                return new Response { Status = "Success", Message = "Not Overlap" };
            }
            else
            {
                return new Response { Status = "Invalid", Message = "Overlaped" };
            }
        }

        // PUT: api/LeaveApplications/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveApplication(int id, LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.LeaveApplicationId)
            {
                return BadRequest();
            }

            _context.Entry(leaveApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveApplicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LeaveApplications
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LeaveApplication>> PostLeaveApplication(LeaveApplication leaveApplication)
        {
            _context.ApplyLeave.Add(leaveApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveApplication", new { id = leaveApplication.LeaveApplicationId }, leaveApplication);
        }

        // DELETE: api/LeaveApplications/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<LeaveApplication>> DeleteLeaveApplication(int id)
        //{
        //    var leaveApplication = await _context.ApplyLeave.FindAsync(id);
        //    if (leaveApplication == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.ApplyLeave.Remove(leaveApplication);
        //    await _context.SaveChangesAsync();

        //    return leaveApplication;
        //}

        private bool LeaveApplicationExists(int id)
        {
            return _context.ApplyLeave.Any(e => e.LeaveApplicationId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_WEB_API_NetCore.Models
{
    public class LeaveApplication
    {
        public int LeaveApplicationId { get; set; }
        public string NumberOfDays { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public string LeaveType { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Required")]
        public string LeaveReason { get; set; }
        [Required(ErrorMessage = "Required")]
        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Date)]
        public string AppliedOn { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ManagerComments { get; set; }

    }
}

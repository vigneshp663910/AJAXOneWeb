using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class PEmployee
    {      
        public int EID { get; set; }
        public string EmployeeUserID { get; set; }
        public string EmployeeName { get; set; }
        public int EmpId { get; set; }
        public string Mail1 { get; set; }
        public string Phone { get; set; }
        public int? ReportingTo { get; set; }
        public PDepartment Department { get; set; }
        public int UserTypeID { get; set; }
        public Boolean IsActive { get; set; }
    }
}

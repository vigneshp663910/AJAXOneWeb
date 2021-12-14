using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
     [Serializable]
    
   public class PDealerEmployee
    {       
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeType { get; set; }
        public string Department { get; set; }

        public PDMS_Dealer Dealer { get; set; }
    }
}

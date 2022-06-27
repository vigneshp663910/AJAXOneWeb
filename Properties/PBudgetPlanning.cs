using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    public class PBudgetPlanning
    {
        
    }
    public class PBudgetPlanningYearWise
    {
        public long BudgetPYWiseID { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Model Model { get; set; }
        public int Year { get; set; }
        public int Budget { get; set; }
        public int Actual { get; set; }
        public Boolean Freezed { get; set; } 
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public PUser ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

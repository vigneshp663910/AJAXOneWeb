using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    public class PColdVisit
    {
        public long ColdVisitID { get; set; }
        public string ColdVisitNumber { get; set; }
        public DateTime ColdVisitDate { get; set; }
        public PActionType ActionType { get; set; }
        public PImportance Importance { get; set; }
        public PPreSaleStatus Status { get; set; }
        public PDMS_Customer Customer { get; set; }
        public string Remark { get; set; }
        public string Location { get; set; }
        public PUser CreatedBy { get; set; }
        public long? ReferenceTableID { get; set; }
        public long? ReferenceID { get; set; }
        public PDealer Dealer { get; set; }
    }
    public class PActionType
    {
        public int ActionTypeID { get; set; }
        public string ActionType { get; set; }
    }
    public class PVisitTarget
    {
        public long VisitTargetID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month).Substring(0, 3);
            }
        }
        public PDMS_Dealer Dealer { get; set; }
        public PUser Engineer { get; set; }
        public int NewCustomerTarget { get; set; }
        public int ProspectCustomerTarget { get; set; }
        public int ExistCustomerTarget { get; set; }
        public int TotalTarget
        {
            get
            {
                return NewCustomerTarget + ProspectCustomerTarget + ExistCustomerTarget;
            }
        }
        public int NewCustomerActual { get; set; }
        public int ProspectCustomerActual { get; set; }
        public int ExistCustomerActual { get; set; }
        public int TotalActual
        {
            get
            {
                return NewCustomerActual + ProspectCustomerActual + ExistCustomerActual;
            }
        } 
        public PUser CreatedBy { get; set; }
    }
}

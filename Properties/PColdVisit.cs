using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
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
        public PCustomerRelation PersonMet { get; set; }
        public string Location { get; set; }
        public PUser CreatedBy { get; set; }
        public long? ReferenceTableID { get; set; }
        public long? ReferenceID { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public PDealer Dealer { get; set; }
    }
    [Serializable]
    public class PColdVisit_Insert
    {
        public long ColdVisitID { get; set; }
        public DateTime ColdVisitDate { get; set; }
        public PActionType ActionType { get; set; }
        public int CustomerVisitTypeID { get; set; }
        public int CallTypeID { get; set; }
        public PImportance Importance { get; set; }
        public PDMS_Customer_Insert Customer { get; set; }
        public string Remark { get; set; }
        public long? PersonMet { get; set; }
        public string Location { get; set; }
        public long? ReferenceTableID { get; set; }
        public long? ReferenceID { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
    [Serializable]
    public class PActionType
    {
        public int ActionTypeID { get; set; }
        public string ActionType { get; set; }
    }
    [Serializable]
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
    [Serializable]
    public class PPreSalesMasterItem
    {
        public int MasterItemID { get; set; }
        public string ItemText { get; set; }
    }
}

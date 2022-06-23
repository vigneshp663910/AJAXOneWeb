using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
   public class PProject
    {
        public long ProjectID { get; set; }
        public string ProjectNumber { get; set; }
        public DateTime EmailDate { get; set; }
        public string TenderNumber { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_District District { get; set; }
        public decimal Value { get; set; }
        public string L1ContractorName { get; set; }
        public string L1ContractorAddress { get; set; }
        public string L2Bidder { get; set; }
        public string L3Bidder { get; set; }
        public DateTime ContractAwardDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string Remarks { get; set; }
        public string ProjectName { get; set; }
        public PUser SalesPersonAssigned { get; set; }
        public DateTime? DateOfVisit { get; set; }
        public Boolean L1ContractorMet { get; set; }
        public Boolean L2ContractorMet { get; set; }
        public Boolean L3ContractorMet { get; set; }
        //public string CustomerType { get; set; }
        //public string MachinesRequiredForThisProejct { get; set; }
        //public int IfYesWhichMacinesRequired { get; set; }
        //public string QuotationNo { get; set; }
        //public string StatusOfQuotation { get; set; }  
    }
}

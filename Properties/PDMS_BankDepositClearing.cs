using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_BankDepositClearing
    {
        public long BankDepositClearingID { get; set; }        
        public PDMS_Dealer Dealer { get; set; }
        public string BankAccount { get; set; } 
        public DateTime TransactionDate { get; set; }
        public DateTime ValueDate { get; set; }
        public string BankDescription { get; set; }
        public string BranchCode { get; set; }
        public decimal Amount { get; set; }


        public Boolean IsMultipleCustomer { get; set; }
        public PDMS_Customer Customer { get; set; }
        public string DepositFor { get; set; }  
        public string InvoiceNumber{ get; set; }
        public string PONumber { get; set; }
        public string SONumber { get; set; }

        public string MachineModel { get; set; }
        public string Department { get; set; }
        public List<PUser> MailTo { get; set; }

        public string Place { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_Region Region { get; set; }

        public string BillDetailGivenBy { get; set; }
        public DateTime? BillDetailUpdatedOn { get; set; }
        public string Remarks { get; set; }

        public PDMS_BankDepositClearingStatus Status { get; set; }

        public PUser AccountedBy { get; set; }
        public DateTime? AccountedOn { get; set; }
        public string SapAccountNo { get; set; }
        public DateTime? SapPostedOn { get; set; }
        public DateTime? SapClearedOn { get; set; }



        public string ReferenceNo { get; set; }
        public string HeaderText { get; set; }
        public string Assignment { get; set; }   
       
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }


        public string RemitterAccount { get; set; }
        public string RemitterName { get; set; }
        public string RemitterEmail { get; set; }
        public string RemitterMobile { get; set; }
        public string RemitterBank { get; set; }
        public string RemitterIFSC { get; set; }



    }
    [Serializable]
    public class PDMS_BankDepositClearingStatus
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }
}

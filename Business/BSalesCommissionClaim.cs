using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
   public class BSalesCommissionClaim
    {
        public List<PSalesQuotation> GetSalesQuotationForClaimCreate(string QuotationNo, DateTime? QuotationDateFrom, DateTime? QuotationDateTo, int? DealerID)
        {
            string endPoint = "SalesCommission/SalesQuotationForClaimCreate?QuotationNo=" + QuotationNo + "&QuotationDateFrom=" + QuotationDateFrom + "&QuotationDateTo=" + QuotationDateTo
               + "&DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<List<PSalesQuotation>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public int InsertSalesCommissionClaim(long SalesQuotationID)
        {
            string endPoint = "SalesCommission/InsertSalesCommissionClaim?SalesQuotationID=" + SalesQuotationID;
            return JsonConvert.DeserializeObject<int>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PSalesCommissionClaim> GetSalesCommissionClaim(long? SalesCommissionClaimID, long? SalesQuotationID
         , string ClaimNumber, string ClaimDateFrom, string ClaimDateTo, int? StatusID, int? DealerID)
        {
            string endPoint = "SalesCommission/SalesCommissionClaim?SalesCommissionClaimID=" + SalesCommissionClaimID + "&SalesQuotationID=" + SalesQuotationID + "&ClaimNumber=" + ClaimNumber
                + "&ClaimDateFrom=" + ClaimDateFrom + "&ClaimDateTo=" + ClaimDateTo + "&StatusID=" + StatusID + "&DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<List<PSalesCommissionClaim>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }
        public PSalesCommissionClaim GetSalesCommissionClaimByID(long SalesCommissionClaimID)
        {
            string endPoint = "SalesCommission/GetSalesCommissionClaimByID?SalesCommissionClaimID=" + SalesCommissionClaimID;
            return JsonConvert.DeserializeObject<PSalesCommissionClaim>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }
        public List<PSalesCommissionClaim> GetSalesCommissionClaimApproval(int? DealerID, string ClaimNumber, string ClaimDateFrom, string ClaimDateTo, int? StatusID)
        {
            string endPoint = "SalesCommission/ClaimForApproval?DealerID=" + DealerID + "&ClaimNumber=" + ClaimNumber + "&ClaimDateFrom=" + ClaimDateFrom + "&ClaimDateTo=" + ClaimDateTo + "&StatusID=" + StatusID;
            return JsonConvert.DeserializeObject<List<PSalesCommissionClaim>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PSalesCommissionClaim> GetSalesCommissionClaimForInvoiceCreate(int? DealerID, string ClaimNumber, string ClaimDateFrom, string ClaimDateTo)
        {
            string endPoint = "SalesCommission/ClaimForInvoiceCreate?DealerID=" + DealerID + "&ClaimNumber=" + ClaimNumber + "&ClaimDateFrom=" + ClaimDateFrom + "&ClaimDateTo=" + ClaimDateTo;
            return JsonConvert.DeserializeObject<List<PSalesCommissionClaim>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data)); 
        } 
    }
}

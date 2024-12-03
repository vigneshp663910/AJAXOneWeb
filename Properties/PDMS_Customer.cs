using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{

    [Serializable]
    public class PDMS_Customer
    {
        public long CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerCodeWithOutZero
        {
            get
            {
                if (string.IsNullOrEmpty(CustomerCode))
                    return "";
                return Convert.ToString(Convert.ToInt64(CustomerCode));
            }

        }
        public string CustomerName { get; set; }
        //public string CustomerName2 { get; set; }

        //public string CustomerFullName
        //{
        //    get
        //    {
        //        return (CustomerName + " " + CustomerName2).Trim() + (string.IsNullOrEmpty(CustomerCodeWithOutZero) ? "" : " (" + CustomerCodeWithOutZero + ")");
        //    }
        //}

        public string CustomerFullName
        {
            get
            {
                return CustomerName + (string.IsNullOrEmpty(CustomerCodeWithOutZero) ? "" : " (" + CustomerCodeWithOutZero + ")");
            }
        }

        public string GSTIN { get; set; }
        public string PAN { get; set; }

        public string OfficeID { get; set; }

        public string ContactPerson { get; set; }
        public string Mobile { get; set; }
        public string AlternativeMobile { get; set; }
        public string Email { get; set; }

        public string Address12 { get; set; }
        public PCustomerType Type { get; set; }
        public PCustomerTitle Title { get; set; }
        public PDMS_CustomerCategory CustomerCategory { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Pincode { get; set; }
        public string CIN { get; set; }
        public string Web { get; set; }
        public PDMS_Country Country { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_District District { get; set; }
        public PDMS_Tehsil Tehsil { get; set; }
        public string City { get; set; }
        public PUser CreatedBy { get; set; }


        public DateTime? DOB { get; set; }
        public DateTime? DOAnniversary { get; set; }
        public Boolean SendSMS { get; set; }
        public Boolean SendEmail { get; set; }


        public Boolean IsVerified { get; set; }
        public PUser VerifiedBy { get; set; }
        public DateTime? VerifiedOn { get; set; }

        public Boolean IsActive { get; set; }
        public Boolean OrderBlock { get; set; }
        public Boolean DeliveryBlock { get; set; }
        public Boolean BillingBlock { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public Boolean IsDraft { get; set; }
        public string CustomerType { get; set; }
        public DateTime? LastVisitDate { get; set; }

        public Boolean IsFinanceVerified { get; set; }
        public PUser FinanceVerifiedBy { get; set; }
        public DateTime? FinanceVerifiedOn { get; set; }
        public PCustomerSalesType SalesType { get; set; }
        
    }
    [Serializable]
    public class PDMS_Customer_Insert
    {
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerName2 { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public string ContactPerson { get; set; }
        public string Mobile { get; set; }
        public string AlternativeMobile { get; set; }
        public string Email { get; set; }
        public PCustomerTitle Title { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Pincode { get; set; }
        public PDMS_Country Country { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_District District { get; set; }
        public PDMS_Tehsil Tehsil { get; set; }
        public string City { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? DOAnniversary { get; set; }
        public Boolean SendSMS { get; set; }
        public Boolean SendEmail { get; set; }
        public Boolean IsDraft { get; set; }
        public int CustomerSalesTypeID { get; set; }
    }
    [Serializable]
    public class PDMS_CustomerShipTo
    {
        public long CustomerShipToID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerCodeWithOutZero
        {
            get
            {
                if (string.IsNullOrEmpty(CustomerCode))
                    return "";
                return Convert.ToString(Convert.ToInt64(CustomerCode));
            }

        }
        public string ContactPerson { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Pincode { get; set; }
        public PDMS_Country Country { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_District District { get; set; }
        public PDMS_Tehsil Tehsil { get; set; }
        public string City { get; set; }
        public PUser CreatedBy { get; set; }
        public Boolean IsActive { get; set; }
    }
    [Serializable]
    public class PCustomerTitle
    {
        public long TitleID { get; set; }
        public string Title { get; set; }
        public string TitleCode { get; set; }
    }
    [Serializable]
    public class PCustomerType
    {
        public int CustomerTypeID { get; set; }
        public string CustomerType { get; set; }
    }
    [Serializable]
    public class PDMS_CustomerCategory
    {
        public int CustomerCategoryID { get; set; }
        public string CustomerCategory { get; set; }
    }
    [Serializable]
    public class PCustomerProduct
    {
        public long CustomerProductID { get; set; }
        public long CustomerID { get; set; }
        public long ICTicketID { get; set; }

        public PMake Make { get; set; }
        public PProductType ProductType { get; set; }
        public PProduct Product { get; set; }
        public int Quantity { get; set; }
        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

    }
    [Serializable]
    public class PCustomerRelation
    {
        public long CustomerRelationID { get; set; }
        public long CustomerID { get; set; }


        public string DecisionMaker { get; set; }
        //public string OrgName { get; set; }

        public string ContactName { get; set; }
        public string Mobile { get; set; }

        public PRelation Relation { get; set; }

        public DateTime? DOB { get; set; }
        public DateTime? DOAnniversary { get; set; }
        public PUser CreatedBy { get; set; }
        public PCustomerEmployeeDesignation Designation { get; set; }

    }
    [Serializable]
    public class PCustomerAttribute
    {
        public long CustomerAttributeID { get; set; }
        public long CustomerID { get; set; }
        public PCustomerAttributeMain AttributeMain { get; set; }
        public PCustomerAttributeSub AttributeSub { get; set; }
        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
    }
    [Serializable]
    public class PCustomerAttributeMain
    {
        public int AttributeMainID { get; set; }
        public string AttributeMain { get; set; }
    }
    [Serializable]
    public class PCustomerAttributeSub
    {
        public int AttributeSubID { get; set; }
        public int AttributeMainID { get; set; }
        public string AttributeSub { get; set; }
        public PCustomerAttributeMain AttributeMain { get; set; }
    }
    [Serializable]
    public class PCustomerResponsibleEmployee
    {
        public long CustomerResponsibleEmployeeID { get; set; }
        public long CustomerID { get; set; }
        public PDMS_DealerEmployee Employee { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    [Serializable]
    public class PCustomerFleet
    {
        public long CustomerFleetID { get; set; }
        public long CustomerID { get; set; }
        public PDMS_Customer Fleet { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    
    [Serializable]
    public class PCustomerGSTApproval
    {
        public long CustomerGSTApprovalID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string OldGSTIN { get; set; }
        public string OldPAN { get; set; }
        public Boolean Unregistered { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public Boolean? IsApproved { get; set; }
        public PUser ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string ApproverRemark { get; set; }
        public int? SendSAP { get; set; }
        public int? Success { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    [Serializable]
    public class PCustomerEmployeeDesignation
    {
        public int DesignationID { get; set; }
        public string Designation { get; set; }
        public string DesignationCode { get; set; }
    }

    [Serializable]
    public class PCustomerSalesType
    {
        public long SalesTypeID { get; set; }
        public string SalesType { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_Address
    {
        public string Code { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Fax { get; set; }
        public string PrimaryContact { get; set; }
        public string PinCode { get; set; }
        public PDMS_District District { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_Region Region { get; set; }
        public PDMS_Country Country { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }

    }
    [Serializable]
    public class PCurrency
    {
        public int CurrencyID { get; set; }
        public string Currency { get; set; }
    }
    [Serializable]
    public class PDMS_Country
    {
        public int CountryID { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public PCurrency Currency { get; set; }
        public string SalesOrganization { get; set; }
    }
    [Serializable]
    public class PDMS_State
    {
        public int StateID { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }
        public string StateSAP { get; set; }
        public PDMS_Country Country { get; set; }
        public PDMS_Region Region { get; set; }
    }
    [Serializable]
    public class PDMS_Region
    {
        public int RegionID { get; set; }
        public string Region { get; set; }
        public PDMS_Country Country { get; set; }
    }
    [Serializable]
    public class PDMS_District
    {
        public int DistrictID { get; set; }
        public string District { get; set; }
        public Boolean Hilly { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_Country Country { get; set; }
        public PSalesOffice SalesOffice { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PUser SalesDealerEngineer { get; set; }
        public PDMS_Dealer ServiceDealer { get; set; }
        public PDMS_Dealer SalesRetailer { get; set; }
        public PUser SalesRetailerEngineer { get; set; }
        public PDMS_Dealer ServiceRetailer { get; set; }
    }
    [Serializable]
    public class PDMS_Tehsil
    {
        public int TehsilID { get; set; }
        public string Tehsil { get; set; }
        public PDMS_District District { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_Country Country { get; set; }
    }
    [Serializable]
    public class PDMS_Village
    {
        public int VillageID { get; set; }
        public string Village { get; set; }
        public PDMS_Tehsil Tehsil { get; set; }

    }
    [Serializable]
    public class PSalesOffice
    {
        public int SalesOfficeID { get; set; }
        public string SalesOffice { get; set; }
        public string SalesGroup { get; set; }
        public string SalesOfficeDescription { get; set; }
        public string SalesGroupDescription { get; set; }
    }
}

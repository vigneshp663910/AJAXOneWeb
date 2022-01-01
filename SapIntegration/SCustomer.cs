using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class SCustomer
    {
        public PDMS_Customer getCustomerAddress(string CustomerCode)
        {
            PDMS_Customer Cust = new PDMS_Customer();

            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZBAPI_GET_CUST_ADDRESS");
            tagListBapi.SetValue("V_KUNNR", CustomerCode.PadLeft(10, '0'));
            tagListBapi.Invoke(SAP.RfcDes());
            Cust.CustomerCode = CustomerCode;
            Cust.CustomerName = tagListBapi.GetString("NAME1").Trim();
            Cust.Address1 = tagListBapi.GetString("ADD1").Trim();
            Cust.Address2 = tagListBapi.GetString("ADD2").Trim();
            Cust.Address3 = tagListBapi.GetString("ADD3").Trim();
            Cust.City = tagListBapi.GetString("CITY").Trim();
            Cust.State = new PDMS_State() { State = tagListBapi.GetString("STATE").Trim(), StateCode = tagListBapi.GetString("STATE_CODE").Trim() }; 
            Cust.Pincode = tagListBapi.GetString("PINCODE").Trim();
            Cust.GSTIN = tagListBapi.GetString("GST").Trim();
            Cust.PAN = tagListBapi.GetString("PAN").Trim();

            Cust.Address12 = (Cust.Address1.Trim(',', ' ') + "," + Cust.Address2.Trim(',', ' ')).Trim(',', ' ');

            Cust.Mobile = tagListBapi.GetString("MOBILE");
            Cust.Email = tagListBapi.GetString("EMAIL");
            Cust.ContactPerson = tagListBapi.GetString("CON_NAME");
            return Cust;
        }

        public List<PDMS_Address> getCustomerShipToAddress(string CustomerCode)
        {
            List<PDMS_Address> Custs = new List<PDMS_Address>();

            PDMS_Address Cust = null;

            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSD_SHIPMENT_DETAILS");
            tagListBapi.SetValue("CUSTOMER", CustomerCode.PadLeft(10, '0'));
            tagListBapi.Invoke(SAP.RfcDes());

            IRfcTable IT_DET = tagListBapi.GetTable("IT_DET");
            for (int i = 0; i < IT_DET.RowCount; i++)
            {
                IT_DET.CurrentIndex = i;
                Cust = new PDMS_Address();
                Custs.Add(Cust);
                Cust.Code = IT_DET.GetString("KUNNR").Trim();
                Cust.Address1 = IT_DET.GetString("NAME1").Trim();
                Cust.Address2 = IT_DET.GetString("STREET").Trim();
                Cust.City = IT_DET.GetString("CITY").Trim();
                Cust.State = new PDMS_State() { State = IT_DET.GetString("BEZEI").Trim() };
                Cust.District = new PDMS_District() { District = IT_DET.GetString("DISTRICT").Trim() };
                Cust.PostalCode = IT_DET.GetString("POST_CODE1").Trim();
            }
            return Custs;
        }
    }
}

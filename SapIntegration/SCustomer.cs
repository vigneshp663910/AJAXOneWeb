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
        public string CreateCustomerInSAP(List<PDMS_Customer> Customer)
        {
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSD_CUSTOMER_CREATE_NEW");
            //IRfcStructure tagTable = tagListBapi.GetStructure("IT_STATUS");
            Int32 country = Customer[0].Country.CountryID;
            tagListBapi.SetValue("MODE", "N");
            tagListBapi.SetValue("P_COMPANYCODE", "AF");
            tagListBapi.SetValue("P_SORG", (country == 1) ? "AJF" : "AJI");
            tagListBapi.SetValue("P_DIS_CH", (country == 1) ? "GT" : "EX");
            tagListBapi.SetValue("P_DIVISION", "CM");
            tagListBapi.SetValue("P_ACC_GROUP", (country == 1) ? "AJGT" : "AJIC");
            tagListBapi.SetValue("P_TITLE", Customer[0].Title.Title);
            tagListBapi.SetValue("P_NAME1", Customer[0].CustomerName);
            tagListBapi.SetValue("P_NAME2", Customer[0].CustomerName2);
            tagListBapi.SetValue("P_NAME3", "");
            tagListBapi.SetValue("P_NAME4", "");
            tagListBapi.SetValue("P_STREET2", Customer[0].Address1);
            tagListBapi.SetValue("P_STREET3", Customer[0].Address2);
            tagListBapi.SetValue("P_STREET4", Customer[0].Tehsil.Tehsil);
            tagListBapi.SetValue("P_HOUSE_NO", Customer[0].Address3);
            tagListBapi.SetValue("P_DISTRICT", Customer[0].District.District);
            tagListBapi.SetValue("P_POSTCODE", Customer[0].Pincode);
            tagListBapi.SetValue("P_CITY", Customer[0].City);
            tagListBapi.SetValue("P_COUNTRY", Customer[0].Country.CountryCode);
            tagListBapi.SetValue("P_REGION", Convert.ToString(Customer[0].State.StateCode));
            tagListBapi.SetValue("P_LANGU", "EN");
            tagListBapi.SetValue("P_PHONE", Customer[0].Mobile);
            tagListBapi.SetValue("P_MOBILE", Customer[0].AlternativeMobile);
            tagListBapi.SetValue("P_EMAIL", Customer[0].Email);
            tagListBapi.SetValue("P_GSTIN", Customer[0].GSTIN);
            tagListBapi.SetValue("P_PANNO", Customer[0].PAN);
            tagListBapi.SetValue("P_CONTACT", Customer[0].ContactPerson);
            tagListBapi.SetValue("P_CONTACT2", Customer[0].ContactPerson);
            tagListBapi.SetValue("P_GL", (country == 1) ? "166104" : "166102");
            tagListBapi.SetValue("P_SALES_DISTRICT", "SOUTH1");
            tagListBapi.SetValue("P_ORD_PROB", "000");
            tagListBapi.SetValue("P_SALES_OFFICE", "KA10");
            tagListBapi.SetValue("P_SALES_GROUP", "100");
            tagListBapi.SetValue("P_CUS_GROUP", "GT");
            tagListBapi.SetValue("P_CURRENCY", (country == 1) ? "INR" : "USD");
            tagListBapi.SetValue("P_EXG_RATE_TYPE", (country == 1) ? "" : "SELL");
            tagListBapi.SetValue("P_PRICE_GROUP", (country == 1) ? "07" : "08");
            tagListBapi.SetValue("P_PRICING_PROCED", (country == 1) ? "7" : "9");
            tagListBapi.SetValue("P_CUST_STAT_GRP", "1");
            tagListBapi.SetValue("P_DEL_PRIORITY", "2");
            tagListBapi.SetValue("P_ORD_COMB_IND", "X");//Checkbox
            tagListBapi.SetValue("P_SHIP_COND", "01");
            tagListBapi.SetValue("P_DEL_ALLOW", "9");
            tagListBapi.SetValue("P_INV_DATE", "AF");
            tagListBapi.SetValue("P_INV_LIST_SCH", "AF");
            tagListBapi.SetValue("P_INCO_TERMS", "EXW");
            tagListBapi.SetValue("P_INCO_TERMS1", "EX WORKS");
            tagListBapi.SetValue("P_PYMT_TERMS", "C000");
            tagListBapi.SetValue("P_ACC_GROUP1", (country == 1) ? "01" : "02");
            tagListBapi.SetValue("P_TAX_CLASS1", (country == 1) ? "0" : "1");
            tagListBapi.SetValue("P_TAX_CLASS2", (country == 1) ? "0" : "1");
            tagListBapi.SetValue("P_TAX_CLASS3", (country == 1) ? "0" : "1");
            tagListBapi.SetValue("P_TAX_CLASS4", (country == 1) ? "0" : "1");
            tagListBapi.SetValue("P_TAX_CLASS5", (country == 1) ? "0" : "1");
            tagListBapi.SetValue("P_TAX_CLASS6", (country == 1) ? "0" : "1");
            tagListBapi.SetValue("P_TAX_CLASS7", (country == 1) ? "0" : "1");
            tagListBapi.SetValue("P_TAX_CLASS8", (country == 1) ? "0" : "1");
            tagListBapi.SetValue("P_TAX_CLASS9", (country == 1) ? "0" : "1");

            tagListBapi.Invoke(SAP.RfcDes());
            string CustomerCode=tagListBapi.GetValue("CUSTOMER").ToString();

            return CustomerCode;
        }
    }
}

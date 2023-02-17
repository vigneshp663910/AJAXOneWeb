using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
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
        public DataTable CreateCustomerInSAP(PDMS_Customer Customer, Boolean IsShipTo)
        { 
             
            List<string> Name = AddressSplit(Customer.CustomerName);
            Customer.CustomerName = Name[0];
            string CustomerName2 = Name[1];
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSD_CUSTOMER_CREATE_NEW");
            tagListBapi.SetValue("MODE", "N");
            tagListBapi.SetValue("P_COMPANYCODE", (IsShipTo == false)?"AF":"");
            tagListBapi.SetValue("P_PARTNER", (IsShipTo == true) ? "WE":"AG");
            tagListBapi.SetValue("P_TITLE", Customer.Title.Title);
            tagListBapi.SetValue("P_NAME1", Customer.CustomerName);
            tagListBapi.SetValue("P_NAME2", CustomerName2);
            // tagListBapi.SetValue("P_NAME2", Customer.CustomerName2);
            tagListBapi.SetValue("P_NAME3", "");
            tagListBapi.SetValue("P_NAME4", "");
            tagListBapi.SetValue("P_STREET2", Customer.Address1);
            tagListBapi.SetValue("P_STREET3", Customer.Address2);
            tagListBapi.SetValue("P_STREET4", Customer.Address3/*Customer.Tehsil.Tehsil*/);
            tagListBapi.SetValue("P_HOUSE_NO", "");
            tagListBapi.SetValue("P_DISTRICT", Customer.District.District);
            tagListBapi.SetValue("P_POSTCODE", Customer.Pincode);
            tagListBapi.SetValue("P_CITY", string.IsNullOrEmpty(Customer.City) ? Customer.District.District : Customer.City);
            tagListBapi.SetValue("P_COUNTRY", Customer.Country.CountryCode);
            tagListBapi.SetValue("P_REGION", Convert.ToString(Customer.State.StateCode));
            tagListBapi.SetValue("P_PHONE", Customer.Mobile);
            tagListBapi.SetValue("P_MOBILE", Customer.AlternativeMobile);
            tagListBapi.SetValue("P_EMAIL", Customer.Email);
            if (Customer.IsFinanceVerified)
            {
                tagListBapi.SetValue("P_GSTIN", Customer.GSTIN);
                tagListBapi.SetValue("P_PANNO", Customer.PAN);
            }
            tagListBapi.SetValue("P_CONTACT", Customer.ContactPerson);
            tagListBapi.SetValue("P_CONTACT2", Customer.ContactPerson);

            tagListBapi.Invoke(SAP.RfcDes());
            string CustomerCode = !string.IsNullOrEmpty(tagListBapi.GetValue("CUSTOMER").ToString()) ? tagListBapi.GetValue("CUSTOMER").ToString().Remove(0, 4) : "";

            IRfcTable table = tagListBapi.GetTable("IT_STATUS");

            DataTable dtRet = new DataTable();

            for (int Column = 0; Column < 13; Column++)
            {
                RfcElementMetadata rfcEMD = table.GetElementMetadata(Column);
                dtRet.Columns.Add(rfcEMD.Name);
            }

            foreach (IRfcStructure row in table)
            {
                DataRow dr = dtRet.NewRow();
                for (int Column = 0; Column < 13; Column++)
                {
                    RfcElementMetadata rfcEMD = table.GetElementMetadata(Column);
                    dr[rfcEMD.Name] = row.GetString(rfcEMD.Name);
                }
                dtRet.Rows.Add(dr);
            }
            if (dtRet.Rows.Count == 0) { dtRet.Rows.Add("", "", "", "S", "", "", "", CustomerCode, "", "", "", "", ""); }

            return dtRet;
        }
        public DataTable ChangeCustomerInSAP(PDMS_Customer  Customer, Boolean IsShipTo)
        {
            List<string> Name = AddressSplit(Customer.CustomerName);
            Customer.CustomerName = Name[0];
           string CustomerName2 = Name[1];

            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSD_CUSTOMER_CHANGE_NEW");
            tagListBapi.SetValue("MODE", "N");
            tagListBapi.SetValue("P_CUSTOMER", Customer.CustomerCode);
            tagListBapi.SetValue("P_COMPANYCODE", (IsShipTo == false) ? "AF" : "");
            tagListBapi.SetValue("P_TITLE", Customer.Title.Title);
            tagListBapi.SetValue("P_NAME1", Customer.CustomerName);
            //tagListBapi.SetValue("P_NAME2", Customer.CustomerName2);
            tagListBapi.SetValue("P_NAME2", CustomerName2);
            tagListBapi.SetValue("P_NAME3", "");
            tagListBapi.SetValue("P_NAME4", "");
            tagListBapi.SetValue("P_STREET2", Customer.Address1);
            tagListBapi.SetValue("P_STREET3", Customer.Address2);
            tagListBapi.SetValue("P_STREET4", Customer.Address3/*Customer.Tehsil.Tehsil*/);
            tagListBapi.SetValue("P_HOUSE_NO", "");
            tagListBapi.SetValue("P_DISTRICT", Customer.District.District);
            tagListBapi.SetValue("P_POSTCODE", Customer.Pincode);
            tagListBapi.SetValue("P_CITY", string.IsNullOrEmpty(Customer.City)? Customer.District.District : Customer.City);
            tagListBapi.SetValue("P_COUNTRY", Customer.Country.CountryCode);
            tagListBapi.SetValue("P_REGION", Convert.ToString(Customer.State.StateCode));
            tagListBapi.SetValue("P_PHONE", Customer.Mobile);
            tagListBapi.SetValue("P_MOBILE", Customer.AlternativeMobile);
            tagListBapi.SetValue("P_EMAIL", Customer.Email);
            if (Customer.IsFinanceVerified)
            {
                tagListBapi.SetValue("P_GSTIN", Customer.GSTIN);
                if (IsShipTo == false)
                {
                    tagListBapi.SetValue("P_PANNO", Customer.PAN);
                }
            }
            tagListBapi.SetValue("P_CONTACT", Customer.ContactPerson);
            tagListBapi.SetValue("P_CONTACT2", Customer.ContactPerson);
            //tagListBapi.SetValue("P_DOC_NO", "");

            tagListBapi.Invoke(SAP.RfcDes());
            string SUBRC = tagListBapi.GetValue("P_SUBRC").ToString();

            IRfcTable table = tagListBapi.GetTable("IT_STATUS");

            DataTable dtRet = new DataTable();

            for (int Column = 0; Column < 13; Column++)
            {
                RfcElementMetadata rfcEMD = table.GetElementMetadata(Column);
                dtRet.Columns.Add(rfcEMD.Name);
            }

            foreach (IRfcStructure row in table)
            {
                DataRow dr = dtRet.NewRow();
                for (int Column = 0; Column < 13; Column++)
                {
                    RfcElementMetadata rfcEMD = table.GetElementMetadata(Column);
                    dr[rfcEMD.Name] = row.GetString(rfcEMD.Name);
                }
                dtRet.Rows.Add(dr);
            }
            if (dtRet.Rows.Count == 0) { dtRet.Rows.Add("", "", "", "S", "", "", "", SUBRC, "", "", "", "", ""); }

            return dtRet;
        }

        public List<string> AddressSplit(string Input)
        {
            List<string> OutPut = new List<string>();
            string[] SplitedInput = Input.Split(' ');

            string N1 = "", N2 = "";
            foreach (string Word in SplitedInput)
            {
                if (((N1 + Word).Length <= 40) && (string.IsNullOrEmpty(N2)))
                {
                    N1 = N1 + " " + Word;
                }
                else
                {
                    N2 = N2 + " " + Word;
                }
            }
            OutPut.Add(N1.Trim());
            OutPut.Add(N2.Trim());
            return OutPut;
        }
    }
}

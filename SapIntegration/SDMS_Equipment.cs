using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class SDMS_Equipment
    { 

        public List<PDMS_Equipment> getEquipmentFromSAP()
        {
            List<PDMS_Equipment> Equipments = new List<PDMS_Equipment>();
            PDMS_Equipment Equ = new PDMS_Equipment();

            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("Z_BAPI_READ_MACHINE_POP_LIST");
           // tagListBapi.SetValue("IV_LFDAT",Convert.ToDateTime("01.01.2010"));
            tagListBapi.SetValue("IV_COUNT", 300);
            tagListBapi.Invoke(SAP.RfcDes());

            IRfcTable TT_EUIP_DET = tagListBapi.GetTable("ET_MACHINE_LIST");
            for (int i = 0; i < TT_EUIP_DET.RowCount; i++)
            {
                try
                {
                    Equ = new PDMS_Equipment();
                    Equ.Ibase = new PDMS_EquipmentIbase();
                    TT_EUIP_DET.CurrentIndex = i;
                    Equ.Material = new PDMS_Material() { MaterialCode = TT_EUIP_DET.GetString("matnr") };
                    Equ.EquipmentSerialNo = TT_EUIP_DET.GetString("DEVICEID");
                    Equ.Ibase.DeliveryNo = TT_EUIP_DET.GetString("vbeln_vl");
                    Equ.Ibase.Item = Convert.ToInt32(TT_EUIP_DET.GetString("posnr"));
                    if(string.IsNullOrEmpty(TT_EUIP_DET.GetString("lfdat")) || TT_EUIP_DET.GetString("lfdat") == "0000-00-00")
                    {
                        continue;
                    }
                    Equ.Ibase.DeliveryDate = Convert.ToDateTime(TT_EUIP_DET.GetString("lfdat"));
                    Equ.Ibase.InstalledBaseNo = TT_EUIP_DET.GetString("ibase");
                    Equ.Ibase.ProductCode = TT_EUIP_DET.GetString("PRODUCT_ID");
                    Equ.Customer = new PDMS_Customer() { CustomerCode = TT_EUIP_DET.GetString("partner") };
                    string CRTIM = TT_EUIP_DET.GetString("CRTIM");

                    Equ.Ibase.IBaseCreatedOn = string.IsNullOrEmpty(CRTIM) ? (DateTime?)null : Convert.ToDateTime(CRTIM.Substring(0, 4) + "-" + CRTIM.Substring(4, 2) + "-" + CRTIM.Substring(6, 2) + " " + CRTIM.Substring(8, 2) + ":" + CRTIM.Substring(10, 2));
                    
                    Equ.Ibase.ShipToParty = new PDMS_Customer() { CustomerCode = TT_EUIP_DET.GetString("SH_PARTNER") };
                    Equ.Ibase.ShipToPartyDealer = new PDMS_Dealer() { DealerCode = TT_EUIP_DET.GetString("SH_DEALER") };
                    Equ.Ibase.IBaseLocation = TT_EUIP_DET.GetString("ADDRES");

                    Equ.Ibase.WarrantyStart = string.IsNullOrEmpty(TT_EUIP_DET.GetString("ST_DATE")) || TT_EUIP_DET.GetString("ST_DATE") == "0000-00-00" ? (DateTime?)null : Convert.ToDateTime(TT_EUIP_DET.GetString("ST_DATE"));
                    Equ.Ibase.WarrantyEnd = string.IsNullOrEmpty(TT_EUIP_DET.GetString("EN_DATE")) || TT_EUIP_DET.GetString("EN_DATE") == "0000-00-00" ? (DateTime?)null : Convert.ToDateTime(TT_EUIP_DET.GetString("EN_DATE"));
                    Equ.Ibase.MajorRegion = new PDMS_Region() { Region = TT_EUIP_DET.GetString("REGION") };
                    Equ.Ibase.SoleToDealer = new PDMS_Dealer() { DealerCode = TT_EUIP_DET.GetString("SD_DLR_CODE") };
                    Equ.EngineSerialNo = TT_EUIP_DET.GetString("V_ENG");
                    Equ.Ibase.FinancialYearOfDispatch = string.IsNullOrEmpty(TT_EUIP_DET.GetString("FIN_YEAR"))   ? (int?)null : Convert.ToInt32(TT_EUIP_DET.GetString("FIN_YEAR"));  

                    Equ.Ibase.Buyer1st = new PDMS_Customer() { CustomerCode = TT_EUIP_DET.GetString("BUYER_CODE1") };
                    Equ.Ibase.Buyer2nd = new PDMS_Customer() { CustomerCode = TT_EUIP_DET.GetString("BUYER_CODE2") };
                    //  Equ.Ibase.IBaseInactive = string.IsNullOrEmpty( TT_EUIP_DET.GetString("ZZINACTIVE");   
                    Equ.Ibase.UpdateOn = string.IsNullOrEmpty(TT_EUIP_DET.GetString("ZZCOM_DATE")) || TT_EUIP_DET.GetString("ZZCOM_DATE") == "0000-00-00" ? (DateTime?)null : Convert.ToDateTime(TT_EUIP_DET.GetString("ZZCOM_DATE"));  
                    Equipments.Add(Equ);
                }
                catch (Exception e1)
                {
                    string s = e1.Message;
                }
            }
            return Equipments;
         }
        public Boolean UpdateICTicketRequestedDateToSAP(List<string> DeliveryNos)
        {
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("Z_BAPI_UPDATE_MACHINE_POP_LIST");
            IRfcTable IT_MACHINE_LIST = tagListBapi.GetTable("IT_MACHINE_LIST");
            foreach (string S in DeliveryNos)
            {
                IT_MACHINE_LIST.Append();
                IT_MACHINE_LIST.SetValue("VBELN_VL", S); 
            }
            tagListBapi.Invoke(SAP.RfcDes());
            return true;
        }
    }
}

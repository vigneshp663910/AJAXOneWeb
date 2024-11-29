using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_EInvoice
    {
        public static DateTime EInvoiveDate
        {
            get
            {
                return Convert.ToDateTime(ConfigurationManager.AppSettings["EInvoiveDate"]);
            }
        }       
        public static decimal TcsTax
        {
            get
            {
                return Convert.ToDecimal(ConfigurationManager.AppSettings["TcsTax"]);
            }
        }    
    }
    
    [Serializable]
    public class PDMS_EInvoiceSigned
    {     
        public long RefInvoiceID { get; set; }
        public string IRN { get; set; }
        public string SignedQRCode { get; set; }
        public string SignedInvoice { get; set; }
        public string Comments { get; set; }
    }

    [Serializable]
    public class PEInvoiceGrid
    { 
        public PEInvoice EInvoice { get; set; }
        public string InvType { get; set; } 
    }

    [Serializable]
    public class PEInvoice
    {
        public string Version { get { return "1.1"; } }
        public PTranDtls TranDtls { get; set; }
        public PDocDtls DocDtls { get; set; }
        public PSellerDtls SellerDtls { get; set; }
        public PBuyerDtls BuyerDtls { get; set; }
        public PDispDtls DispDtls { get; set; }
        public PShipDtls ShipDtls { get; set; }
        public List<PItemList> ItemList { get; set; }
        public PValDtls ValDtls { get; set; }
        public PPayDtls PayDtls { get; set; }
        public PRefDtls RefDtls { get; set; }
        public List<PAddlDocDtls> AddlDocDtls { get; set; }
        public PExpDtls ExpDtls { get; set; }
        public PEwbDtls EwbDtls { get; set; }
    }
    public class PTranDtls
    {
        public string TaxSch { get { return "GST"; } }
        public string SupTyp { get { return "B2B"; } }
        public string RegRev { get { return "N"; } }
        public string EcmGstin { get; set; }
        public string IgstOnIntra { get; set; }
        // "TranDtls": {
        //"TaxSch": "GST",
        //"SupTyp": "B2B",
        //"RegRev": "Y",
        //"EcmGstin": null,
        //"IgstOnIntra": "N"
        //}
    }
    public class PDocDtls
    {
        public string Typ { get; set; }
        public string No { get; set; }
        public string Dt { get; set; }
        //     "DocDtls": {
        //    "Typ": "INV",
        //    "No": "DOC/001",
        //    "Dt": "18/08/2020"
        //},
    }
    public class PSellerDtls
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public string TrdNm { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public string Pin { get; set; }
        public string Stcd { get; set; }
        public string Ph { get; set; }
        public string Em { get; set; }
        //    "SellerDtls": {
        //    "Gstin": "37ARZPT4384Q1MT",
        //    "LglNm": "NIC company pvt ltd",
        //    "TrdNm": "NIC Industries",
        //    "Addr1": "5th block, kuvempu layout",
        //    "Addr2": "kuvempu layout",
        //    "Loc": "GANDHINAGAR",
        //    "Pin": 518001,
        //    "Stcd": "37",
        //    "Ph": "9000000000",
        //    "Em": "abc@gmail.com"
        //},
    }
    public class PBuyerDtls
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public string TrdNm { get; set; }
        public string Pos { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public string Pin { get; set; }
        public string Stcd { get; set; }
        public string Ph { get; set; }
        public string Em { get; set; }
        //   "BuyerDtls": {
        //    "Gstin": "29AWGPV7107B1Z1",
        //    "LglNm": "XYZ company pvt ltd",
        //    "TrdNm": "XYZ Industries",
        //    "Pos": "12",
        //    "Addr1": "7th block, kuvempu layout",
        //    "Addr2": "kuvempu layout",
        //    "Loc": "GANDHINAGAR",
        //    "Pin": 562160,
        //    "Stcd": "29",
        //    "Ph": "91111111111",
        //    "Em": "xyz@yahoo.com"
        //},
    }
    public class PDispDtls
    {
        public string Nm { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public string Pin { get; set; }
        public string Stcd { get; set; }
        //     "DispDtls": {
        //    "Nm": "ABC company pvt ltd",
        //    "Addr1": "7th block, kuvempu layout",
        //    "Addr2": "kuvempu layout",
        //    "Loc": "Banagalore",
        //    "Pin": 562160,
        //    "Stcd": "29"
        //},
    }
    public class PShipDtls
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public string TrdNm { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public string Pin { get; set; }
        public string Stcd { get; set; }
        //"ShipDtls": {
        // "Gstin": "29AWGPV7107B1Z1",
        // "LglNm": "CBE company pvt ltd",
        // "TrdNm": "kuvempu layout",
        // "Addr1": "7th block, kuvempu layout",
        // "Addr2": "kuvempu layout",
        // "Loc": "Banagalore",
        // "Pin": 562160,
        // "Stcd": "29"
        //},
    }
    public class PItemList
    {
        public string SlNo { get; set; }
        public string PrdDesc { get; set; }
        public string IsServc { get; set; }
        public string HsnCd { get; set; }
        public string Barcde { get; set; }
        public string Qty { get; set; }
        public string FreeQty { get; set; }
        public string Unit { get; set; }
        public string UnitPrice { get; set; }
        public string TotAmt { get; set; }
        public string Discount { get; set; }
        public string PreTaxVal { get; set; }
        public string AssAmt { get; set; }
        public string GstRt { get; set; }
        public string IgstAmt { get; set; }
        public string CgstAmt { get; set; }
        public string SgstAmt { get; set; }
        public string CesRt { get; set; }
        public string CesAmt { get; set; }
        public string CesNonAdvlAmt { get; set; }
        public string StateCesRt { get; set; }
        public string StateCesAmt { get; set; }
        public string StateCesNonAdvlAmt { get; set; }
        public string OthChrg { get; set; }
        public string TotItemVal { get; set; }
        public string OrdLineRef { get; set; }
        public string OrgCntry { get; set; }
        public string PrdSlNo { get; set; }
        public PBchDtls BchDtls { get; set; }
        public List<PAttribDtls> AttribDtls { get; set; }
        //    "ItemList": [{
        //    "SlNo": "1",
        //    "PrdDesc": "Rice",
        //    "IsServc": "N",
        //    "HsnCd": "1001",
        //    "Barcde": "123456",
        //    "Qty": 100.345,
        //    "FreeQty": 10,
        //    "Unit": "BAG",
        //    "UnitPrice": 99.545,
        //    "TotAmt": 9988.84,
        //    "Discount": 10,
        //    "PreTaxVal": 1,
        //    "AssAmt": 9978.84,
        //    "GstRt": 12.0,
        //    "IgstAmt": 1197.46,
        //    "CgstAmt": 0,
        //    "SgstAmt": 0,
        //    "CesRt": 5,
        //    "CesAmt": 498.94,
        //    "CesNonAdvlAmt": 10,
        //    "StateCesRt": 12,
        //    "StateCesAmt": 1197.46,
        //    "StateCesNonAdvlAmt": 5,
        //    "OthChrg": 10,
        //    "TotItemVal": 12897.7,
        //    "OrdLineRef": "3256",
        //    "OrgCntry": "AG",
        //    "PrdSlNo": "12345",
        //        "BchDtls": {
        //            "Nm": "123456",
        //            "ExpDt": "01/08/2020",
        //            "WrDt": "01/09/2020"
        //        },
        //    "AttribDtls": [{
        //        "Nm": "Rice",
        //        "Val": "10000"
        //    }]
        //}],
    }
    public class PBchDtls
    {
        public string Nm { get; set; }
        public string ExpDt { get; set; }
        public string WrDt { get; set; }
        // "BchDtls": {
        //            "Nm": "123456",
        //            "ExpDt": "01/08/2020",
        //            "WrDt": "01/09/2020"
        //        },
    }
    public class PAttribDtls
    {
        public string Nm { get; set; }
        public string Val { get; set; }
        // "AttribDtls": [{
        //        "Nm": "Rice",
        //        "Val": "10000"
        //    }]
    }
    public class PValDtls
    {
        public string AssVal { get; set; }
        public string CgstVal { get; set; }
        public string SgstVal { get; set; }
        public string IgstVal { get; set; }
        public string CesVal { get; set; }
        public string StCesVal { get; set; }
        public string Discount { get; set; }
        public string OthChrg { get; set; }
        public string RndOffAmt { get; set; }
        public string TotInvVal { get; set; }
        public string TotInvValFc { get; set; }
        //   "ValDtls": {
        //    "AssVal": 9978.84,
        //    "CgstVal": 0,
        //    "SgstVal": 0,
        //    "IgstVal": 1197.46,
        //    "CesVal": 508.94,
        //    "StCesVal": 1202.46,
        //    "Discount": 10,
        //    "OthChrg": 20,
        //    "RndOffAmt": 0.3,
        //    "TotInvVal": 12908,
        //    "TotInvValFc": 12897.7
        //},
    }
    public class PPayDtls
    {
        public string Nm { get; set; }
        public string AccDet { get; set; }
        public string Mode { get; set; }
        public string FinInsBr { get; set; }
        public string PayTerm { get; set; }
        public string PayInstr { get; set; }
        public string CrTrn { get; set; }
        public string DirDr { get; set; }
        public string CrDay { get; set; }
        public string PaidAmt { get; set; }
        public string PaymtDue { get; set; }

        //     "PayDtls": {
        //    "Nm": "ABCDE",
        //    "AccDet": "5697389713210",
        //    "Mode": "Cash",
        //    "FinInsBr": "SBIN11000",
        //    "PayTerm": "100",
        //    "PayInstr": "Gift",
        //    "CrTrn": "test",
        //    "DirDr": "test",
        //    "CrDay": 100,
        //    "PaidAmt": 10000,
        //    "PaymtDue": 5000
        //},
    }
    public class PRefDtls
    {
        public string InvRm { get; set; }
        public PDocPerdDtls DocPerdDtls { get; set; }
        public List<PPrecDocDtls> PrecDocDtls { get; set; }
        public List<PContrDtls> ContrDtls { get; set; }

        // "RefDtls": {
        //    "InvRm": "TEST",
        //    "DocPerdDtls": {
        //    "InvStDt": "01/08/2020",
        //    "InvEndDt": "01/09/2020"
        //},
    }
    public class PDocPerdDtls
    {
        public string InvStDt { get; set; }
        public string InvEndDt { get; set; }


        //    "DocPerdDtls": {
        //    "InvStDt": "01/08/2020",
        //    "InvEndDt": "01/09/2020"
        //},
    }
    public class PPrecDocDtls
    {
        public string InvNo { get; set; }
        public string InvDt { get; set; }
        public string OthRefNo { get; set; }

        //"PrecDocDtls": [{
        //     "InvNo": "DOC/002",
        //     "InvDt": "01/08/2020",
        //     "OthRefNo": "123456"
        // }],
    }
    public class PContrDtls
    {
        public string RecAdvRefr { get; set; }
        public string RecAdvDt { get; set; }
        public string TendRefr { get; set; }
        public string ContrRefr { get; set; }
        public string ExtRefr { get; set; }
        public string ProjRefr { get; set; }
        public string PORefr { get; set; }
        public string PORefDt { get; set; }


        // "ContrDtls": [{
        //    "RecAdvRefr": "Doc/003",
        //    "RecAdvDt": "01/08/2020",
        //    "TendRefr": "Abc001",
        //    "ContrRefr": "Co123",
        //    "ExtRefr": "Yo456",
        //    "ProjRefr": "Doc-456",
        //    "PORefr": "Doc-789",
        //    "PORefDt": "01/08/2020"
        //}]
    }

    public class PAddlDocDtls
    {
        public string Url { get; set; }
        public string Docs { get; set; }
        public string Info { get; set; }

        //     "AddlDocDtls": [{
        //    "Url": "https://einv-apisandbox.nic.in",
        //    "Docs": "Test Doc",
        //    "Info": "Document Test"
        //}],
    }
    public class PExpDtls
    { 
        public string ShipBNo { get; set; }
        public string ShipBDt { get; set; }
        public string Port { get; set; }
        public string RefClm { get; set; }
        public string ForCur { get; set; }
        public string CntCode { get; set; }
        public string ExpDuty { get; set; }


        //"ExpDtls": {
        //    "ShipBNo": "A-248",
        //    "ShipBDt": "01/08/2020",
        //    "Port": "INABG1",
        //    "RefClm": "N",
        //    "ForCur": "AED",
        //    "CntCode": "AE",
        //    "ExpDuty": null
        //},
    }
    public class PEwbDtls
    {
        public string TransId { get; set; }
        public string TransName { get; set; }
        public string Distance { get; set; }
        public string TransDocNo { get; set; }
        public string TransDocDt { get; set; }
        public string VehNo { get; set; }
        public string VehType { get; set; }
        public string TransMode { get; set; }

        //"EwbDtls": {
        //    "TransId": "12AWGPV7107B1Z1",
        //    "TransName": "XYZ EXPORTS",
        //    "Distance": 100,
        //    "TransDocNo": "DOC01",
        //    "TransDocDt": "18/08/2020",
        //    "VehNo": "ka123456",
        //    "VehType": "R",
        //    "TransMode": "1"
        //}
    }
}

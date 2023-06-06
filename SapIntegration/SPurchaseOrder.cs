using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class SPurchaseOrder
    {
        public List<PAsn> getPurchaseOrderAsnDetails(string InvoiceNo)
        {
            List<PAsn> AsnList = new List<PAsn>();
            PAsn Asn = null;
            PAsnItem Item;

            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSD_ASN_DETAILS");
            tagListBapi.SetValue("P_INV", InvoiceNo);
            tagListBapi.Invoke(SAP.RfcDes());
            IRfcTable IT_DET = tagListBapi.GetTable("IT_DET");

            string SONumber = "";            
            for (int i = 0; i < IT_DET.RowCount; i++)
            {
                if (SONumber != IT_DET.CurrentRow.GetString("VGBEL").Trim())
                {
                    Asn = new PAsn();
                    Asn.AsnNumber = "";
                    Asn.AsnDate = DateTime.Now;
                    Asn.PurchaseOrder = new PPurchaseOrder()
                    {
                        PurchaseOrderNumber = "",
                    };
                    Asn.SoNumber = IT_DET.CurrentRow.GetString("VGBEL").Trim();
                    SONumber = IT_DET.CurrentRow.GetString("VGBEL").Trim();
                    Asn.SoDate = DateTime.Now;
                    Asn.DeliveryNumber = IT_DET.CurrentRow.GetString("VBELN").Trim();
                    Asn.DeliveryDate = Convert.ToDateTime(IT_DET.CurrentRow.GetString("LFDAT").Trim());
                    Asn.NetWeight = Convert.ToDecimal(IT_DET.CurrentRow.GetString("NTGEW").Trim());
                    Asn.CourierID = "";
                    Asn.CourierDate = null;
                    Asn.Remarks = "";
                    Asn.LRNo = IT_DET.CurrentRow.GetString("LR_NO").Trim();
                    Asn.TrackID = "";
                    Asn.AsnItemS = new List<PAsnItem>();
                }
                if (!Asn.AsnItemS.Exists(s => s.Qty == Convert.ToDecimal(IT_DET.CurrentRow.GetString("LFIMG").Trim()) && s.AsnItem == Convert.ToInt32(IT_DET.CurrentRow.GetString("POSNR")) && s.Material.MaterialCode == IT_DET.CurrentRow.GetString("MATNR")))
                {
                    Item = new PAsnItem();
                    Item.Qty = Convert.ToDecimal(IT_DET.CurrentRow.GetString("LFIMG").Trim());
                    Item.Material = new PDMS_Material()
                    {
                        MaterialCode = IT_DET.CurrentRow.GetString("MATNR"),
                    };
                    Item.AsnItem = Convert.ToInt32(IT_DET.CurrentRow.GetString("POSNR"));
                    Item.NetWeight = Convert.ToDecimal(IT_DET.CurrentRow.GetString("NTGEW").Trim());
                    Item.UomWeight = IT_DET.CurrentRow.GetString("GEWEI");
                    Item.PackCount = 0;
                    Item.UomPackCount = IT_DET.CurrentRow.GetString("VRKME");
                    Item.StockType = "";
                    Item.Remarks = "";
                    Item.IsChangedpart = true;

                    Asn.AsnItemS.Add(Item);
                }
                if (!AsnList.Exists(s => s.SoNumber == SONumber))
                    AsnList.Add(Asn);

            }

            return AsnList;
        }
    }
}

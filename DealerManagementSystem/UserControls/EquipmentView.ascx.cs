using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class EquipmentView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillEquipment();
            FillICTicket();
        }
        void FillEquipment()
        {
            string EquipmentSerialNo = (string)Session["SerEquipmentSerialNo"];
            List<PDMS_EquipmentHeader> Equ = new BDMS_Equipment().GetEquipment(null, EquipmentSerialNo);
            if (Equ.Count == 1)
            {
                gvEquipment.DataSource = Equ;
                gvEquipment.DataBind();
            }
        }
        void FillICTicket()
        {
            string EquipmentSerialNo = (string)Session["SerEquipmentSerialNo"];
            List<PDMS_ICTicket> Equ = new BDMS_ICTicket().GetICTicketByEquipmentSerialNo(EquipmentSerialNo);
            gvICTicket.DataSource = Equ;
            gvICTicket.DataBind();
        }
        public void FillServiceCharges(PDMS_ICTicket Ticket, GridView gv)
        {

            List<PDMS_ServiceCharge> Charge = new BDMS_Service().GetServiceCharges(Ticket.ICTicketID, null, "", false);
            string ClaimNumber = "";
            if (Charge.Count == 0)
            {
                PDMS_ServiceCharge c = new PDMS_ServiceCharge();
                Charge.Add(c);
            }
            else
            {
                ClaimNumber = Charge[0].ClaimNumber;
            }

            gv.DataSource = Charge;
            gv.DataBind();
            //if ((Ticket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid) || (Ticket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others))
            //{
            //    gv.Columns[8].Visible = false; 
            //}
            //else
            //{
            //    gv.Columns[9].Visible = false;
            //    gv.Columns[10].Visible = false;
            //    gv.Columns[11].Visible = false; 
            //}
        }

        protected void gvICTicket_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    long ICTicketID = Convert.ToInt64(gvICTicket.DataKeys[e.Row.RowIndex].Value);
                    PDMS_ICTicket Ticket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);
                    GridView gvServiceCharges = (GridView)e.Row.FindControl("gvServiceCharges");
                    FillServiceCharges(Ticket, gvServiceCharges);

                    GridView gvMaterial = (GridView)e.Row.FindControl("gvMaterial");
                    List<PDMS_ServiceMaterial> Material = new BDMS_Service().GetServiceMaterials(ICTicketID, null, null, "", false, "");
                    gvMaterial.DataSource = Material;
                    gvMaterial.DataBind();

                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
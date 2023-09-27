using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.Dashboard
{
    [ToolboxData("<{0}:DMS_ICTicketEscalationOnBreakdownLevel1 runat=server></{0}:DMS_ICTicketEscalationOnBreakdownLevel1>")]
    public partial class ICTicketEscalationOnBreakdownLevel : System.Web.UI.UserControl
    {
        public Int32 DashboardControlID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            int? DealerID = (int?)Session["SerDealerID"];
            DateTime? DateFrom = (DateTime?)Session["SerDateFrom"];
            DateTime? DateTo = (DateTime?)Session["SerDateTo"];

            List<PDMS_MTTR_New> MTTR = null;
            if (DashboardControlID == 1)
            {
                ucTitle.Text = "Escalation report on Break down More than 8 Hrs";
                MTTR = new BDMS_MTTR().GetMTTRTeamLeader(DealerID, DateFrom, DateTo, PSession.User.UserID);
            }
            else if (DashboardControlID == 2)
            {
                ucTitle.Text = "Escalation report on Break down more than 24 Hrs";
                MTTR = new BDMS_MTTR().GetMTTRServiceManagers(DealerID, DateFrom, DateTo, PSession.User.UserID);
            }
            else if (DashboardControlID == 3)
            {
                ucTitle.Text = "Escalation report on Break down More than 48 Hrs";
                MTTR = new BDMS_MTTR().GetMTTRReginalServiceManagers(DealerID, DateFrom, DateTo, PSession.User.UserID);
            }
            else
            {
                ucTitle.Text = "Escalation report on Break down More than 72 Hrs";
                MTTR = new BDMS_MTTR().GetMTTRDM(DealerID, DateFrom, DateTo, PSession.User.UserID);
            }
            //var MTTR1 = (from S in MTTR
            //             join D in PSession.User.Dealer on S.ICTicket.Dealer.DealerCode equals D.UserName
            //             select new
            //             {
            //                 S
            //             }).ToList();
            //MTTR.Clear();
            //foreach (var w in MTTR1)
            //{
            //    MTTR.Add(w.S);
            //}
            ucTitle.Text = ucTitle.Text + " - " + MTTR.Count();
            gvMaterialAnalysis.DataSource = MTTR;
            gvMaterialAnalysis.DataBind();
        }



        protected void lbExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IC Ticket Number");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Status");
            dt.Columns.Add("Customer");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Dealer");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Model");
            dt.Columns.Add("Machine Serial Number");
            for (int i = 0; i < gvMaterialAnalysis.Rows.Count; i++)
            {
                Label lblICTicketNumber = (Label)gvMaterialAnalysis.Rows[i].FindControl("lblICTicketNumber");
                Label lblICTicketDate = (Label)gvMaterialAnalysis.Rows[i].FindControl("lblICTicketDate");
                Label lblServiceStatus = (Label)gvMaterialAnalysis.Rows[i].FindControl("lblServiceStatus");
                Label lblCustomerCode = (Label)gvMaterialAnalysis.Rows[i].FindControl("lblCustomerCode");
                Label lblCustomerName = (Label)gvMaterialAnalysis.Rows[i].FindControl("lblCustomerName");
                Label lblDealerCode = (Label)gvMaterialAnalysis.Rows[i].FindControl("lblDealerCode");
                Label lblDealerName = (Label)gvMaterialAnalysis.Rows[i].FindControl("lblDealerName");
                Label lblModel = (Label)gvMaterialAnalysis.Rows[i].FindControl("lblModel");
                Label lblEquipmentSerialNo = (Label)gvMaterialAnalysis.Rows[i].FindControl("lblEquipmentSerialNo");

                dt.Rows.Add(
                        lblICTicketNumber.Text
                      , lblICTicketDate.Text
                      , lblServiceStatus.Text
                      , lblCustomerCode.Text
                      , lblCustomerName.Text
                      , lblDealerCode.Text
                      , lblDealerName.Text
                      , lblModel.Text
                      , lblEquipmentSerialNo.Text
                      );
            }
            new BXcel().ExporttoExcel(dt, ucTitle.Text);
        }
    }
}
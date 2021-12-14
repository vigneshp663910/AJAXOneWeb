using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.Dashboard
{
    public partial class ICTicketTransactionStatics : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillPieChartForCategory();
        }
        private void FillPieChartForCategory()
        {
            int? DealerID = (int?)Session["SerDealerID"];
            DateTime? DateFrom = (DateTime?)Session["SerDateFrom"];
            DateTime? DateTo = (DateTime?)Session["SerDateTo"];
            List<PDMS_ICTicket> Ticket = new BDMS_Dashboard().GetDashboardTransactionStatistics(DealerID, DateFrom, DateTo, PSession.User.UserID);
            //var TicketChart = Ticket.GroupBy(info => info.ServiceStatus.ServiceStatus)
            //            .Select(group => new
            //            {
            //                Metric = group.Key,
            //                Count = group.Count()
            //            }).ToList();

            string[] x = new string[Ticket.Count];
            int[] y = new int[Ticket.Count];
            for (int i = 0; i < Ticket.Count; i++)
            {
                x[i] = Ticket[i].ServiceStatus.ServiceStatus;
                y[i] = Ticket[i].ServiceStatus.ServiceStatusID;
                switch (Ticket[i].ServiceStatus.ServiceStatus)
                {
                    case "Open": x[i] = "Open"; break;
                    case "Technician Assigned": x[i] = "Technician Assigned"; break;
                    case "SE Reached": x[i] = "SE Reached"; break;
                    case "Restored": x[i] = "Restored"; break;
                    case "Req. Declined": x[i] = "Req. Declined"; break;
                    case "Declined": x[i] = "Declined"; break;
                    case "Reopen": x[i] = "Reopen"; break;
                }
            }
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].BorderWidth = 10;
            Chart1.Series[0].ChartType = SeriesChartType.Pie;
            foreach (Series charts in Chart1.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    switch (point.AxisLabel)
                    {
                        case "Open": point.Color = Color.Red; break;
                        case "Technician Assigned": point.Color = Color.SandyBrown; break;
                        case "SE Reached": point.Color = Color.YellowGreen; break;
                        case "Restored": point.Color = Color.Green; break;
                        case "Req. Declined": point.Color = Color.BlueViolet; break;
                        case "Declined": point.Color = Color.RoyalBlue; break;
                        case "Reopen": point.Color = Color.Yellow; break;
                    }
                    point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);
                }
            }

        }
    }
}
using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
 
using System.Data;
using System.IO; 
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Pre_Sales_Dashboard : System.Web.UI.Page
    {
        DateTime? From = null;
       // DateTime? To = DateTime.Now.Date;
        DateTime? FromF = null;
       // DateTime? ToF = DateTime.Now.Date;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dashboard  » Pre-Sales');</script>");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                From = DateTime.Now.Date;
                FromF = DateTime.Now.Date.AddDays(-7);
                new DDLBind().FillDealerAndEngneer(ddlDealer, ddlDealerEmployee);
                FillStatusCount(); 
            }
        }
        public string funnelData()
        {
            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            row = new Dictionary<string, object>();
            row.Add("value", "5000");
            row.Add("color", "#ED5565");
            row.Add("label", "Newly Created: 14"); 
            rows.Add(row);

            row = new Dictionary<string, object>();
            row.Add("value", "4000");
            row.Add("color", "#FC6D58");
            row.Add("label", "Convert To Quotation: 7");
            rows.Add(row);

            row = new Dictionary<string, object>();
            row.Add("value", "3000");
            row.Add("color", "#46CFB0");
            row.Add("label", "Won: 0");
            rows.Add(row);

            row = new Dictionary<string, object>();
            row.Add("value", "2000");
            row.Add("color", "#9FD477");
            row.Add("label", "Value 4");
            rows.Add(row);

             

            return JsonConvert.SerializeObject(rows);   

            // return CurrentLocation;

        }

        void FillStatusCount()
        {
            lblOpen.Text = "0";
            lblAssigned.Text = "0";
            lblQuotation.Text = "0";
            lblWon.Text = "0";
            lblLost.Text = "0";
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? EngineerUserID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            DateTime TodayDate = DateTime.Now.Date;
            if (rbToday.Checked)
            {
                From = TodayDate;
            }
            else if (rbWeek.Checked)
            {
                From = TodayDate.AddDays(-7);
            }
            else if (rbMonth.Checked)
            {
                From = TodayDate.AddMonths(-1);
            }
            else if (rbYear.Checked)
            {
                From = TodayDate.AddYears(-1);
            }

            List<PLeadStatus> Status = new BLead().GetLeadCountByStatus(From, null, DealerID, EngineerUserID);
           
            if (Status != null)
            {
                if ((Status.Where(m => m.StatusID == 1).Count() != 0))
                {
                    var ss = Status.Where(m => m.StatusID == 1).ToList();
                    lblOpen.Text = ss[0].Count.ToString();
                }

                if ((Status.Where(m => m.StatusID == 2).Count() != 0))
                {
                    var ss = Status.Where(m => m.StatusID == 2).ToList();
                    lblAssigned.Text = ss[0].Count.ToString();
                }
                if ((Status.Where(m => m.StatusID == 3).Count() != 0))
                {
                    var ss = Status.Where(m => m.StatusID == 3).ToList();
                    lblQuotation.Text = ss[0].Count.ToString();
                }
                if ((Status.Where(m => m.StatusID == 4).Count() != 0))
                {
                    var ss = Status.Where(m => m.StatusID == 4).ToList();
                    lblWon.Text = ss[0].Count.ToString();
                }
                if ((Status.Where(m => m.StatusID == 5).Count() != 0))
                {
                    var ss = Status.Where(m => m.StatusID == 5).ToList();
                    lblLost.Text = ss[0].Count.ToString();
                }
                if ((Status.Where(m => m.StatusID == 6).Count() != 0))
                {
                    var ss = Status.Where(m => m.StatusID == 6).ToList();
                    lblCancelled.Text = ss[0].Count.ToString();
                }
            }


            lblEnquiryOpen.Text = "0";
            lblEnquiryInProgress.Text = "0";
            lblEnquiryConvertedToLead.Text = "0";
            lblEnquiryRejected.Text = "0";
            int TotalEnq = 0;
            List <PPreSaleStatus> StatusEnq = new BEnquiry().GetEnquiryCountByStatus(From, null, DealerID, EngineerUserID);
            if (StatusEnq != null)
            {
                if ((StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.Unattended).Count() != 0))
                {
                    var ss = StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.Unattended).ToList();
                    lblEnquiryOpen.Text = ss[0].Count.ToString();
                    TotalEnq = ss[0].Count;
                }
                if ((StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.InProgress).Count() != 0))
                {
                    var ss = StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.InProgress).ToList();
                    lblEnquiryInProgress.Text = ss[0].Count.ToString();
                    TotalEnq = TotalEnq + ss[0].Count;
                }
                if ((StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.ConvertedToLead).Count() != 0))
                {
                    var ss = StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.ConvertedToLead).ToList();
                    lblEnquiryConvertedToLead.Text = ss[0].Count.ToString();
                    TotalEnq = TotalEnq+ss[0].Count;
                }
                if ((StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.Rejected).Count() != 0))
                {
                    var ss = StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.Rejected).ToList();
                    lblEnquiryRejected.Text = ss[0].Count.ToString();
                    TotalEnq = TotalEnq + ss[0].Count;
                }
            }

            List<PLeadStatus> StatusFunnel = new BLead().GetLeadCountByStatus(From, null, DealerID, EngineerUserID);

            int AssignedFunnel = 0, QuotationFunnel = 0, WonFunnel = 0, LostFunnel = 0, CancelFunnel = 0;
            if (StatusFunnel != null)
            {
                if ((StatusFunnel.Where(m => m.StatusID == 1).Count() != 0))
                {
                    var ss = StatusFunnel.Where(m => m.StatusID == 1).ToList();
                    AssignedFunnel = ss[0].Count;
                }
                if ((StatusFunnel.Where(m => m.StatusID == 2).Count() != 0))
                {
                    var ss = StatusFunnel.Where(m => m.StatusID == 2).ToList();
                    AssignedFunnel = AssignedFunnel + ss[0].Count;
                }

                if ((StatusFunnel.Where(m => m.StatusID == 3).Count() != 0))
                {
                    var ss = StatusFunnel.Where(m => m.StatusID == 3).ToList();
                    QuotationFunnel = ss[0].Count;
                }
                if ((StatusFunnel.Where(m => m.StatusID == 4).Count() != 0))
                {
                    var ss = StatusFunnel.Where(m => m.StatusID == 4).ToList();
                    WonFunnel = ss[0].Count;
                }
                if ((StatusFunnel.Where(m => m.StatusID == 5).Count() != 0))
                {
                    var ss = StatusFunnel.Where(m => m.StatusID == 5).ToList();
                    LostFunnel = ss[0].Count;
                }
                if ((StatusFunnel.Where(m => m.StatusID == 6).Count() != 0))
                {
                    var ss = StatusFunnel.Where(m => m.StatusID == 6).ToList();
                    CancelFunnel = ss[0].Count;
                }
            }

            lblNewlyCreatedF.InnerText = "Enquiry Created : " + TotalEnq.ToString();
            lblConvertToProspectF.InnerText = "Enquiry To Lead : " + (AssignedFunnel + QuotationFunnel + WonFunnel + LostFunnel + CancelFunnel).ToString();
            lblQuotationF.InnerText = (QuotationFunnel + WonFunnel).ToString();
            lblWonF.InnerText = "Won: " + WonFunnel.ToString();
        }
         
        protected void rbStatus_CheckedChanged(object sender, EventArgs e)
        {
           FillStatusCount();
            
        }


        //protected void rbStatusF_CheckedChanged(object sender, EventArgs e)
        //{
        //    FillFunnel();
        //}

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Unattended")
            {
                Session["leadStatusID"] = (short)LeadStatus.Unattended;
            }
            else if (lbActions.Text == "In Progress")
            {
                Session["leadStatusID"] = (short)LeadStatus.InProgress;
            }
            else if (lbActions.Text == "Quotation")
            {
                Session["leadStatusID"] = (short)LeadStatus.Quotation;
            }
            else if (lbActions.Text == "Won")
            {
                Session["leadStatusID"] = (short)LeadStatus.Won;
            }
            else if (lbActions.Text == "Sales Lost")
            {
                Session["leadStatusID"] = (short)LeadStatus.SalesLost;
            }
            else if (lbActions.Text == "Dropped")
            {
                Session["leadStatusID"] = (short)LeadStatus.Dropped;
            }


            DateTime TodayDate = DateTime.Now.Date;

            if (rbToday.Checked)
            {
                Session["leadDateFrom"] = TodayDate; 
            }
            else if (rbWeek.Checked)
            {
                Session["leadDateFrom"] = TodayDate.AddDays(-7);
            }
            else if (rbMonth.Checked)
            {
                Session["leadDateFrom"] = TodayDate.AddMonths(-1);
            }
            else
            {
                Session["leadDateFrom"] = TodayDate.AddYears(-1);
            }

            Session["leadDealerID"] = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue); 
            Session["EngineerUserID"] = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);

            Response.Redirect("leadN.aspx");
        }

        protected void lbEnquiryActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Unattended")
            {
                Session["leadStatusID"] = (short)PreSaleStatus.Unattended;
            }
            else if (lbActions.Text == "In Progress")
            {
                Session["leadStatusID"] = (short)PreSaleStatus.InProgress;
            }
            else if (lbActions.Text == "Converted To Lead")
            {
                Session["leadStatusID"] = (short)PreSaleStatus.ConvertedToLead;
            }
            else if (lbActions.Text == "Rejected")
            {
                Session["leadStatusID"] = (short)PreSaleStatus.Rejected;
            }

            DateTime TodayDate = DateTime.Now.Date;

            if (rbToday.Checked)
            {
                Session["leadDateFrom"] = TodayDate;
            }
            else if (rbWeek.Checked)
            {
                Session["leadDateFrom"] = TodayDate.AddDays(-7);
            }
            else if (rbMonth.Checked)
            {
                Session["leadDateFrom"] = TodayDate.AddMonths(-1);
            }
            else
            {
                Session["leadDateFrom"] = TodayDate.AddYears(-1);
            }

            Session["leadDealerID"] = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            Session["EngineerUserID"] = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            Response.Redirect("EnquiryN.aspx");
        }
        
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlDealerEmployee, DealerUser, "ContactName", "UserID");
            FillStatusCount();
        }

        protected void ddlDealerEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStatusCount();
        }
    }
}
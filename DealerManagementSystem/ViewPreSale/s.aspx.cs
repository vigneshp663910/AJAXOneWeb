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
        DateTime? To = DateTime.Now;
        DateTime? FromF = null;
        DateTime? ToF = DateTime.Now;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales Dashboard');</script>");
                // lblConvertToProspect.InnerText = "Convert To  Prospect: 25000";
                From = DateTime.Now;
                FromF = DateTime.Now.AddDays(-7);
                new DDLBind().FillDealerAndEngneer(ddlDealer, ddlDealerEmployee);
                FillStatusCount();
                //FillFunnel();
                //FillEnquiryStatusCount();
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
            if (rbToday.Checked)
            {
                From = DateTime.Now;
            }
            else if (rbWeek.Checked)
            {
                From = DateTime.Now.AddDays(-7);
            }
            else if (rbMonth.Checked)
            {
                From = DateTime.Now.AddMonths(-1);
            }
            else if (rbYear.Checked)
            {
                From = DateTime.Now.AddYears(-1);
            }

            List<PLeadStatus> Status = new BLead().GetLeadCountByStatus(From, To, DealerID, EngineerUserID);
           
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
            lblEnquiryConvertedToLead.Text = "0";
            lblEnquiryRejected.Text = "0";
            List <PPreSaleStatus> StatusEnq = new BEnquiry().GetEnquiryCountByStatus(From, To, DealerID, EngineerUserID);
            if (StatusEnq != null)
            {
                if ((StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.Open).Count() != 0))
                {
                    var ss = StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.Open).ToList();
                    lblEnquiryOpen.Text = ss[0].Count.ToString();
                }

                if ((StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.ConvertedToLead).Count() != 0))
                {
                    var ss = StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.ConvertedToLead).ToList();
                    lblEnquiryConvertedToLead.Text = ss[0].Count.ToString();
                }
                if ((StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.Rejected).Count() != 0))
                {
                    var ss = StatusEnq.Where(m => m.StatusID == (short)PreSaleStatus.Rejected).ToList();
                    lblEnquiryRejected.Text = ss[0].Count.ToString();
                }
            }

            List<PLeadStatus> StatusFunnel = new BLead().GetLeadCountByStatus(From, To, DealerID, EngineerUserID);

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
            lblNewlyCreatedF.InnerText = "Newly Created: " + (AssignedFunnel + QuotationFunnel + WonFunnel + LostFunnel + CancelFunnel).ToString();
            lblConvertToProspectF.InnerText = (QuotationFunnel + WonFunnel).ToString();
            lblWonF.InnerText = "Won: " + WonFunnel.ToString();
        }


        //void FillFunnel()
        //{

        //    lblNewlyCreatedF.InnerText = "Newly Created: 0" ;
        //    lblConvertToProspectF.InnerText = "0";
        //    lblWonF.InnerText = "0";

        //    if (rbWeekF.Checked)
        //    {
        //        FromF = DateTime.Now.AddDays(-7);
        //    }
        //    else if (rbMonthF.Checked)
        //    {
        //        FromF = DateTime.Now.AddMonths(-1);
        //    }
        //    else if (rbYearF.Checked)
        //    {
        //        FromF = DateTime.Now.AddYears(-1);
        //    }

           
        //}

        //protected void rbStatusE_CheckedChanged(object sender, EventArgs e)
        //{
        //    FillEnquiryStatusCount();
        //}

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
            if (lbActions.Text == "Open")
            {
                Session["leadStatusID"] = 1;
            }
            else if (lbActions.Text == "Assigned")
            {
                Session["leadStatusID"] = 2;
            }
            else if (lbActions.Text == "Quotation")
            {
                Session["leadStatusID"] = 3;
            }
            else if (lbActions.Text == "Won")
            {
                Session["leadStatusID"] = 5;
            }
            else if (lbActions.Text == "Lost")
            {
                Session["leadStatusID"] = 6;
            }
            else if (lbActions.Text == "Cancelled")
            {
                Session["leadStatusID"] = 7;
            }
         
            
            if(rbToday.Checked)
            {
                Session["leadDateFrom"] = DateTime.Now; 
            }
            else if (rbWeek.Checked)
            {
                Session["leadDateFrom"] = DateTime.Now.AddDays(-7);
            }
            else if (rbMonth.Checked)
            {
                Session["leadDateFrom"] = DateTime.Now.AddMonths(-1);
            }
            else
            {
                Session["leadDateFrom"] = DateTime.Now.AddYears(-1);
            }

            Session["leadDealerID"] = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue); 
            Session["EngineerUserID"] = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);

            Response.Redirect("lead.aspx");
        }

        protected void lbEnquiryActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Open")
            {
                Session["leadStatusID"] = 1;
            }
            else if (lbActions.Text == "Converted To Lead")
            {
                Session["leadStatusID"] = 4;
            }
            else if (lbActions.Text == "Rejected")
            {
                Session["leadStatusID"] = 5;
            }

            if (rbToday.Checked)
            {
                Session["leadDateFrom"] = DateTime.Now;
            }
            else if (rbWeek.Checked)
            {
                Session["leadDateFrom"] = DateTime.Now.AddDays(-7);
            }
            else if (rbMonth.Checked)
            {
                Session["leadDateFrom"] = DateTime.Now.AddMonths(-1);
            }
            else
            {
                Session["leadDateFrom"] = DateTime.Now.AddYears(-1);
            }

            Session["leadDealerID"] = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            Session["EngineerUserID"] = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            Response.Redirect("Enquiry.aspx");
        }
        //void FillEnquiryStatusCount()
        //{
        //    lblEnquiryOpen.Text = "0";
        //    lblEnquiryConvertedToLead.Text = "0";
        //    lblEnquiryRejected.Text = "0"; 
        //    int? DealerID = null;
        //    if (rbEnquiryToday.Checked)
        //    {
        //        From = DateTime.Now;
        //    }
        //    else if (rbEnquiryWeek.Checked)
        //    {
        //        From = DateTime.Now.AddDays(-7);
        //    }
        //    else if (rbEnquiryMonth.Checked)
        //    {
        //        From = DateTime.Now.AddMonths(-1);
        //    }
        //    else if (rbEnquiryYear.Checked)
        //    {
        //        From = DateTime.Now.AddYears(-1);
        //    }



        //}

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
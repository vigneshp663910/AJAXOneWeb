using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyFailureMaterialGateEntry : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_WarrantyFailureMaterialGateEntry.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }

        public List<PDMS_WarrantyFailureMaterial> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_WarrantyFailureMaterialGateEntry"] == null)
                {
                    Session["DMS_WarrantyFailureMaterialGateEntry"] = new List<PDMS_WarrantyFailureMaterial>();
                }
                return (List<PDMS_WarrantyFailureMaterial>)Session["DMS_WarrantyFailureMaterialGateEntry"];
            }
            set
            {
                Session["DMS_WarrantyFailureMaterialGateEntry"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Failed Material » Warranty DC Gate Entry');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillDeliveryChallan();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillDeliveryChallan()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                if (string.IsNullOrEmpty(txtDeliveryChallanNumber.Text.Trim()))
                {
                    lblMessage.Text = "Please enter Delivery Challan Number";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                //  CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
                List<PDMS_WarrantyFailureMaterial> SOIs = new BDMS_WarrantyFailureMaterial().GetWarrantyFailedMaterialDeliveryChallan(null, txtDeliveryChallanNumber.Text.Trim(), null, null, "", null, null, null);

                if (SOIs.Count != 1)
                {
                    lblMessage.Text = "Please check with Delivery Challan Number";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                lblDeliveryChallanNumber.Text = SOIs[0].DeliveryChallanNumber;
                lblDeliveryChallanDate.Text = ((DateTime)SOIs[0].DeliveryChallanDate).ToShortDateString();
                lblDeliveryTo.Text = SOIs[0].DeliveryTo;
                lblTransporterName.Text = SOIs[0].TransporterName;

                lblDocketDetails.Text = SOIs[0].DocketDetails;
                lblCreatedBy.Text = SOIs[0].CreatedBy.ContactName;
                lblDealer.Text = SOIs[0].Dealer.DealerCode + "" + SOIs[0].Dealer.DealerName;
                gvDCItem.DataSource = SOIs[0].FailureMaterialItems;
                gvDCItem.DataBind();
                TraceLogger.Log(DateTime.Now);

            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyFailureMaterialGateEntry", "fillDeliveryChallan", e1);
                throw e1;
            }
        }
        protected void gvDCTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                }

                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long DeliveryChallanItemID = Convert.ToInt64(gvDCItem.DataKeys[gvRow.RowIndex].Value);
            if (new BDMS_WarrantyFailureMaterial().AcknowledgeWarrantyFailureMaterialDCItem(DeliveryChallanItemID, PSession.User.UserID))
            {
                lblMessage.Text = "Delivery Challan Item Acknowledged ";
                lblMessage.ForeColor = Color.Green;
                fillDeliveryChallan();
            }
            else
            {
                lblMessage.Text = "Delivery Challan Item is not Acknowledged ";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long DeliveryChallanItemID = Convert.ToInt64(gvDCItem.DataKeys[gvRow.RowIndex].Value);
            if (new BDMS_WarrantyFailureMaterial().CancelWarrantyFailureMaterialDCItem(DeliveryChallanItemID, PSession.User.UserID))
            {
                lblMessage.Text = "Delivery Challan Item Canceled ";
                lblMessage.ForeColor = Color.Green;
                fillDeliveryChallan();
            }
            else
            {
                lblMessage.Text = "Delivery Challan Item is not Canceled ";
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Business;
using Properties;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class DealerCustomerMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Relate Dealer & Customer');</script>");

            if (!IsPostBack)
            {
                FillDealer();                
            }
        }
        void FillDealer()
        {
            List<PDMS_Dealer> Dealer = new List<PDMS_Dealer>();
            Dealer = new BDMS_Dealer().GetDealer(null, null, null);
            new DDLBind(ddlDealerCode, Dealer, "DealerCode", "DealerID");
        }


        public List<PDMS_Customer> Customer
        {
            get
            {
                if (Session["Customer"] == null)
                {
                    Session["Customer"] = new List<PDMS_Customer>();
                }
                return (List<PDMS_Customer>)Session["Customer"];
            }
            set
            {
                Session["Customer"] = value;
            }
        }

        protected void ibtnDCArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvCustomer.PageIndex > 0)
            {
                gvCustomer.PageIndex = gvCustomer.PageIndex - 1;
                DCBind(gvCustomer, lblRowCount, Customer);
            }
        }
        protected void ibtnDCArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvCustomer.PageCount > gvCustomer.PageIndex)
            {
                gvCustomer.PageIndex = gvCustomer.PageIndex + 1;
                DCBind(gvCustomer, lblRowCount, Customer);
            }
        }

        void DCBind(GridView gv, Label lbl, List<PDMS_Customer> Customer)
        {
            gv.DataSource = Customer;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Customer.Count;
        }

        void FillCustomer()
        {
            int? DealerID = null;
            string DealerCode = null;
            if (ddlDealerCode.SelectedValue!="0")
            {
                DealerID = Convert.ToInt32(ddlDealerCode.SelectedValue.Trim());
            }

            //List<PDMS_Customer> Customer = new BDMS_Customer().GetDealerCustomer(DealerID, DealerCode);
            Customer = new BDMS_Customer().GetDealerCustomer(DealerID, DealerCode);
            gvCustomer.DataSource = Customer;
            gvCustomer.DataBind();

            if (Customer.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnDCArrowLeft.Visible = false;
                ibtnDCArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnDCArrowLeft.Visible = true;
                ibtnDCArrowRight.Visible = true;
                lblRowCount.Text = (((gvCustomer.PageIndex) * gvCustomer.PageSize) + 1) + " - " + (((gvCustomer.PageIndex) * gvCustomer.PageSize) + gvCustomer.Rows.Count) + " of " + Customer.Count;
            }


        }
        protected void lbViewCustomer_Click(object sender, EventArgs e)
        {
        }

        protected void gvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomer.PageIndex = e.NewPageIndex;
            FillCustomer();
        }

        protected void ddlDealerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCustomer();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                lblMessage.Text = string.Empty;
                Boolean Success = true;
                int Result = 0;
                string Message = "";

                if (ddlDealerCode.SelectedValue=="0")
                {
                    Message = Message + "<br/> Please Select the Dealer...!";
                    Success = false;
                }
                if(string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
                {
                    Message = Message + "<br/> Please Enter the CustomerCode...!";
                    Success = false;
                }
                lblMessage.Text = Message;
                if (Success == false)
                {
                    return;
                }
                else
                {
                    Result = new BDMS_Customer().InsertOrUpdateDealerCustomerMapping(null, Convert.ToInt32(ddlDealerCode.SelectedValue), txtCustomerCode.Text.Trim(), PSession.User.UserID, true);
                    if (Result == 1)
                    {
                        lblMessage.Text = "Dealer To Customer Mapped successfully";
                        lblMessage.ForeColor = Color.Green;
                        FillCustomer();
                    }
                    else if(Result == 2)
                    {
                        lblMessage.Text = "Dealer To Customer Already Mapped";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Dealer To Customer Is Not Mapped successfully";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void lblCustomerDelete_Click(object sender, EventArgs e)
        {
            int Result = 0;
            LinkButton lblCustomerDelete = (LinkButton)sender;
            string customercode = lblCustomerDelete.CommandArgument;
            Result = new BDMS_Customer().InsertOrUpdateDealerCustomerMapping(null, Convert.ToInt32(ddlDealerCode.SelectedValue), customercode.ToString().Trim(), PSession.User.UserID, false);
            if (Result == 1)
            {
                lblMessage.Text = "Mapping was Deleted successfully";
                lblMessage.ForeColor = Color.Green;
                FillCustomer();
            }
            else
            {
                lblMessage.Text = "Mapping was not deleted successfully";
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Business;
using Properties;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Newtonsoft.Json;

namespace DealerManagementSystem.ViewMaster
{
    public partial class DealerCustomerMapping : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_DealerCustomerMapping; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Related Dealer & Customer ');</script>");
            if (!IsPostBack)
            {
                FillDealer();                
            }
        }
        void FillDealer()
        {
            List<PDMS_Dealer> Dealer = new List<PDMS_Dealer>();
            Dealer = new BDMS_Dealer().GetDealer(null, null, null, null);
            new DDLBind(ddlDealerCode, Dealer, "DealerCode", "DealerID");
        }
        void FillCustomer()
        {
            int? DealerID = null;
            string DealerCode = null;
            if (ddlDealerCode.SelectedValue!="0")
            {
                DealerID = Convert.ToInt32(ddlDealerCode.SelectedValue.Trim());
            }
            gvCustomer.DataSource = new BDMS_Customer().GetDealerCustomer(DealerID, DealerCode, null);
            gvCustomer.DataBind();
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
                     
                    List<PDMS_Customer> Customer = JsonConvert.DeserializeObject<List<PDMS_Customer>>(JsonConvert.SerializeObject(new BDMS_Customer().GetCustomerN(null, txtCustomerCode.Text.Trim(), null, null, null, null, null, null, null, null).Data));
                    if (Result == 1)
                    {
                        lblMessage.Text = "Dealer To Customer Mapped successfully";
                        lblMessage.ForeColor = Color.Green;
                        FillCustomer();
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
                lblMessage.Text = Ex.Message.ToString();
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
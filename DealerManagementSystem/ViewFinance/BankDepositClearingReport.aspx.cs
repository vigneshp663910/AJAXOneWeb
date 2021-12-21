using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Business;
using Properties;

namespace DealerManagementSystem.ViewFinance
{
    public partial class BankDepositClearingReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            if (!IsPostBack)
            {
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    PDealer Dealer = new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0];
                    ddlDealer.Items.Add(new ListItem(PSession.User.ExternalReferenceID, Dealer.DID.ToString()));
                    ddlDealer.Enabled = false;
                }
                else
                {
                    ddlDealer.Enabled = true;
                    fillDealer();
                }

                new BDMS_BankDepositClearing().GetBankDepositClearingStatus(ddlStatus, null, null);
                new BDMS_Address().GetState(ddlState, null, null);
                new BDMS_Address().GetRegion(ddlRegion, null, null);

                List<PUser> u = new BUser().GetUsers(null, null, null, "");
                u = u.FindAll(m => m.SystemCategoryID == (short)SystemCategory.Dealer);


                ddlCreatedBy.DataTextField = "ContactName";
                ddlCreatedBy.DataValueField = "UserID";
                ddlCreatedBy.DataSource = u;
                ddlCreatedBy.DataBind();
                ddlCreatedBy.Items.Insert(0, new ListItem("All", "0"));

                ddlAccountedBy.DataTextField = "ContactName";
                ddlAccountedBy.DataValueField = "UserID";
                ddlAccountedBy.DataSource = u;
                ddlAccountedBy.DataBind();
                ddlAccountedBy.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();

            ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        }

        void FillBankDepositClearing()
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);

            string CustomerCode = txtCustomer.Text.Trim();
            DateTime? TransactionDateF = string.IsNullOrEmpty(txtTransactionDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTransactionDateF.Text.Trim());
            DateTime? TransactionDateT = string.IsNullOrEmpty(txtTransactionDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTransactionDateT.Text.Trim());

            int? CreatedBy = ddlCreatedBy.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCreatedBy.SelectedValue);
            DateTime? CreatedOnF = string.IsNullOrEmpty(txtCreatedDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtCreatedDateF.Text.Trim());
            DateTime? CreatedOnT = string.IsNullOrEmpty(txtCreatedDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtCreatedDateT.Text.Trim());

            int? AccountedBy = ddlAccountedBy.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlAccountedBy.SelectedValue);
            DateTime? AccountedOnF = string.IsNullOrEmpty(txtAccountedDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtAccountedDateF.Text.Trim());
            DateTime? AccountedOnT = string.IsNullOrEmpty(txtAccountedDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtAccountedDateT.Text.Trim());

            int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
            int? StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
            int? RegionID = ddlRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);


            gvSo.DataSource = new BDMS_BankDepositClearing().GetBankDepositClearing(null, DealerID, CustomerCode, TransactionDateF, TransactionDateT,
                CreatedBy, CreatedOnF, CreatedOnT, AccountedBy, AccountedOnF, AccountedOnT, StatusID, StateID, RegionID);
            gvSo.DataBind();


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillBankDepositClearing();
        }
    }
}
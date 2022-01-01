using Business;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewFinance
{
    public partial class BankDepositClearingPostingInSAP : System.Web.UI.Page
    {
        public List<PDMS_BankDepositClearing> BankConfirm
        {
            get
            {
                if (Session["DMS_BankDepositClearingPostingInSAP"] == null)
                {
                    Session["DMS_BankDepositClearingPostingInSAP"] = new List<PDMS_BankDepositClearing>();
                }
                return (List<PDMS_BankDepositClearing>)Session["DMS_BankDepositClearingPostingInSAP"];
            }
            set
            {
                Session["DMS_BankDepositClearingPostingInSAP"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Edit_BankDepositClearingID"] = null;
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                List<PModuleAccess> AccessModule = new BUser().GetDMSModuleByUser(PSession.User.UserID, null, (short)DMS_MenuSub.BankDepositClearingPostingInSAP);
                if (AccessModule.Count() == 0)
                {
                    Response.Redirect("Home.aspx");
                }

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
                List<PUser> u = new BUser().GetUsers(null, null, null, "");
                u = u.FindAll(m => m.SystemCategoryID == (short)SystemCategory.Dealer);
                ddlCreatedBy.DataTextField = "ContactName";
                ddlCreatedBy.DataValueField = "UserID";
                ddlCreatedBy.DataSource = u;
                ddlCreatedBy.DataBind();
                ddlCreatedBy.Items.Insert(0, new ListItem("All", "0"));
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

            DateTime? TransactionDateF = string.IsNullOrEmpty(txtTransactionDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTransactionDateF.Text.Trim());
            DateTime? TransactionDateT = string.IsNullOrEmpty(txtTransactionDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTransactionDateT.Text.Trim());

            int? CreatedBy = ddlCreatedBy.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCreatedBy.SelectedValue);
            DateTime? CreatedOnF = string.IsNullOrEmpty(txtCreatedDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtCreatedDateF.Text.Trim());
            DateTime? CreatedOnT = string.IsNullOrEmpty(txtCreatedDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtCreatedDateT.Text.Trim());

            BankConfirm = new BDMS_BankDepositClearing().GetBankDepositClearing(null, DealerID, "", TransactionDateF, TransactionDateT, CreatedBy, CreatedOnF, CreatedOnT,
                null, null, null, 2, null, null);
            gvSo.DataSource = BankConfirm;
            gvSo.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillBankDepositClearing();
        }

        protected void btnPostInSAP_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            string ID = gvSo.DataKeys[index].Value.ToString();
            PDMS_BankDepositClearing Bank = new BDMS_BankDepositClearing().GetBankDepositClearing(Convert.ToInt64(ID), null, null, null, null,
                null, null, null, null, null, null, null, null, null)[0];
            Bank = new SDMS_BankDepositClearing().UpdateBankDepositClearingToSAP(Bank);

            if (!string.IsNullOrEmpty(Bank.SapAccountNo))
            {
                Bank.AccountedBy = new PUser() { UserID = PSession.User.UserID };
                Bank.AccountedOn = DateTime.Now;
                new BDMS_BankDepositClearing().UpdateBankDepositClearing(Bank);
                lblMessage.Text = "Bank Deposit Clearing is successfully updated in SAP ";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "Bank Deposit Clearing is not successfully updated in SAP";
                lblMessage.ForeColor = Color.Green;
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Session["Edit_BankDepositClearingID"] = gvSo.DataKeys[index].Value.ToString();
            Response.Redirect("DMS_BankDepositClearingCreate.aspx");
        }
    }
}
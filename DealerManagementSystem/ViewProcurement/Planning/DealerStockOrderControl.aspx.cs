using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement.Planning
{
    public partial class DealerStockOrderControl : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewProcurement_DealerStockOrderControl; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        } 
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Procurement » Planning » Dealer Stock Order Control');</script>");
            
            lblMessage.Text = string.Empty;
            lblMessageCreateDealerSOControl.Text = string.Empty;
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                fillDealer();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
                if (PSession.User.SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerStockOrderControlAdminPermission).Count() == 0)
                {
                    btnCreate.Visible = false;
                }
            }
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("Select", "0"));

            ddlCDealerCode.DataTextField = "CodeWithName";
            ddlCDealerCode.DataValueField = "DID";
            ddlCDealerCode.DataSource = PSession.User.Dealer;
            ddlCDealerCode.DataBind();
            ddlCDealerCode.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillDealerStockOrderControlList();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillDealerStockOrderControlList();
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PageCount = 0;
                PageIndex = 1;
                fillDealerStockOrderControlList();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        void fillDealerStockOrderControlList()
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                TraceLogger.Log(DateTime.Now);
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);

                PApiResult Result = new BDMS_PurchaseOrder().GetDealerStockOrderControl(DealerID, PageIndex, gvDealerStockOrderControl.PageSize);
                List<PDealerStockOrderControl> DealerStockOrderControlList = JsonConvert.DeserializeObject<List<PDealerStockOrderControl>>(JsonConvert.SerializeObject(Result.Data));

                gvDealerStockOrderControl.DataSource = DealerStockOrderControlList;
                gvDealerStockOrderControl.DataBind();

                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvDealerStockOrderControl.PageSize - 1) / gvDealerStockOrderControl.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvDealerStockOrderControl.PageSize) + 1) + " - " + (((PageIndex - 1) * gvDealerStockOrderControl.PageSize) + gvDealerStockOrderControl.Rows.Count) + " of " + Result.RowCount;
                }

                foreach (GridViewRow row in gvDealerStockOrderControl.Rows)
                {
                    LinkButton LnkEdit = (LinkButton)row.FindControl("LnkEdit");
                    LinkButton LnkDelete = (LinkButton)row.FindControl("LnkDelete");
                    if (PSession.User.SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerStockOrderControlAdminPermission).Count() != 0)
                    {

                    }
                    else if (PSession.User.SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerStockOrderControlBasicPermission).Count() != 0)
                    {
                        LnkDelete.Visible = false;
                    }
                    else
                    {
                        LnkDelete.Visible = false;
                        LnkEdit.Visible = false;
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DealerStockOrderControl", "fillDealerStockOrderControlList", e1);
                throw e1;
            }
        }

        protected void LnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton LnkEdit = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(LnkEdit.NamingContainer);

            Label lblDealerStockOrderControlID = (Label)row.FindControl("lblDealerStockOrderControlID");
            HidDealerStockOrderControlID.Value = lblDealerStockOrderControlID.Text;

            Label lblMaxCount = (Label)row.FindControl("lblMaxCount");
            Label lblMinimumValue = (Label)row.FindControl("lblMinimumValue");
            Label lblDefaultCount = (Label)row.FindControl("lblDefaultCount");
            Label lblDealerID = (Label)row.FindControl("lblDealerID");
            if (PSession.User.SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerStockOrderControlAdminPermission).Count() == 0)
            {
                divDefaultvalue.Visible = false;
            }
            ddlCDealerCode.SelectedValue = lblDealerID.Text;
            ddlCDealerCode.Enabled = false;
            txtMaxCount.Text = lblMaxCount.Text;
            txtMinimumValue.Text = lblMinimumValue.Text;
            txtDefaultCount.Text = lblDefaultCount.Text;
            MPE_DealerSOControl.Show();
        }

        protected void LnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;

                LinkButton LnkDelete = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(LnkDelete.NamingContainer);

                Label lblMaxCount = (Label)row.FindControl("lblMaxCount");
                Label lblMinimumValue = (Label)row.FindControl("lblMinimumValue");
                Label lblDefaultCount = (Label)row.FindControl("lblDefaultCount");
                Label lblDealerStockOrderControlID = (Label)row.FindControl("lblDealerStockOrderControlID");
                Label lblDealerID = (Label)row.FindControl("lblDealerID");

                PDealerStockOrderControl orderControl = new PDealerStockOrderControl();
                orderControl.DealerStockOrderControlID = Convert.ToInt32(lblDealerStockOrderControlID.Text);
                orderControl.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(lblDealerID.Text) };
                orderControl.MaxCount = (string.IsNullOrEmpty(lblMaxCount.Text)) ? 0 : Convert.ToInt32(lblMaxCount.Text);
                orderControl.MinimumValue = (string.IsNullOrEmpty(lblMinimumValue.Text)) ? 0 : Convert.ToInt32(lblMinimumValue.Text);
                orderControl.DefaultCount = (string.IsNullOrEmpty(lblDefaultCount.Text)) ? 0 : Convert.ToInt32(lblDefaultCount.Text);
                orderControl.IsActive = false;

                string result = new BAPI().ApiPut("PurchaseOrder/InsertOrUpdateDealerStockOrderControl", orderControl);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.Text = Result.Message;
                    return;
                }
                fillDealerStockOrderControlList();
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString(); ;
            }
        }
        protected void btnSaveDealerSOControl_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageCreateDealerSOControl.ForeColor = Color.Red;
                string Message = Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessageCreateDealerSOControl.Text = Message;
                    MPE_DealerSOControl.Show();
                    return;
                }

                PDealerStockOrderControl orderControl = new PDealerStockOrderControl();
                if (!string.IsNullOrEmpty(HidDealerStockOrderControlID.Value))
                {
                    orderControl.DealerStockOrderControlID = Convert.ToInt32(HidDealerStockOrderControlID.Value);
                }
                orderControl.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(ddlCDealerCode.SelectedValue) };
                orderControl.MaxCount = Convert.ToInt32(txtMaxCount.Text);
                orderControl.MinimumValue = Convert.ToInt32(txtMinimumValue.Text);
                orderControl.DefaultCount = (string.IsNullOrEmpty(txtDefaultCount.Text)) ? 0 : Convert.ToInt32(txtDefaultCount.Text);
                orderControl.IsActive = true;

                string result = new BAPI().ApiPut("PurchaseOrder/InsertOrUpdateDealerStockOrderControl", orderControl);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessageCreateDealerSOControl.Text = Result.Message;
                    MPE_DealerSOControl.Show();
                    return;
                }
                clear();
                fillDealerStockOrderControlList();
                MPE_DealerSOControl.Hide();
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (PSession.User.SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerStockOrderControlAdminPermission).Count() == 0)
            {
                divDefaultvalue.Visible = false;
            }
            ddlCDealerCode.Enabled = true;
            clear();
            MPE_DealerSOControl.Show();
        }
        public string Validation()
        {
            ddlCDealerCode.BorderColor = Color.Silver;
            txtMaxCount.BorderColor = Color.Silver;
            txtMinimumValue.BorderColor = Color.Silver;
            txtDefaultCount.BorderColor = Color.Silver;
            string Message = "";
            if (ddlCDealerCode.SelectedValue == "0")
            {
                ddlCDealerCode.BorderColor = Color.Red;
                return "Please select the Dealer Code.";
            }
            if (string.IsNullOrEmpty(txtMaxCount.Text))
            {
                txtMaxCount.BorderColor = Color.Red;
                return "Please enter the Max Count.";
            }
            if (string.IsNullOrEmpty(txtMinimumValue.Text))
            {
                txtMinimumValue.BorderColor = Color.Red;
                return "Please enter the Minimum Value.";
            }
            Decimal.TryParse(txtMaxCount.Text, out decimal MaxCount);
            if (MaxCount < 0)
            {
                txtMaxCount.BorderColor = Color.Red;
                return "Please enter Valid Max Count.";
            }
            Decimal.TryParse(txtMinimumValue.Text, out decimal MinimumValue);
            if (MinimumValue < 0)
            {
                txtMinimumValue.BorderColor = Color.Red;
                return "Please enter Valid Minimum Value.";
            }
            return Message;
        }
        void clear()
        {
            ddlCDealerCode.SelectedValue = "0";
            txtMaxCount.Text = "";
            txtMinimumValue.Text = "";
            txtDefaultCount.Text = "";
            HidDealerStockOrderControlID.Value = "";
        }
    }
}
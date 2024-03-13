using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewBusinessExcellence.UserControls
{
    public partial class ViewDealerBusinessExcellence : System.Web.UI.UserControl
    {
        public PDealerBusinessExcellenceHeader DealerBusiness
        {
            get
            {
                if (ViewState["ViewDealerBusinessExcellence"] == null)
                {
                    ViewState["ViewDealerBusinessExcellence"] = new PDealerBusinessExcellenceHeader();
                }
                return (PDealerBusinessExcellenceHeader)ViewState["ViewDealerBusinessExcellence"];
            }
            set
            {
                ViewState["ViewDealerBusinessExcellence"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbtnSubmit")
            {
                string endPoint = "DealerBusinessExcellence/UpdateDealerBusinessExcellenceStatus?Year=" + DealerBusiness.Year
                    + "&Month=" + DealerBusiness.Month + "&DealerID=" + DealerBusiness.Dealer.DealerID 
                    + "&StatusID=" + ((short)AjaxOneStatus.DealerBusinessExcellence_Submitted).ToString();
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                else
                {
                    lblMessage.Text = "Submitted Successfully";
                    fill(DealerBusiness.DealerBusinessExcellenceID);
                }
            }
            else if (lbActions.ID == "lbtnApproveL1")
            {
                string endPoint = "DealerBusinessExcellence/UpdateDealerBusinessExcellenceStatus?Year=" + DealerBusiness.Year + "&Month=" + DealerBusiness.Month + "&DealerID=" + DealerBusiness.Dealer.DealerID
                    + "&StatusID=" + ((short)AjaxOneStatus.DealerBusinessExcellence_ApprovalL1).ToString();
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                else
                {
                    lblMessage.Text = "Submitted Successfully";
                    fill(DealerBusiness.DealerBusinessExcellenceID);
                }
            }
            else if (lbActions.ID == "lbtnApproveL2")
            {
                string endPoint = "DealerBusinessExcellence/UpdateDealerBusinessExcellenceStatus?Year=" + DealerBusiness.Year + "&Month=" + DealerBusiness.Month + "&DealerID=" + DealerBusiness.Dealer.DealerID
                    + "&StatusID=" + ((short)AjaxOneStatus.DealerBusinessExcellence_ApprovalL2).ToString();
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                else
                {
                    lblMessage.Text = "Submitted Successfully";
                    fill(DealerBusiness.DealerBusinessExcellenceID);
                } 
            }
            else if (lbActions.ID == "lbtnApproveL3")
            {
                string endPoint = "DealerBusinessExcellence/UpdateDealerBusinessExcellenceStatus?Year=" + DealerBusiness.Year + "&Month=" + DealerBusiness.Month + "&DealerID=" + DealerBusiness.Dealer.DealerID
                    + "&StatusID=" + ((short)AjaxOneStatus.DealerBusinessExcellence_ApprovalL3).ToString();
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                else
                {
                    lblMessage.Text = "Submitted Successfully";
                    fill(DealerBusiness.DealerBusinessExcellenceID);
                }
            }
            else if (lbActions.ID == "lbtnApproveL4")
            {
                string endPoint = "DealerBusinessExcellence/UpdateDealerBusinessExcellenceStatus?Year=" + DealerBusiness.Year + "&Month=" + DealerBusiness.Month + "&DealerID=" + DealerBusiness.Dealer.DealerID
                    + "&StatusID=" + ((short)AjaxOneStatus.DealerBusinessExcellence_Approved).ToString();
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                else
                {
                    lblMessage.Text = "Submitted Successfully";
                    fill(DealerBusiness.DealerBusinessExcellenceID);
                }
            }
            
        }
        public void fill(long DealerBusinessExcellenceID)
        {
            DealerBusiness = new BDealerBusiness().GetDealerBusinessExcellenceByID(DealerBusinessExcellenceID);
            lblYear.Text = Convert.ToString(DealerBusiness.Year);
            lblMonth.Text = DealerBusiness.MonthName;
            lblDealer.Text = DealerBusiness.Dealer.DealerCode;
            lblDealerName.Text = DealerBusiness.Dealer.DealerName;
            lblRequestedBy.Text = DealerBusiness.RequestedBy.ContactName;
            lblRequestedOn.Text = DealerBusiness.RequestedOn.ToLongDateString();

            lblSubmittedBy.Text = DealerBusiness.SubmittedBy == null ? "" : DealerBusiness.SubmittedBy.ContactName;
            lblSubmittedOn.Text = Convert.ToString(DealerBusiness.SubmittedOn);

            lblApprovalL1By.Text = DealerBusiness.ApprovalL1By == null ? "" : DealerBusiness.ApprovalL1By.ContactName;
            lblApprovalL1On.Text = Convert.ToString(DealerBusiness.ApprovalL1On);

            lblApprovalL2By.Text = DealerBusiness.ApprovalL2By == null ? "" : DealerBusiness.ApprovalL2By.ContactName;
            lblApprovalL2On.Text = Convert.ToString(DealerBusiness.ApprovalL2On);

            lblApprovalL3By.Text = DealerBusiness.ApprovalL3By == null ? "" : DealerBusiness.ApprovalL3By.ContactName;
            lblApprovalL3On.Text = Convert.ToString(DealerBusiness.ApprovalL3On);

            lblApprovalL4By.Text = DealerBusiness.ApprovalL4By == null ? "" : DealerBusiness.ApprovalL4By.ContactName;
            lblApprovalL4On.Text = Convert.ToString(DealerBusiness.ApprovalL4On);

            gvDealer.DataSource = DealerBusiness.Items;
            gvDealer.DataBind(); 
            ActionControlMange();

            if (!(lbtnSubmit.Visible || lbtnApproveL1.Visible || lbtnApproveL2.Visible || lbtnApproveL3.Visible || lbtnApproveL4.Visible))
            {
                for (int i = 0; i < gvDealer.Rows.Count;    i++)
                { 
                    LinkButton lnkBtnMissionPlanningEdit = (LinkButton)gvDealer.Rows[i].FindControl("lnkBtnMissionPlanningEdit");
                    lnkBtnMissionPlanningEdit.Visible = false; 
                }
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtnMissionPlanningEdit = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(lnkBtnMissionPlanningEdit.NamingContainer);

            Label lblTarget = (Label)row.FindControl("lblTarget");
            Label lblActual = (Label)row.FindControl("lblActual");
            Label lblRemarks = (Label)row.FindControl("lblRemarks");

            TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
            TextBox txtActual = (TextBox)row.FindControl("txtActual");
            TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

            lblTarget.Visible = false;
            lblActual.Visible = false;
            lblRemarks.Visible = false;

            txtTarget.Visible = true;
            txtActual.Visible = true;
            txtRemarks.Visible = true;

            txtTarget.Text = lblTarget.Text;
            txtActual.Text = lblActual.Text;
            txtRemarks.Text = lblRemarks.Text;

            Button BtnUpdateMissionPlanning = (Button)row.FindControl("BtnUpdateMissionPlanning");
            Button btnBack = (Button)row.FindControl("btnBack");
            BtnUpdateMissionPlanning.Visible = true;
            btnBack.Visible = true;
            lnkBtnMissionPlanningEdit.Visible = false;
        }
        protected void BtnUpdateMissionPlanning_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)(btn.NamingContainer);

            Label lblTarget = (Label)row.FindControl("lblTarget");
            Label lblActual = (Label)row.FindControl("lblActual");
            Label lblRemarks = (Label)row.FindControl("lblRemarks");

            TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
            TextBox txtActual = (TextBox)row.FindControl("txtActual");
            TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

            if (btn.ID == "btnBack")
            {
                lblTarget.Visible = true;
                lblActual.Visible = true;
                lblRemarks.Visible = true;

                txtTarget.Visible = false;
                txtActual.Visible = false;
                txtRemarks.Visible = false;

                Button BtnUpdateMissionPlanning = (Button)row.FindControl("BtnUpdateMissionPlanning");
                Button btnBack = (Button)row.FindControl("btnBack");
                LinkButton lnkBtnMissionPlanningEdit = (LinkButton)row.FindControl("lnkBtnMissionPlanningEdit");
                BtnUpdateMissionPlanning.Visible = false;
                btnBack.Visible = false;
                lnkBtnMissionPlanningEdit.Visible = true;

            }
            else
            { 

                Label lblParameterID = (Label)row.FindControl("lblParameterID"); 
                List<PDealerBusinessExcellence> Plannings = new List<PDealerBusinessExcellence>();
                decimal value;
                if (!Decimal.TryParse(txtTarget.Text.Trim(), out value))
                {
                    lblMessage.Text = "Please update proper value in Target";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (!Decimal.TryParse(txtActual.Text.Trim(), out value))
                {
                    lblMessage.Text = "Please update proper value in Actual";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                PDealerBusinessExcellence Planning = new PDealerBusinessExcellence()
                {
                    DealerID = DealerBusiness.Dealer.DealerID,
                    DealerBusinessExcellenceCategory3ID = Convert.ToInt32(lblParameterID.Text),
                    Year = DealerBusiness.Year,
                    Month = DealerBusiness.Month,
                    Target = string.IsNullOrEmpty(txtTarget.Text.Trim()) ? 0 : Convert.ToDecimal(txtTarget.Text.Trim()),
                    Actual = string.IsNullOrEmpty(txtActual.Text.Trim()) ? 0 : Convert.ToDecimal(txtActual.Text.Trim()),
                    Remarks = txtRemarks.Text.Trim()
                };
                Plannings.Add(Planning);

                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("DealerBusinessExcellence/InsertOrUpdateDealerBusinessExcellence", Plannings));

                if (result.Status == PApplication.Failure)
                {
                    lblMessage.Text = "Dealer Mission Planning is not updated successfully ";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Dealer Mission Planning is updated successfully ";

                    lblTarget.Visible = true;
                    lblActual.Visible = true;
                    lblRemarks.Visible = true;

                    txtTarget.Visible = false;
                    txtActual.Visible = false;
                    txtRemarks.Visible = false;

                    lblTarget.Text = txtTarget.Text;
                    lblActual.Text = txtActual.Text;
                    lblRemarks.Text = txtRemarks.Text; ;

                    Button BtnUpdateMissionPlanning = (Button)row.FindControl("BtnUpdateMissionPlanning");
                    Button btnBack = (Button)row.FindControl("btnBack");
                    LinkButton lnkBtnMissionPlanningEdit = (LinkButton)row.FindControl("lnkBtnMissionPlanningEdit");
                    BtnUpdateMissionPlanning.Visible = false;
                    btnBack.Visible = false;
                    lnkBtnMissionPlanningEdit.Visible = true;
                }
            }
        }
        void ActionControlMange()
        {
            lbtnSubmit.Visible = true;
            lbtnApproveL1.Visible = true;
            lbtnApproveL2.Visible = true;
            lbtnApproveL3.Visible = true;
            lbtnApproveL4.Visible = true;  
             
            if (DealerBusiness.Status.StatusID == (short)AjaxOneStatus.DealerBusinessExcellence_Requested) 
            {
                lbtnApproveL1.Visible = false;
                lbtnApproveL2.Visible = false;
                lbtnApproveL3.Visible = false;
                lbtnApproveL4.Visible = false;
            }
            else if (DealerBusiness.Status.StatusID == (short)AjaxOneStatus.DealerBusinessExcellence_Submitted)
            {
                lbtnSubmit.Visible = false;
                lbtnApproveL2.Visible = false;
                lbtnApproveL3.Visible = false;
                lbtnApproveL4.Visible = false;
            }
            else if (DealerBusiness.Status.StatusID == (short)AjaxOneStatus.DealerBusinessExcellence_ApprovalL1)
            {
                lbtnSubmit.Visible = false;
                lbtnApproveL1.Visible = false;
                lbtnApproveL3.Visible = false;
                lbtnApproveL4.Visible = false;
            }
            else if (DealerBusiness.Status.StatusID == (short)AjaxOneStatus.DealerBusinessExcellence_ApprovalL2)
            {
                lbtnSubmit.Visible = false;
                lbtnApproveL1.Visible = false;
                lbtnApproveL2.Visible = false;
                lbtnApproveL4.Visible = false;
            }
            else if (DealerBusiness.Status.StatusID == (short)AjaxOneStatus.DealerBusinessExcellence_ApprovalL3)
            {
                lbtnSubmit.Visible = false;
                lbtnApproveL1.Visible = false;
                lbtnApproveL2.Visible = false;
                lbtnApproveL3.Visible = false;
            }
            else if (DealerBusiness.Status.StatusID == (short)AjaxOneStatus.DealerBusinessExcellence_Approved)
            {
                lbtnSubmit.Visible = false;
                lbtnApproveL1.Visible = false;
                lbtnApproveL2.Visible = false;
                lbtnApproveL3.Visible = false;
                lbtnApproveL4.Visible = false;
            }
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerBusinessExcellenceSubmit).Count() == 0)
            {
                lbtnSubmit.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerBusinessExcellenceApproveL1).Count() == 0)
            {
                lbtnApproveL1.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerBusinessExcellenceApproveL2).Count() == 0)
            {
                lbtnApproveL2.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerBusinessExcellenceApproveL3).Count() == 0)
            {
                lbtnApproveL3.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerBusinessExcellenceApproveL4).Count() == 0)
            {
                lbtnApproveL4.Visible = false;
            }
        }
    }
}
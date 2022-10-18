using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class DealerEmployeeManage : System.Web.UI.Page
    {
        public string AadhaarCardNo
        {
            get
            {
                return txtAadhaarCardNo.Text.Trim().Replace("-", "");
            }
        }
        public List<PDMS_DealerEmployee> ICTicket
        {
            get
            {
                if (Session["DMS_DealerEmployeeManage"] == null)
                {
                    Session["DMS_DealerEmployeeManage"] = new List<PDMS_DealerEmployee>();
                }
                return (List<PDMS_DealerEmployee>)Session["DMS_DealerEmployeeManage"];
            }
            set
            {
                Session["DMS_DealerEmployeeManage"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_DealerEmployeeManage.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealership Manpower Manage');</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealer Employee » Manage');</script>");

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

                new BDMS_Address().GetStateDDL(ddlState, null, null, null, null);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillDealerEmployee();
        }
        private void FillDealerEmployee()
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? StateID = null;
            Boolean? StatusID = null;
            if (ddlStatus.SelectedValue != "-1")
            {
                StatusID = Convert.ToBoolean(Convert.ToInt32(ddlStatus.SelectedValue));
            }
            ICTicket = new BDMS_Dealer().GetDealerEmployeeManage(DealerID, AadhaarCardNo, StateID, null, txtName.Text.Trim(), null, StatusID);
            gvDealerEmployee.DataSource = ICTicket;
            gvDealerEmployeeDataBind();
        }
        void gvDealerEmployeeDataBind()
        {
            gvDealerEmployee.DataBind();
            lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + ICTicket.Count;
             
            Boolean EditAlloved = false;

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;

            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EmployeeEdit).Count() != 0)
            {
                EditAlloved = true;
            }
            //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DealerEmployeeApproval).Count() != 0))
            //{
            //    EditAlloved = true;
            //}
            for (int i = 0; i < gvDealerEmployee.Rows.Count; i++)
            {
                Label lblCreatedByID = (Label)gvDealerEmployee.Rows[i].FindControl("lblCreatedByID");
                CheckBox cbIsAjaxHPApproved = (CheckBox)gvDealerEmployee.Rows[i].FindControl("cbIsAjaxHPApproved");
                LinkButton lbEdit = (LinkButton)gvDealerEmployee.Rows[i].FindControl("lbEdit");
                if (lblCreatedByID.Text != PSession.User.UserID.ToString() || cbIsAjaxHPApproved.Checked)
                {
                    lbEdit.Visible = false;
                }
                if (EditAlloved == true)
                {
                    lbEdit.Visible = true;
                }
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageIndex > 0)
            {
                gvDealerEmployee.DataSource = ICTicket;
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex - 1;
                gvDealerEmployeeDataBind();
                //gvDealerEmployee.DataBind();
                //lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + ICTicket.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageCount > gvDealerEmployee.PageIndex)
            {
                gvDealerEmployee.DataSource = ICTicket;
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex + 1;
                gvDealerEmployeeDataBind();
                //gvDealerEmployee.DataBind();
                //lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + ICTicket.Count;
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
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Role ID");
            dt.Columns.Add("Status");
            dt.Columns.Add("Aadhaar Card No");
            dt.Columns.Add("Name");
            dt.Columns.Add("Contact Number1");
            dt.Columns.Add("Email");
            dt.Columns.Add("Date Of Joining");
            dt.Columns.Add("Date Of Leaving");

            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");

            dt.Columns.Add("Dealer region");
            dt.Columns.Add("Department");
            dt.Columns.Add("Designation");
            dt.Columns.Add("Father Name");
            dt.Columns.Add("DOB");
            dt.Columns.Add("Contact Number2");
            dt.Columns.Add("Office Name");
            dt.Columns.Add("Reporting To");
            dt.Columns.Add("SAP Emp Code");


            dt.Columns.Add("Total Experience");
            //  dt.Columns.Add("Equcational");
            dt.Columns.Add("PAN No");
            dt.Columns.Add("BankName");
            dt.Columns.Add("AccountNo");
            dt.Columns.Add("IFSCCode");

            List<PDMS_DealerEmployee> DealerEmployee = new List<PDMS_DealerEmployee>();
            if (cbBasedOnRole.Checked)
            {
                DealerEmployee = new BDMS_Dealer().GetDealerEmployeeManageBasedRole(AadhaarCardNo);
            }
            else
            {
                DealerEmployee = ICTicket;
            }
            foreach (PDMS_DealerEmployee Emp in DealerEmployee)
            {
                if (Emp.DealerEmployeeRole == null)
                {
                    dt.Rows.Add(
                         ""
                      , ""
                  , Emp.AadhaarCardNo
                 , Emp.Name
                 , Emp.ContactNumber
                , Emp.Email
                , "" //  Emp.DealerEmployeeRole.DateOfJoining.ToShortDateString()
                 , "" // Emp.DealerEmployeeRole.DateOfLeaving == null ? "" : ((DateTime)Emp.DealerEmployeeRole.DateOfLeaving).ToShortDateString()

                  , ""  // Emp.DealerEmployeeRole.Dealer.DealerCode
                  , "" //  Emp.DealerEmployeeRole.Dealer.DealerName

                     , "" //  Emp.DealerEmployeeRole.Dealer.State 
                     , "" // Emp.DealerEmployeeRole.DealerDepartment.DealerDepartment
                      , "" //  Emp.DealerEmployeeRole.DealerDesignation.DealerDesignation
                      , Emp.FatherName
                      , Emp.DOB == null ? "" : ((DateTime)Emp.DOB).ToShortDateString()
                       , Emp.ContactNumber1
                        , "" //  Emp.DealerEmployeeRole.DealerOffice.OfficeName
                      , "" //  Emp.DealerEmployeeRole.ReportingTo == null ? "" : Emp.DealerEmployeeRole.ReportingTo.Name

                      , "" // Emp.DealerEmployeeRole.SAPEmpCode
                     , Emp.TotalExperience
                     , Emp.PANNo
                     , Emp.BankName
                     , "'" + Emp.AccountNo
                     , Emp.IFSCCode

                );
                }
                else
                {
                    dt.Rows.Add(
                        Emp.DealerEmployeeRole.DealerEmployeeRoleID
                    , Emp.DealerEmployeeRole.IsActiveString
                        , Emp.AadhaarCardNo
                     , Emp.Name
                     , Emp.ContactNumber
                    , Emp.Email
                     , Emp.DealerEmployeeRole.DateOfJoining == null ? "" : ((DateTime)Emp.DealerEmployeeRole.DateOfJoining).ToShortDateString()  
                    , Emp.DealerEmployeeRole.DateOfLeaving == null ? "" : ((DateTime)Emp.DealerEmployeeRole.DateOfLeaving).ToShortDateString()

                       , Emp.DealerEmployeeRole.Dealer.DealerCode
                        , Emp.DealerEmployeeRole.Dealer.DealerName

                         , Emp.DealerEmployeeRole.Dealer.State
                         , Emp.DealerEmployeeRole.DealerDepartment.DealerDepartment
                         , Emp.DealerEmployeeRole.DealerDesignation.DealerDesignation
                         , Emp.FatherName
                          , Emp.DOB == null ? "" : ((DateTime)Emp.DOB).ToShortDateString()
                           , Emp.ContactNumber1
                           , Emp.DealerEmployeeRole.DealerOffice.OfficeName
                         , Emp.DealerEmployeeRole.ReportingTo == null ? "" : Emp.DealerEmployeeRole.ReportingTo.Name

                         , Emp.DealerEmployeeRole.SAPEmpCode

                     , Emp.TotalExperience
                     , Emp.PANNo
                     , Emp.BankName
                     , "'" + Emp.AccountNo
                         , Emp.IFSCCode

                     );
                }
            }
            new BXcel().ExporttoExcel(dt, "Dealer Employee");
        }
        protected void lbEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            string url = "";
           Label lblDealerCode = (Label)gvDealerEmployee.Rows[index].FindControl("lblDealerCode");
            if (lblDealerCode.Text == "2000")
            {
                url = "CreateAjaxEmployee.aspx?DealerEmployeeID=" + gvDealerEmployee.DataKeys[index].Value.ToString();
            }
            else
            {
                url = "DealerEmployeeCreate.aspx?DealerEmployeeID=" + gvDealerEmployee.DataKeys[index].Value.ToString();
            } 
            Response.Redirect(url);
        }
        protected void lbView_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            string url = "DealerEmployeeView.aspx?DealerEmployeeID=" + gvDealerEmployee.DataKeys[index].Value.ToString();
            Response.Redirect(url);
        }
        protected void gvDealerEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerEmployee.PageIndex = e.NewPageIndex;
            gvDealerEmployee.DataSource = ICTicket;
            gvDealerEmployeeDataBind();
            //gvDealerEmployee.DataBind();
            //lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + ICTicket.Count;

        }
    }
}
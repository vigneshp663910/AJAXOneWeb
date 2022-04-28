using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class VisitTargetManage : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Visit Target');</script>");
            if (!IsPostBack)
            {
                FillYearAndMonth();
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;

                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EditVisitTarget).Count() != 0)
                {
                    btnEdit.Visible = true;
                }
            }
        }

        void FillYearAndMonth()
        {
            ddlYear.Items.Insert(0, new ListItem("All", "0"));
            for (int i = 2022; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Insert(i + 1 - 2022, new ListItem(i.ToString(), i.ToString()));
            }

            ddlMonth.Items.Insert(0, new ListItem("All", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Insert(i, new ListItem(i.ToString(), i.ToString()));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (BtnSearch.Text == "Back")
            {
                for (int i = 0; i < gvVisitTarget.Rows.Count; i++)
                {
                    TextBox txtNewCustomerTarget = (TextBox)gvVisitTarget.Rows[i].FindControl("txtNewCustomerTarget");
                    TextBox txtProspectCustomerTarget = (TextBox)gvVisitTarget.Rows[i].FindControl("txtProspectCustomerTarget");
                    TextBox txtExistCustomerTarget = (TextBox)gvVisitTarget.Rows[i].FindControl("txtExistCustomerTarget");

                    Label lblNewCustomerTargett = (Label)gvVisitTarget.Rows[i].FindControl("lblNewCustomerTarget");
                    Label lblProspectCustomerTarget = (Label)gvVisitTarget.Rows[i].FindControl("lblProspectCustomerTarget");
                    Label lblExistCustomerTarget = (Label)gvVisitTarget.Rows[i].FindControl("lblExistCustomerTarget");

                    txtNewCustomerTarget.Visible = false;
                    txtProspectCustomerTarget.Visible = false;
                    txtExistCustomerTarget.Visible = false;

                    lblNewCustomerTargett.Visible = true;
                    lblProspectCustomerTarget.Visible = true;
                    lblExistCustomerTarget.Visible = true;

                }
                BtnSearch.Text = "Retrieve";
                btnEdit.Text = "Edit";
            }
            else
            {
                FillVisitTarget();
            }
        }


        public List<PVisitTarget> VT
        {
            get
            {
                if (Session["PVisitTarget"] == null)
                {
                    Session["PVisitTarget"] = new List<PVisitTarget>();
                }
                return (List<PVisitTarget>)Session["PVisitTarget"];
            }
            set
            {
                Session["PVisitTarget"] = value;
            }
        }

        protected void gvVisitTarget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvVisitTarget.PageIndex = e.NewPageIndex;
            FillVisitTarget();
            gvVisitTarget.DataBind();
        }


        protected void ibtnVTArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvVisitTarget.PageIndex > 0)
            {
                gvVisitTarget.PageIndex = gvVisitTarget.PageIndex - 1;
                VTBind(gvVisitTarget, lblRowCountV, VT);
            }
        }
        protected void ibtnVTArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvVisitTarget.PageCount > gvVisitTarget.PageIndex)
            {
                gvVisitTarget.PageIndex = gvVisitTarget.PageIndex + 1;
                VTBind(gvVisitTarget, lblRowCountV, VT);
            }
        }

        void VTBind(GridView gv, Label lbl, List<PVisitTarget> VT)
        {
            gv.DataSource = VT;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + VT.Count;
        }


        void FillVisitTarget()
        {
            BtnSearch.Text = "Retrieve";
            btnEdit.Text = "Edit";
            int? Year = ddlYear.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlYear.SelectedValue);
            int? Month = ddlMonth.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? DepartmentID = null;
            int? DealerEmployeeID = null;
            //gvVisitTarget.DataSource = new BColdVisit().GetVisitTarget(Year, Month, DealerID, DepartmentID, DealerEmployeeID, PSession.User.UserID);

            // TO VERIFY WITH JOHN
            VT = new BColdVisit().GetVisitTarget(Year, Month, DealerID, DepartmentID, DealerEmployeeID, PSession.User.UserID);
            gvVisitTarget.DataSource = VT;
            gvVisitTarget.DataBind();

            if (VT.Count == 0)
            {
                lblRowCountV.Visible = false;
                ibtnVTArrowLeft.Visible = false;
                ibtnVTArrowRight.Visible = false;
            }
            else
            {
                lblRowCountV.Visible = true;
                ibtnVTArrowLeft.Visible = true;
                ibtnVTArrowRight.Visible = true;
                lblRowCountV.Text = (((gvVisitTarget.PageIndex) * gvVisitTarget.PageSize) + 1) + " - " + (((gvVisitTarget.PageIndex) * gvVisitTarget.PageSize) + gvVisitTarget.Rows.Count) + " of " + VT.Count;
            }

        }


        void Update()
        {
            List<PVisitTarget> VisitTarget = new List<PVisitTarget>();
            for (int i = 0; i < gvVisitTarget.Rows.Count; i++)
            {
                Label lblVisitTargetID = (Label)gvVisitTarget.Rows[i].FindControl("lblVisitTargetID");
                Label lblDealerEmployeeID = (Label)gvVisitTarget.Rows[i].FindControl("lblDealerEmployeeID");
                Label lblDealerID = (Label)gvVisitTarget.Rows[i].FindControl("lblDealerID");
                Label lblYear = (Label)gvVisitTarget.Rows[i].FindControl("lblYear");
                Label lblMonth = (Label)gvVisitTarget.Rows[i].FindControl("lblMonth");

                TextBox txtNewCustomerTarget = (TextBox)gvVisitTarget.Rows[i].FindControl("txtNewCustomerTarget");
                TextBox txtProspectCustomerTarget = (TextBox)gvVisitTarget.Rows[i].FindControl("txtProspectCustomerTarget");
                TextBox txtExistCustomerTarget = (TextBox)gvVisitTarget.Rows[i].FindControl("txtExistCustomerTarget");

                VisitTarget.Add(new PVisitTarget()
                {
                    VisitTargetID = Convert.ToInt64(lblVisitTargetID.Text),
                    Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(lblDealerID.Text) },
                    Employee = new PDMS_DealerEmployee() { DealerEmployeeID = Convert.ToInt32(lblDealerEmployeeID.Text) },
                    Year = Convert.ToInt32(lblYear.Text),
                    Month = Convert.ToInt32(lblMonth.Text),
                    NewCustomerTarget = Convert.ToInt32(txtNewCustomerTarget.Text),
                    ProspectCustomerTarget = Convert.ToInt32(txtProspectCustomerTarget.Text),
                    ExistCustomerTarget = Convert.ToInt32(txtExistCustomerTarget.Text),
                    CreatedBy = new PUser() { UserID = PSession.User.UserID }

                });
            }
            string result = new BAPI().ApiPut("ColdVisit/VisitTarget", VisitTarget);
            result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data);
            if (result == "0")
            {
                lblMessage.Text = "Visit Target is not updated successfully ";
                return;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Visit Target is updated successfully ";
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Update")
            {
                Update();
                FillVisitTarget();
            }
            else
            {
                btnEdit.Text = "Update";
                BtnSearch.Text = "Back";
                for (int i = 0; i < gvVisitTarget.Rows.Count; i++)
                {
                    TextBox txtNewCustomerTarget = (TextBox)gvVisitTarget.Rows[i].FindControl("txtNewCustomerTarget");
                    TextBox txtProspectCustomerTarget = (TextBox)gvVisitTarget.Rows[i].FindControl("txtProspectCustomerTarget");
                    TextBox txtExistCustomerTarget = (TextBox)gvVisitTarget.Rows[i].FindControl("txtExistCustomerTarget");

                    Label lblNewCustomerTargett = (Label)gvVisitTarget.Rows[i].FindControl("lblNewCustomerTarget");
                    Label lblProspectCustomerTarget = (Label)gvVisitTarget.Rows[i].FindControl("lblProspectCustomerTarget");
                    Label lblExistCustomerTarget = (Label)gvVisitTarget.Rows[i].FindControl("lblExistCustomerTarget");

                    txtNewCustomerTarget.Visible = true;
                    txtProspectCustomerTarget.Visible = true;
                    txtExistCustomerTarget.Visible = true;

                    lblNewCustomerTargett.Visible = false;
                    lblProspectCustomerTarget.Visible = false;
                    lblExistCustomerTarget.Visible = false;
                }
            }
        }


        protected void OnDataBound(object sender, EventArgs e)
        {

            //if (btnEdit.Text == "Edit")
            //{
            //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            //    TableHeaderCell cell = new TableHeaderCell();
            //    cell.Text = "";
            //    cell.ColumnSpan = 5;
            //    row.Controls.Add(cell);

            //    cell = new TableHeaderCell();
            //    cell.ColumnSpan = 4;
            //    cell.Text = "Target";
            //    row.Controls.Add(cell);

            //    cell = new TableHeaderCell();
            //    cell.ColumnSpan = 4;
            //    cell.Text = "Actual";
            //    row.Controls.Add(cell);

            //    row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            //    gvVisitTarget.HeaderRow.Parent.Controls.AddAt(0, row);
            //}
            //else
            //{
            //}
        }
    }
}
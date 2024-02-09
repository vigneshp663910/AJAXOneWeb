using Business;
using ClosedXML.Excel;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.Planning
{
    public partial class DealerMissionPlanning : BasePage
    {
        public List<PDealerMissionPlanning> VT
        {
            get
            {
                if (Session["PDealerMissionPlanning"] == null)
                {
                    Session["PDealerMissionPlanning"] = new List<PDealerMissionPlanning>();
                }
                return (List<PDealerMissionPlanning>)Session["PDealerMissionPlanning"];
            }
            set
            {
                Session["PDealerMissionPlanning"] = value;
            }
        }
        public override SubModule SubModuleName { get { return SubModule.ViewMaster_DealerMissionPlanning; } }
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Planning » Dealer Mission Planning');</script>");
            if (!IsPostBack)
            {
                FillYearAndMonth();
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithDisplayName", "DID", true, "All Dealer");
                new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
            }
        }

        void FillYearAndMonth()
        {
            ddlYear.Items.Insert(0, new ListItem("All", "0"));
            for (int i = 2023; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Insert(i + 1 - 2023, new ListItem(i.ToString(), i.ToString()));
            }

            ddlMonth.Items.Insert(0, new ListItem("All", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Insert(i, new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), i.ToString()));
            }
        }
 
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
             
                FillVisitTarget(); 
        }




        protected void gvVisitTarget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvMissionPlanning.PageIndex = e.NewPageIndex;
            FillVisitTarget();
            gvMissionPlanning.DataBind();
        }


        protected void ibtnVTArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMissionPlanning.PageIndex > 0)
            {
                gvMissionPlanning.PageIndex = gvMissionPlanning.PageIndex - 1;
                VTBind(gvMissionPlanning, lblRowCountV, VT);
            }
        }
        protected void ibtnVTArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMissionPlanning.PageCount > gvMissionPlanning.PageIndex)
            {
                gvMissionPlanning.PageIndex = gvMissionPlanning.PageIndex + 1;
                VTBind(gvMissionPlanning, lblRowCountV, VT);
            }
        }

        void VTBind(GridView gv, Label lbl, List<PDealerMissionPlanning> VT)
        {
            gv.DataSource = VT;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + VT.Count;
        }


        void FillVisitTarget()
        {  
            int? Year = ddlYear.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlYear.SelectedValue);
            int? Month = ddlMonth.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? ProductTypeID = ddlProductType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductType.SelectedValue);
            VT = new BLead().GetDealerMissionPlanning(Year, Month, DealerID, ProductTypeID);
            gvMissionPlanning.DataSource = VT;
            gvMissionPlanning.DataBind();

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
                lblRowCountV.Text = (((gvMissionPlanning.PageIndex) * gvMissionPlanning.PageSize) + 1) + " - " + (((gvMissionPlanning.PageIndex) * gvMissionPlanning.PageSize) + gvMissionPlanning.Rows.Count) + " of " + VT.Count;
            }

        }


        void Update()
        {
            List<PDealerMissionPlanning> VisitTarget = new List<PDealerMissionPlanning>();
            for (int i = 0; i < gvMissionPlanning.Rows.Count; i++)
            {
                //  Label lblVisitTargetID = (Label)gvMissionPlanning.Rows[i].FindControl("lblVisitTargetID"); 

                Label lblDealerID = (Label)gvMissionPlanning.Rows[i].FindControl("lblDealerID");
                Label lblYear = (Label)gvMissionPlanning.Rows[i].FindControl("lblYear");
                Label lblMonth = (Label)gvMissionPlanning.Rows[i].FindControl("lblMonth");

                TextBox txtBillingPlan = (TextBox)gvMissionPlanning.Rows[i].FindControl("txtBillingPlan");
                TextBox txtBillingRevenuePlan = (TextBox)gvMissionPlanning.Rows[i].FindControl("txtBillingRevenuePlan");
                TextBox txtRetailPlan = (TextBox)gvMissionPlanning.Rows[i].FindControl("txtRetailPlan");
                TextBox txtLeadPlan = (TextBox)gvMissionPlanning.Rows[i].FindControl("txtLeadPlan");
                TextBox txtQuotationPlan = (TextBox)gvMissionPlanning.Rows[i].FindControl("txtQuotationPlan");
                TextBox txtPartsQuotationPlan = (TextBox)gvMissionPlanning.Rows[i].FindControl("txtPartsQuotationPlan");
                TextBox txtPartsRetailPlan = (TextBox)gvMissionPlanning.Rows[i].FindControl("txtPartsRetailPlan");
                TextBox txtPartsBillingPlan = (TextBox)gvMissionPlanning.Rows[i].FindControl("txtPartsBillingPlan"); 

                VisitTarget.Add(new PDealerMissionPlanning()
                {
                    Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(lblDealerID.Text) },
                    Year = Convert.ToInt32(lblYear.Text),
                    Month = DateTime.ParseExact(lblMonth.Text, "MMM", CultureInfo.CurrentCulture).Month,
                    BillingPlan = Convert.ToInt32(txtBillingPlan.Text),
                    BillingRevenuePlan = Convert.ToInt32(txtBillingRevenuePlan.Text),
                    RetailPlan = Convert.ToInt32(txtRetailPlan.Text),
                    LeadPlan = Convert.ToInt32(txtLeadPlan.Text),
                    QuotationPlan = Convert.ToInt32(txtQuotationPlan.Text),
                    PartsQuotationPlan = Convert.ToInt32(txtPartsQuotationPlan.Text),
                    PartsRetailPlan = Convert.ToInt32(txtPartsRetailPlan.Text),
                    PartsBillingPlan = Convert.ToInt32(txtPartsBillingPlan.Text),
                    CreatedBy = new PUser() { UserID = PSession.User.UserID }

                });
            }
            string result = new BAPI().ApiPut("Lead/DealerMissionPlanning", VisitTarget);
            result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data);
            if (result == "0")
            {
                lblMessage.Text = "Visit Target is not updated successfully ";
                return;
            }
            else
            { 
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Visit Target is updated successfully ";
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtnMissionPlanningEdit = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(lnkBtnMissionPlanningEdit.NamingContainer);

            Label lblBillingPlan = (Label)row.FindControl("lblBillingPlan");
            Label lblBillingRevenuePlan = (Label)row.FindControl("lblBillingRevenuePlan");
            Label lblRetailPlan = (Label)row.FindControl("lblRetailPlan");
            Label lblLeadPlan = (Label)row.FindControl("lblLeadPlan");
            Label lblLeadConversionPlan = (Label)row.FindControl("lblLeadConversionPlan");
            Label lblQuotationPlan = (Label)row.FindControl("lblQuotationPlan");
            Label lblQuotationConversionPlan = (Label)row.FindControl("lblQuotationConversionPlan");
            Label lblPartsQuotationPlan = (Label)row.FindControl("lblPartsQuotationPlan");
            Label lblPartsQuotationConversionPlan = (Label)row.FindControl("lblPartsQuotationConversionPlan");
            Label lblPartsRetailPlan = (Label)row.FindControl("lblPartsRetailPlan");
            Label lblPartsBillingPlan = (Label)row.FindControl("lblPartsBillingPlan");

            TextBox txtBillingPlan = (TextBox)row.FindControl("txtBillingPlan");
            TextBox txtBillingRevenuePlan = (TextBox)row.FindControl("txtBillingRevenuePlan");
            TextBox txtRetailPlan = (TextBox)row.FindControl("txtRetailPlan");
            TextBox txtLeadPlan = (TextBox)row.FindControl("txtLeadPlan");
            TextBox txtLeadConversionPlan = (TextBox)row.FindControl("txtLeadConversionPlan");
            TextBox txtQuotationPlan = (TextBox)row.FindControl("txtQuotationPlan");
            TextBox txtQuotationConversionPlan = (TextBox)row.FindControl("txtQuotationConversionPlan");
            TextBox txtPartsQuotationPlan = (TextBox)row.FindControl("txtPartsQuotationPlan");
            TextBox txtPartsQuotationConversionPlan = (TextBox)row.FindControl("txtPartsQuotationConversionPlan");
            TextBox txtPartsRetailPlan = (TextBox)row.FindControl("txtPartsRetailPlan");
            TextBox txtPartsBillingPlan = (TextBox)row.FindControl("txtPartsBillingPlan");

            lblBillingPlan.Visible = false;
            lblBillingRevenuePlan.Visible = false;
            lblRetailPlan.Visible = false;
            lblLeadPlan.Visible = false;
            lblLeadConversionPlan.Visible = false;
            lblQuotationPlan.Visible = false;
            lblQuotationConversionPlan.Visible = false;
            lblPartsQuotationPlan.Visible = false;
            lblPartsQuotationConversionPlan.Visible = false;
            lblPartsRetailPlan.Visible = false;
            lblPartsBillingPlan.Visible = false;

            txtBillingPlan.Visible = true;
            txtBillingRevenuePlan.Visible = true;
            txtRetailPlan.Visible = true;
            txtLeadPlan.Visible = true;
            txtLeadConversionPlan.Visible = true;
            txtQuotationPlan.Visible = true;
            txtQuotationConversionPlan.Visible = true;
            txtPartsQuotationPlan.Visible = true;
            txtPartsQuotationConversionPlan.Visible = true;
            txtPartsRetailPlan.Visible = true;
            txtPartsBillingPlan.Visible = true;

            txtBillingPlan.Text = lblBillingPlan.Text;
            txtBillingRevenuePlan.Text = lblBillingRevenuePlan.Text;
            txtRetailPlan.Text = lblRetailPlan.Text;
            txtLeadPlan.Text = lblLeadPlan.Text;
            txtLeadConversionPlan.Text = lblLeadConversionPlan.Text;
            txtQuotationPlan.Text = lblQuotationPlan.Text;
            txtQuotationConversionPlan.Text = lblQuotationConversionPlan.Text;
            txtPartsQuotationPlan.Text = lblPartsQuotationPlan.Text;
            txtPartsQuotationConversionPlan.Text = lblPartsQuotationConversionPlan.Text;
            txtPartsRetailPlan.Text = lblPartsRetailPlan.Text;
            txtPartsBillingPlan.Text = lblPartsBillingPlan.Text;

            Button BtnUpdateMissionPlanning = (Button)row.FindControl("BtnUpdateMissionPlanning");
            Button btnBack = (Button)row.FindControl("btnBack");
            BtnUpdateMissionPlanning.Visible = true;
            btnBack.Visible = true;
            lnkBtnMissionPlanningEdit.Visible = false;
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

         
        protected void BtnUpdateMissionPlanning_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)(btn.NamingContainer);

            Label lblBillingPlan = (Label)row.FindControl("lblBillingPlan");
            Label lblBillingRevenuePlan = (Label)row.FindControl("lblBillingRevenuePlan");
            Label lblRetailPlan = (Label)row.FindControl("lblRetailPlan");
            Label lblLeadPlan = (Label)row.FindControl("lblLeadPlan");
            Label lblLeadConversionPlan = (Label)row.FindControl("lblLeadConversionPlan");
            Label lblQuotationPlan = (Label)row.FindControl("lblQuotationPlan");
            Label lblQuotationConversionPlan = (Label)row.FindControl("lblQuotationConversionPlan");
            Label lblPartsQuotationPlan = (Label)row.FindControl("lblPartsQuotationPlan");
            Label lblPartsQuotationConversionPlan = (Label)row.FindControl("lblPartsQuotationConversionPlan");
            Label lblPartsRetailPlan = (Label)row.FindControl("lblPartsRetailPlan");
            Label lblPartsBillingPlan = (Label)row.FindControl("lblPartsBillingPlan");

            TextBox txtBillingPlan = (TextBox)row.FindControl("txtBillingPlan");
            TextBox txtBillingRevenuePlan = (TextBox)row.FindControl("txtBillingRevenuePlan");
            TextBox txtRetailPlan = (TextBox)row.FindControl("txtRetailPlan");
            TextBox txtLeadPlan = (TextBox)row.FindControl("txtLeadPlan");
            TextBox txtLeadConversionPlan = (TextBox)row.FindControl("txtLeadConversionPlan");
            TextBox txtQuotationPlan = (TextBox)row.FindControl("txtQuotationPlan");
            TextBox txtQuotationConversionPlan = (TextBox)row.FindControl("txtQuotationConversionPlan");
            TextBox txtPartsQuotationPlan = (TextBox)row.FindControl("txtPartsQuotationPlan");
            TextBox txtPartsQuotationConversionPlan = (TextBox)row.FindControl("txtPartsQuotationConversionPlan");
            TextBox txtPartsRetailPlan = (TextBox)row.FindControl("txtPartsRetailPlan");
            TextBox txtPartsBillingPlan = (TextBox)row.FindControl("txtPartsBillingPlan");

            if (btn.ID == "btnBack")
            {
                lblBillingPlan.Visible = true;
                lblBillingRevenuePlan.Visible = true;
                lblRetailPlan.Visible = true;
                lblLeadPlan.Visible = true;
                lblLeadConversionPlan.Visible = true;
                lblQuotationPlan.Visible = true;
                lblQuotationConversionPlan.Visible = true;
                lblPartsQuotationPlan.Visible = true;
                lblPartsQuotationConversionPlan.Visible = true;
                lblPartsRetailPlan.Visible = true;
                lblPartsBillingPlan.Visible = true;

                txtBillingPlan.Visible = false;
                txtBillingRevenuePlan.Visible = false;
                txtRetailPlan.Visible = false;
                txtLeadPlan.Visible = false;
                txtLeadConversionPlan.Visible = false;
                txtQuotationPlan.Visible = false;
                txtQuotationConversionPlan.Visible = false;
                txtPartsQuotationConversionPlan.Visible = false;
                txtPartsQuotationPlan.Visible = false;
                txtPartsRetailPlan.Visible = false;
                txtPartsBillingPlan.Visible = false;

                Button BtnUpdateMissionPlanning = (Button)row.FindControl("BtnUpdateMissionPlanning");
                Button btnBack = (Button)row.FindControl("btnBack");
                LinkButton lnkBtnMissionPlanningEdit = (LinkButton)row.FindControl("lnkBtnMissionPlanningEdit");
                BtnUpdateMissionPlanning.Visible = false;
                btnBack.Visible = false;
                lnkBtnMissionPlanningEdit.Visible = true;

            }
            else
            {
                Label lblDealerID = (Label)row.FindControl("lblDealerID");

                Label lblProductTypeID = (Label)row.FindControl("lblProductTypeID");
                Label lblYear = (Label)row.FindControl("lblYear");
                Label lblMonth = (Label)row.FindControl("lblMonth");
                List<PDealerMissionPlanning> Plannings = new List<PDealerMissionPlanning>();

                PDealerMissionPlanning Planning =
              new PDealerMissionPlanning()
              {
                  Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(lblDealerID.Text) },
                  ProductType = new PProductType() { ProductTypeID = Convert.ToInt32(lblProductTypeID.Text) },
                  Year = Convert.ToInt32(lblYear.Text),
                  Month = DateTime.ParseExact(lblMonth.Text, "MMM", CultureInfo.CurrentCulture).Month,
                  BillingPlan = string.IsNullOrEmpty(txtBillingPlan.Text.Trim())?(int?)null: Convert.ToInt32(txtBillingPlan.Text.Trim()),
                  BillingRevenuePlan = string.IsNullOrEmpty(txtBillingRevenuePlan.Text.Trim()) ? (int?)null : Convert.ToInt32(txtBillingRevenuePlan.Text.Trim()),
                  RetailPlan = string.IsNullOrEmpty(txtRetailPlan.Text.Trim()) ? (int?)null : Convert.ToInt32(txtRetailPlan.Text.Trim()),
                  LeadPlan = string.IsNullOrEmpty(txtLeadPlan.Text.Trim()) ? (int?)null : Convert.ToInt32(txtLeadPlan.Text.Trim()),
                  LeadConversionPlan = string.IsNullOrEmpty(txtBillingPlan.Text.Trim()) ? (int?)null : Convert.ToInt32(txtLeadConversionPlan.Text.Trim()),
                  QuotationPlan = string.IsNullOrEmpty(txtQuotationPlan.Text.Trim()) ? (int?)null : Convert.ToInt32(txtQuotationPlan.Text.Trim()),
                  QuotationConversionPlan = string.IsNullOrEmpty(txtQuotationConversionPlan.Text.Trim()) ? (int?)null : Convert.ToInt32(txtQuotationConversionPlan.Text.Trim()),
                  PartsQuotationPlan = string.IsNullOrEmpty(txtPartsQuotationPlan.Text.Trim()) ? (int?)null : Convert.ToInt32(txtPartsQuotationPlan.Text.Trim()),
                  PartsQuotationConversionPlan = string.IsNullOrEmpty(txtPartsQuotationConversionPlan.Text.Trim()) ? (int?)null : Convert.ToInt32(txtPartsQuotationConversionPlan.Text.Trim()),
                  PartsRetailPlan = string.IsNullOrEmpty(txtPartsRetailPlan.Text.Trim()) ? (int?)null : Convert.ToInt32(txtPartsRetailPlan.Text.Trim()),
                  PartsBillingPlan = string.IsNullOrEmpty(txtPartsBillingPlan.Text.Trim()) ? (int?)null : Convert.ToInt32(txtPartsBillingPlan.Text.Trim()),
                  CreatedBy = new PUser() { UserID = PSession.User.UserID }
              };
                Plannings.Add(Planning);

                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/DealerMissionPlanning", Plannings));

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

                    lblBillingPlan.Visible = true;
                    lblBillingRevenuePlan.Visible = true;
                    lblRetailPlan.Visible = true;
                    lblLeadPlan.Visible = true;
                    lblLeadConversionPlan.Visible = true;
                    lblQuotationPlan.Visible = true;
                    lblQuotationConversionPlan.Visible = true;
                    lblPartsQuotationPlan.Visible = true;
                    lblPartsQuotationConversionPlan.Visible = true;
                    lblPartsRetailPlan.Visible = true;
                    lblPartsBillingPlan.Visible = true;

                    txtBillingPlan.Visible = false;
                    txtBillingRevenuePlan.Visible = false;
                    txtRetailPlan.Visible = false;
                    txtLeadPlan.Visible = false;
                    txtLeadConversionPlan.Visible = false;
                    txtQuotationPlan.Visible = false;
                    txtQuotationConversionPlan.Visible = false;
                    txtPartsQuotationConversionPlan.Visible = false;
                    txtPartsQuotationPlan.Visible = false;
                    txtPartsRetailPlan.Visible = false;
                    txtPartsBillingPlan.Visible = false;

                    lblBillingPlan.Text = txtBillingPlan.Text;
                    lblBillingRevenuePlan.Text = txtBillingRevenuePlan.Text;
                    lblRetailPlan.Text = txtRetailPlan.Text;
                    lblLeadPlan.Text = txtLeadPlan.Text;
                    lblLeadConversionPlan.Text = txtLeadConversionPlan.Text;
                    lblQuotationPlan.Text = txtQuotationPlan.Text;
                    lblQuotationConversionPlan.Text = txtQuotationConversionPlan.Text;
                    lblPartsQuotationPlan.Text = txtPartsQuotationPlan.Text;
                    lblPartsQuotationConversionPlan.Text = txtPartsQuotationConversionPlan.Text;
                    lblPartsRetailPlan.Text = txtPartsRetailPlan.Text;
                    lblPartsBillingPlan.Text = txtPartsBillingPlan.Text;

                    Button BtnUpdateMissionPlanning = (Button)row.FindControl("BtnUpdateMissionPlanning");
                    Button btnBack = (Button)row.FindControl("btnBack");
                    LinkButton lnkBtnMissionPlanningEdit = (LinkButton)row.FindControl("lnkBtnMissionPlanningEdit");
                    BtnUpdateMissionPlanning.Visible = false;
                    btnBack.Visible = false;
                    lnkBtnMissionPlanningEdit.Visible = true;
                }
            } 
        }


        public DataTable dtDMP
        {
            get
            {
                if (ViewState["DTDealerOperatorDetailsUpload"] == null)
                {
                    ViewState["DTDealerOperatorDetailsUpload"] = new DataTable();
                }
                return (DataTable)ViewState["DTDealerOperatorDetailsUpload"];
            }
            set
            {
                ViewState["DTDealerOperatorDetailsUpload"] = value;
            }
        } 
        private Boolean FillUpload()
        {
            Boolean Success = true;

            if (fileUpload.HasFile == true)
            {
                using (XLWorkbook workBook = new XLWorkbook(fileUpload.PostedFile.InputStream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Create a new DataTable.
                    dtDMP = new DataTable();

                    //Loop through the Worksheet rows.
                    int sno = 0;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        sno += 1;
                        //Use the first row to add columns to DataTable.
                        if (sno == 1)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                dtDMP.Columns.Add(cell.Value.ToString());
                            }
                        }
                        else if (sno > 1)
                        {
                            //Add rows to DataTable.
                            dtDMP.Rows.Add();
                            int i = 0;


                            foreach (IXLCell cell in row.Cells())
                            {
                                dtDMP.Rows[dtDMP.Rows.Count - 1][i] = cell.Value.ToString();
                                i++;
                            }
                        }
                    }
                    List<PDMS_Dealer> pDMS_Dealers = new BDMS_Dealer().GetDealer(null, null, null, null);
                    List<PProductType> ProductType = new BDMS_Master().GetProductType(null, null);
                    foreach (DataRow dr in dtDMP.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr[0].ToString()))
                        {
                            bool containsItem = pDMS_Dealers.Any(item => item.DealerCode == dr[2].ToString());
                            if (!containsItem)
                            {
                                lblMessage.Text = "Please Check the DealerCode : " + dr[2].ToString() + " Not Available in the Dealer List...!";
                                lblMessage.ForeColor = Color.Red;
                                Success = false;
                                return Success;
                            }
                            bool containsItemState = ProductType.Any(item => item.ProductType.ToUpper() == dr[3].ToString().ToUpper());
                            if (!containsItemState)
                            {
                                lblMessage.Text = "Please Check the Product Type : " + dr[3].ToString() + " Not Available in the Product Type List...!";
                                lblMessage.ForeColor = Color.Red;
                                Success = false;
                                return Success;
                            }
                        }
                    }

                    var duplicates = dtDMP.AsEnumerable().GroupBy(i => new { Year = i.Field<string>("Year"), Month = i.Field<string>("Month"), DealerCode = i.Field<string>("Dealer Code"), ProductType = i.Field<string>("Product Type") }).Where(g => g.Count() > 1).Select(g => new { g.Key.Year, g.Key.Month, g.Key.DealerCode, g.Key.ProductType }).ToList();
                    if (duplicates.Count > 0)
                    {
                        lblMessage.Text = "Duplicate Records Found : " + duplicates.Count + "...!";
                        lblMessage.ForeColor = Color.Red;
                        Success = false;
                        return Success;
                    }

                    if (dtDMP.Rows.Count > 0)
                    {
                        GVUpload.DataSource = dtDMP;
                        GVUpload.DataBind();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Please Upload the File...!";
                lblMessage.ForeColor = Color.Red;
                Success = false;
                return Success;
            }

            return Success;
        }
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (BtnUpload.Text == "Submit")
                {

                    string BillingPlan = "Billing Plan";
                    string BillingRevenuePlan = "Billing Revenue Plan";
                    string RetailPlan = "Retail Plan";
                    string LeadPlan = "Lead Plan";
                    string LeadConversionPlan = "Lead Conversion Plan";
                    string QuotationPlan = "Quotation Plan";
                    string QuotationConversionPlan = "Quotation Conversion Plan";
                    string PartsQuotationPlan = "Parts Quotation Plan";
                    string PartsQuotationConversionPlan = "Parts Quotation Conversion Plan";
                    string PartsRetailPlan = "Parts Retail Plan";
                    string PartsBillingPlan = "Parts Billing Plan";

                    List<PDMS_Dealer> pDMS_Dealers = new BDMS_Dealer().GetDealer(null, null, null, null);
                    List<PProductType> ProductType = new BDMS_Master().GetProductType(null, null);

                    List<PDealerMissionPlanning> Plannings = new List<PDealerMissionPlanning>();

                    PDealerMissionPlanning Planning = null;
                    if (dtDMP.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtDMP.Rows)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(dr["Dealer Code"])))
                            {
                                continue;
                            }
                            var DealerID = from D in pDMS_Dealers where D.DealerCode == Convert.ToString(dr["Dealer Code"]) select D.DealerID;
                            var ProductTypeID = from D in ProductType where D.ProductType == Convert.ToString(dr["Product Type"]) select D.ProductTypeID;
                            Planning = new PDealerMissionPlanning()
                            {
                                Dealer = new PDMS_Dealer() { DealerID = DealerID.First() },
                                ProductType = new PProductType() { ProductTypeID = ProductTypeID.First() },
                                Year = Convert.ToInt32(dr["Year"]),
                                Month = Convert.ToInt32(dr["Month"]),
                                BillingPlan = !dtDMP.Columns.Contains(BillingPlan) ? (int?)null : dr[BillingPlan] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[BillingPlan]),
                                BillingRevenuePlan = !dtDMP.Columns.Contains(BillingRevenuePlan) ? (int?)null : dr[BillingRevenuePlan] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[BillingRevenuePlan]),
                                RetailPlan = !dtDMP.Columns.Contains(RetailPlan) ? (int?)null : dr[RetailPlan] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[RetailPlan]),
                                LeadPlan = !dtDMP.Columns.Contains(LeadPlan) ? (int?)null : dr[LeadPlan] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[LeadPlan]),
                                LeadConversionPlan = !dtDMP.Columns.Contains(LeadConversionPlan) ? (int?)null : dr[LeadConversionPlan] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[LeadConversionPlan]),
                                QuotationPlan = !dtDMP.Columns.Contains(QuotationPlan) ? (int?)null : dr[QuotationPlan] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[QuotationPlan]),
                                QuotationConversionPlan = !dtDMP.Columns.Contains(QuotationConversionPlan) ? (int?)null : dr[QuotationConversionPlan] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[QuotationConversionPlan]),
                                PartsQuotationPlan = !dtDMP.Columns.Contains(PartsQuotationPlan) ? (int?)null : dr[PartsQuotationPlan] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[PartsQuotationPlan]),
                                PartsQuotationConversionPlan = !dtDMP.Columns.Contains(PartsQuotationConversionPlan) ? (int?)null : dr[PartsQuotationConversionPlan] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[PartsQuotationConversionPlan]),
                                PartsRetailPlan = !dtDMP.Columns.Contains(PartsRetailPlan) ? (int?)null : dr[PartsRetailPlan] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[PartsRetailPlan]),
                                PartsBillingPlan = !dtDMP.Columns.Contains(PartsBillingPlan) ? (int?)null : dr[PartsBillingPlan] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[PartsBillingPlan]),
                                CreatedBy = new PUser() { UserID = PSession.User.UserID }
                            };
                            Plannings.Add(Planning);
                        }
                        PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/DealerMissionPlanning", Plannings));

                        if (result.Status == PApplication.Failure)
                        {
                            lblMessage.Text = "Dealer Mission Planning is not updated successfully ";
                            return;
                        }
                        else
                        { 
                            lblMessage.ForeColor = Color.Green;
                            lblMessage.Text = "Dealer Mission Planning is updated successfully ";
                            dtDMP = new DataTable();
                            GVUpload.DataSource = dtDMP;
                            GVUpload.DataBind();
                        }
                    }

                    BtnUpload.Text = "Upload";
                }
                else
                {
                    Boolean Result = false;
                    Result = FillUpload();
                    if (Result)
                    {
                        BtnUpload.Text = "Submit";
                        fileUpload.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            //divDealerOperatorDetailsList.Visible = true;
            //divDealerOperatorDetailsView.Visible = false;
        }
        protected void BtnFUpload_Click(object sender, EventArgs e)
        {
            FldSearch.Visible = false;
            DivReport.Visible = false;
            FldUpload.Visible = true;
            fileUpload.Visible = true;
            dtDMP = new DataTable();
            GVUpload.DataSource = dtDMP;
            GVUpload.DataBind();
            BtnUpload.Text = "Upload";
        }
        protected void BtnFBack_Click(object sender, EventArgs e)
        {
            FldSearch.Visible = true;
            DivReport.Visible = true;
            FldUpload.Visible = false;
        }
    }
}
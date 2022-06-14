using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMarketing
{
    public partial class RollingPlanModelWise : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        BDMS_Planning oPlan = new BDMS_Planning();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalPlan.SelectedDate = DateTime.Now;


                btnSubmit.Visible = false;
                List<PDealer> Dealer = new BDMS_Activity().GetDealerByUserID(PSession.User.UserID);
                ddlDealer.DataTextField = "CodeWithName"; ddlDealer.DataValueField = "DID"; ddlDealer.DataSource = Dealer; ddlDealer.DataBind();
                if (ddlDealer.Items.Count > 1) ddlDealer.Items.Insert(0, new ListItem("Select", "0"));
                oPlan.BindYear(ddlYear);
                oPlan.BindMonths(ddlMonth);
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlMonth_SelectedIndexChanged(ddlMonth, null);
                //CDDProduct.ContextKey = "";
                txtNo.Text = "1";
                Session["dtPlan"] = null;


                FillPlant();

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPlan = new DataTable();
                dtPlan.Columns.Add("Product"); dtPlan.Columns.Add("ProductID");
                dtPlan.Columns.Add("Model"); dtPlan.Columns.Add("ModelID");
                dtPlan.Columns.Add("PlanDate"); dtPlan.Columns.Add("PlanNo"); dtPlan.Columns.Add("Tag");
                if (Session["dtPlan"] != null)
                {
                    dtPlan = Session["dtPlan"] as DataTable;
                }

                DataRow dr = dtPlan.NewRow();
                if (dtPlan.Select("ModelID=" + ddlModel.SelectedItem.Value + " And PlanDate='" + txtDate.Text + "'").Length == 0)
                {

                    dr["Product"] = ddlProduct.SelectedItem.Text;
                    dr["ProductID"] = ddlProduct.SelectedItem.Value;
                    dr["Model"] = ddlModel.SelectedItem.Text;
                    dr["ModelID"] = ddlModel.SelectedItem.Value;
                    dr["PlanDate"] = txtDate.Text;
                    dr["PlanNo"] = txtNo.Text;
                    dr["Tag"] = "I";
                    dtPlan.Rows.Add(dr);

                }
                else
                {
                    dr = ((DataRow)dtPlan.Select("ModelID=" + ddlModel.SelectedItem.Value + " And PlanDate='" + txtDate.Text + "'").GetValue(0));
                    dr["Tag"] = "I";
                    dr["PlanDate"] = txtDate.Text;
                    dr["PlanNo"] = txtNo.Text;
                    dtPlan.AcceptChanges();
                }
                Session["dtPlan"] = dtPlan;
                gvPlan.DataSource = dtPlan;
                gvPlan.DataBind();
                CalPlan.DataBind();
                txtDate.Text = ""; txtNo.Text = "1";
                //CDDModel.SelectedValue = null;
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                return;
            }
        }
        [WebMethod]
        public static AjaxControlToolkit.CascadingDropDownNameValue[] GetProduct(string knownCategoryValues, string category, string contextKey)
        {
            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            DataTable dt = new BDMS_Planning().GetAllProducts();
            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();

            foreach (DataRow dRow in dt.Rows)
            {
                string _ID = dRow["DivisionID"].ToString();
                string _Text = dRow["DivisionDescription"].ToString();
                if (_ID != "0")
                {
                    cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(_Text, _ID, contextKey == _ID));
                }
            }
            return cascadingValues.ToArray();
        }
        [WebMethod]
        public static AjaxControlToolkit.CascadingDropDownNameValue[] GetModels(string knownCategoryValues, string category, string contextKey)
        {
            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            string _ProductID = categoryValues["ProductID"];
            DataTable dt = new BDMS_Planning().GetModelsByProductID(Convert.ToInt32(_ProductID));
            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();

            foreach (DataRow dRow in dt.Rows)
            {
                string _ID = dRow["ModelID"].ToString();
                string _Text = dRow["ModelDescription"].ToString();
                if (_ID != "0")
                {
                    cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(_Text, _ID));
                }
            }
            return cascadingValues.ToArray();
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["dtPlan"] = null;
            gvPlan.DataSource = null;
            gvPlan.DataBind();
            BindPlan();
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                GridViewRow gvRow = lnk.NamingContainer as GridViewRow;
                HiddenField hdnModelID = gvRow.FindControl("hdnModelID") as HiddenField;
                Label lblDate = gvRow.FindControl("lblDate") as Label;
                DataTable dtPlan = Session["dtPlan"] as DataTable;
                DataRow dr = ((DataRow)dtPlan.Select("ModelID='" + hdnModelID.Value + "' And PlanDate='" + lblDate.Text + "'").GetValue(0));
                dr["Tag"] = "D";
                dtPlan.AcceptChanges();
                //dtPlan.Rows.Remove(((DataRow)dtPlan.Select("ModelID='" + hdnModelID.Value + "'").GetValue(0)));
                Session["dtPlan"] = dtPlan;
                gvPlan.DataSource = dtPlan; gvPlan.DataBind();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                return;
            }
        }

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["dtPlan"] = null;
            gvPlan.DataSource = null;
            gvPlan.DataBind();
            BindPlan();
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["dtPlan"] = null;
            gvPlan.DataSource = null;
            gvPlan.DataBind();
            BindGrid();
            BindPlan();
        }
        protected void BindGrid()
        {
            DateTime datStart = DateTime.Parse("01-" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedValue);
            DateTime datEnd = datStart.AddMonths(1).AddDays(-1);
            CalDate.StartDate = datStart;
            CalDate.EndDate = datEnd;
            CalDate.SelectedDate = datStart;
            Session["dtPlan"] = null;
            gvPlan.DataSource = null;
            gvPlan.DataBind();
            Session["dtPlan"] = null;
            gvPlan.DataSource = null;
            gvPlan.DataBind();
            btnSubmit.Visible = false;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["dtPlan"] != null)
                {
                    DataTable dt = Session["dtPlan"] as DataTable;
                    foreach (DataRow dr in dt.Rows)
                    {
                        int ModelID = Convert.ToInt32(dr["ModelID"]);
                        int DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
                        DateTime PlanDate = Convert.ToDateTime(dr["PlanDate"]);
                        int PlanNo = Convert.ToInt32(dr["PlanNo"]);
                        string sTag = dr["Tag"].ToString();
                        oPlan.SavePlan(DealerID, PlanDate, ModelID, PlanNo, PSession.User.UserID, sTag);
                    }
                }
                lblMessage.Text = "Saved Successfully...!";
                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                return;
            }
        }

        protected void gvPlan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Visible = ((HiddenField)e.Row.FindControl("hdnTag")).Value != "D";
            }
        }
        static DataTable dtCalPlan = new DataTable();
        protected void BindPlan()
        {
            try
            {
                DateTime dat = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 15);
                DateTime curMonthDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                DataTable dtCalPlan = oPlan.GetPlanForDealer(Convert.ToInt32(ddlDealer.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                ViewState["CalPlan"] = dtCalPlan;
                Session["dtPlan"] = dtCalPlan;
                gvPlan.DataSource = dtCalPlan;
                gvPlan.DataBind();
                int MonthDiff = ((curMonthDate.Year - dat.Year) * 12) + curMonthDate.Month - dat.Month;
                CalPlan.SelectedDate = dat;
                CalPlan.VisibleDate = dat;
                CalPlan.DataBind();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                return;
            }
        }

        protected void CalPlan_DayRender(object sender, DayRenderEventArgs e)
        {
            Label label1 = new Label();
            dtCalPlan = Session["dtPlan"] as DataTable;
            if (dtCalPlan != null && dtCalPlan.Rows.Count > 0)
            {
                if (dtCalPlan.Select("PlanDate='" + e.Day.Date.ToString("dd-MMM-yyyy") + "'").Length > 0)
                {
                    Literal literal1 = new Literal();
                    literal1.Text = "<br/><br/>";
                    e.Cell.Controls.Add(literal1);
                    string sText = "<table style='width:100%'>";
                    foreach (DataRow dr in dtCalPlan.Select("PlanDate='" + e.Day.Date.ToString("dd-MMM-yyyy") + "'"))
                    {
                        sText += "<tr><td style='text-align:left;width:80%'>" + dr["Model"].ToString() + "</td><td style='width:20%;text-align:right'>" + dr["PlanNo"].ToString() + "</td></tr>";
                    }
                    sText += "</table>";
                    label1.Text = sText;
                    label1.CssClass = "lblDayPlan";
                    e.Cell.Controls.Add(label1);
                    e.Cell.CssClass = "calCell";
                    //e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#dff8f8");
                }
            }
        }

        protected void gvPlan_DataBound(object sender, EventArgs e)
        {
            btnSubmit.Visible = gvPlan.Rows.Count > 0;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                int? DealerID = null, Month = null, Year = null;
                if (ddlDealer.SelectedValue != "0")
                {
                    DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
                }
                if (ddlMonth.SelectedValue != "0")
                {
                    Month = Convert.ToInt32(ddlMonth.SelectedValue);
                }
                if (ddlYear.SelectedValue != "0")
                {
                    Year = Convert.ToInt32(ddlYear.SelectedValue);
                }
                DataTable dtCalPlan = oPlan.GetPlanForDealer(DealerID, Month, Year);
                dtCalPlan.Columns.Remove("ModelID");
                dtCalPlan.Columns.Remove("Tag");
                if (dtCalPlan.Rows.Count > 0)
                {
                    DataRow dr = dtCalPlan.NewRow();
                    dr["D. Code"] = dtCalPlan.Rows[0]["D. Code"].ToString();
                    dr["D. Name"] = dtCalPlan.Rows[0]["D. Name"].ToString();
                    dr["D. Location"] = dtCalPlan.Rows[0]["D. Location"].ToString();
                    dr["Product"] = "Total";
                    dr["PlanNo"] = dtCalPlan.Compute("SUM(PlanNo)", "").ToString();
                    dtCalPlan.Rows.Add(dr);
                }
                else
                {
                    lblMessage.Text = "No Data Available..!";
                    lblMessage.Visible = true;
                    return;
                }
                GridView gvExcel = new GridView();
                gvExcel.DataSource = dtCalPlan;
                gvExcel.DataBind();
                gvExcel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                gvExcel.Rows[gvExcel.Rows.Count - 1].Font.Bold = true;
                gvExcel.Rows[gvExcel.Rows.Count - 1].BackColor = System.Drawing.Color.LightGray;
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvExcel.RenderControl(hw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "";
                Response.AppendHeader("content-disposition", "attachment;filename=\"" + ddlMonth.SelectedItem.Text + " Month Plan.xls\"");

                Response.Write(sw.ToString());
                Response.End();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                return;
            }
        }


        void FillPlant()
        {
            DataTable dt = new BDMS_Planning().GetAllProducts();
            ddlProduct.DataTextField = "DivisionDescription"; 
            ddlProduct.DataValueField = "DivisionID"; 
            ddlProduct.DataSource = dt;
            ddlProduct.DataBind();
            if (ddlProduct.Items.Count > 1) ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new BDMS_Planning().GetModelsByProductID(Convert.ToInt32(ddlProduct.SelectedValue));
            ddlModel.DataTextField = "ModelDescription";
            ddlModel.DataValueField = "ModelID";
            ddlModel.DataSource = dt;
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Properties;

namespace DealerManagementSystem.ViewMarketing
{
    public partial class ABPSparePart_Retail : System.Web.UI.Page
    {
        BDMS_Planning oPlan = new BDMS_Planning();
        int SPM_PlanType = 2;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (PSession.User.UserTypeID == 7)
                {
                    btnExportAll.Visible = false;
                }
                List<PDealer> Dealer = new BDMS_Activity().GetDealerByUserID(PSession.User.UserID);
                ddlDealer.DataTextField = "CodeWithName"; ddlDealer.DataValueField = "DID"; ddlDealer.DataSource = Dealer; ddlDealer.DataBind();
                if (ddlDealer.Items.Count > 1) ddlDealer.Items.Insert(0, new ListItem("Select", "0"));
                oPlan.BindFinancialYear(ddlYear);
                Session["dtPlan"] = null;
                BindGrid();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
                string FY = ddlYear.SelectedValue;
                DropDownList ddlPartCategory = gvPlan.FooterRow.FindControl("ddlPartCategory") as DropDownList;
                string[] arrMonths = { "txtJan", "txtFeb", "txtMar", "txtApr", "txtMay", "txtJun", "txtJul", "txtAug", "txtSep", "txtOct", "txtNov", "txtDec" };
                for (int iMonth = 1; iMonth < 13; iMonth++)
                {
                    TextBox txt = gvPlan.FooterRow.FindControl(arrMonths[iMonth - 1]) as TextBox;
                    int Year = Convert.ToInt32(iMonth < 4 ? FY.Substring(5, 4) : FY.Substring(0, 4));
                    oPlan.SavePartABP(DealerID, SPM_PlanType, Year, iMonth, ddlPartCategory.SelectedValue, Convert.ToDouble("0" + txt.Text), PSession.User.UserID);
                }
                BindGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('Saved Successfully!')", true);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError("While saving ABP Plan", ex);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('Error Occured!')", true);
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



        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            GridViewRow gvRow = lnk.NamingContainer as GridViewRow;

            Label lblPartCat = gvRow.FindControl("lblPartCat") as Label;
            oPlan.DeletePartABPForDealer(Convert.ToInt32(ddlDealer.SelectedValue), ddlYear.SelectedValue, lblPartCat.Text, PSession.User.UserID, SPM_PlanType);
            BindGrid();
        }

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["dtPlan"] = null;
            gvPlan.DataSource = null;
            gvPlan.DataBind();
            BindGrid();
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["dtPlan"] = null;
            gvPlan.DataSource = null;
            gvPlan.DataBind();
            BindGrid();

        }
        protected void BindGrid()
        {

            DataTable dtPlan = oPlan.GetPartABPForDealer(Convert.ToInt32(ddlDealer.SelectedValue), ddlYear.SelectedValue, SPM_PlanType);
            if (dtPlan.Rows.Count == 0)
            {
                DataRow dr = dtPlan.NewRow();
                dtPlan.Rows.Add(dr);
                gvPlan.DataSource = dtPlan;
                gvPlan.DataBind();
                gvPlan.Rows[0].Style.Add("display", "none");
            }
            else
            {
                gvPlan.DataSource = dtPlan;
                gvPlan.DataBind();
            }
            gvPlan.ShowHeaderWhenEmpty = true;
            gvPlan.ShowFooter = true;
            gvPlan.Visible = ddlDealer.SelectedValue != "0";
        }


        protected void gvPlan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void gvPlan_DataBound(object sender, EventArgs e)
        {

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPlan = oPlan.GetPartABPForDealer(Convert.ToInt32(ddlDealer.SelectedValue), ddlYear.SelectedValue, SPM_PlanType);

                if (dtPlan.Rows.Count > 0)
                {
                    DataRow dr = dtPlan.NewRow();
                    dr["D. Code"] = dtPlan.Rows[0]["D. Code"].ToString();
                    dr["D. Name"] = dtPlan.Rows[0]["D. Name"].ToString();
                    dr["D. Location"] = dtPlan.Rows[0]["D. Location"].ToString();
                    dr["PartCategory"] = "Total";

                    string[] arrMonths = new string[] { "Apr", "May", "Jun", "Q1", "Jul", "Aug", "Sep", "Q2", "Oct", "Nov", "Dec", "Q3", "Jan", "Feb", "Mar", "Q4", "Total" };
                    for (int i = 0; i < arrMonths.Length; i++)
                    {
                        dr[arrMonths[i]] = dtPlan.Compute("SUM(" + arrMonths[i] + ")", "").ToString();
                    }
                    dtPlan.Rows.Add(dr);
                }
                GridView gvExcel = new GridView();
                gvExcel.DataSource = dtPlan;
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
                Response.AppendHeader("content-disposition", "attachment;filename=\"" + ddlYear.SelectedItem.Text + " Plan.xls\"");

                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ky", "alert(\"" + ex.Message + "\")", true);
            }
        }
        protected void btnExportAll_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPlan = oPlan.GetPartABPForDealer_All(PSession.User.UserID, ddlYear.SelectedValue, SPM_PlanType);

                GridView gvExcel = new GridView();
                gvExcel.DataSource = dtPlan;
                gvExcel.DataBind();
                int ColCount = dtPlan.Columns.Count;
                foreach (GridViewRow gvRow in gvExcel.Rows)
                {
                    if (gvRow.Cells[0].Text.ToUpper().Contains("GRAND TOTAL"))
                    {
                        for (int i = 0; i < ColCount; i++)
                        {
                            gvRow.Cells[i].BackColor = ColorTranslator.FromHtml("#AAABAA");
                            gvRow.Cells[i].Font.Bold = true;
                        }
                    }
                    else if (gvRow.Cells[0].Text.ToUpper().Contains("TOTAL"))
                    {
                        for (int i = 0; i < ColCount; i++)
                        {
                            gvRow.Cells[i].BackColor = ColorTranslator.FromHtml("#D4D4D4");
                            gvRow.Cells[i].Font.Bold = true;
                        }

                    }
                    else if (gvRow.Cells[1].Text.ToUpper().Contains("TOTAL"))
                    {
                        for (int i = 0; i < ColCount; i++)
                        {
                            gvRow.Cells[i].BackColor = ColorTranslator.FromHtml("#EDD9D9");
                            gvRow.Cells[i].Font.Bold = true;
                        }

                    }
                }
                gvExcel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                gvExcel.HeaderStyle.BackColor = ColorTranslator.FromHtml("#D4D4D4");
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvExcel.RenderControl(hw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "";
                Response.AppendHeader("content-disposition", "attachment;filename=\"ABP Spare Part Retail " + ddlYear.SelectedItem.Text + ".xls\"");

                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ky", "alert(\"" + ex.Message + "\")", true);
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Properties;

namespace DealerManagementSystem.ViewMarketing
{
    public partial class LostCustomerData : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        BDMS_Activity oActivity = new BDMS_Activity();
        BDMS_CustomerSale oCustSale = new BDMS_CustomerSale();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["PkCustSaleID"] = 0;
                oActivity.GetCommonMaster(ddlCompititor, "CMP_MAKE", 0);
                oActivity.GetCommonMaster(ddlCompetitorSrch, "CMP_MAKE", 0);
                oActivity.GetCommonMaster(ddlResType, "LOST_REASON", 0);

                oCustSale.BindCustomerSale_GetAjaxModel(ddlAjax);
                oCustSale.BindYear(ddlYear);
                gvSearch.DataBind();

            }

        }

        //protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    odbcState.SelectParameters[0].DefaultValue = ddlCountry.SelectedValue.ToString();
        //    ddlState.DataBind();
        //}




        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BDMS_CustomerSale objCustomerSale = new BDMS_CustomerSale();
            DataTable dtDlr = objCustomerSale.GetDealerByStateID(PSession.User.UserID, Convert.ToInt32(ddlState.SelectedValue));
            ddlDealer.DataSourceID = "";
            ddlDealer.DataSource = dtDlr;
            ddlDealer.DataBind();
            if (ddlDealer.Items.Count > 1)
            {
                ddlDealer.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string sReturn = "";
            try
            {
                BDMS_CustomerSale oCS = new BDMS_CustomerSale();
                int DealerID = int.Parse(ddlDealer.SelectedValue);
                Int32 iYear = Convert.ToInt32(ddlYear.SelectedValue);
                Int32 iMonth = Convert.ToInt32(ddlMon.SelectedValue);
                string CustomerName = txtCustName.Text;
                string ContactPerson = txtConPerson.Text;
                string ContactNumber = txtContDeatil.Text;
                Int32 AjaxModelID = Convert.ToInt32(ddlAjax.SelectedValue);
                double AjaxPrice = Convert.ToDouble(txtAjaxPrice.Text);
                Int32 CompmakeID = Convert.ToInt32(ddlCompititor.SelectedValue);
                Int32 CompModelID = Convert.ToInt32(ddlComModel.SelectedValue);
                double CompPrice = Convert.ToDouble(txtComPrice.Text);
                Int32 Qty = Convert.ToInt32(txtQty.Text);
                Int32 Noofvisit = Convert.ToInt32(txtNOofVisit.Text);
                Int32 ResonTypeID = Convert.ToInt32(ddlResType.SelectedValue);
                string ReasonRemarks = txtremarks.Text;
                Int32 PkCustSaleID = Convert.ToInt32(ViewState["PkCustSaleID"]);
                sReturn = oCS.SaveCustomerSale(DealerID, iMonth, iYear, CustomerName, ContactPerson, ContactNumber, AjaxModelID, AjaxPrice, CompmakeID,
                    CompModelID, CompPrice, Qty, Noofvisit, ResonTypeID, ReasonRemarks, PSession.User.UserID, PkCustSaleID);
                lblMessage.Text = sReturn;
                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                return;
            }
        }

        protected void ddlCompititor_SelectedIndexChanged(object sender, EventArgs e)
        {
            oCustSale.BindCustomerSale_GetCompetitorModel(ddlComModel, Convert.ToInt32(ddlCompititor.SelectedValue));
        }

        protected void ddlStateSrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BDMS_CustomerSale objCustomerSale = new BDMS_CustomerSale();
            DataTable dtDlr = objCustomerSale.GetDealerByStateID(PSession.User.UserID, Convert.ToInt32(ddlStateSrch.SelectedValue));
            ddlDlrSrch.DataSourceID = "";
            ddlDlrSrch.DataSource = dtDlr;
            ddlDlrSrch.DataBind();
            if (ddlDlrSrch.Items.Count > 1)
            {
                ddlDlrSrch.Items.Insert(0, new ListItem("Select", "0"));
            }

        }

        protected void CompetitorSrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            oCustSale.BindCustomerSale_GetCompetitorModel(ddlCompetitorMdlSrch, Convert.ToInt32(ddlCompetitorSrch.SelectedValue));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int StateID = Convert.ToInt32(ddlStateSrch.SelectedValue);
                int DealeriD = Convert.ToInt32(ddlDlrSrch.SelectedValue);
                int FromYear = Convert.ToInt32(ddlFromYear.SelectedValue);
                int ToYear = Convert.ToInt32(ddlToYear.SelectedValue);
                int FromMonth = Convert.ToInt32(ddlFromMonth.SelectedValue);
                int ToMonth = Convert.ToInt32(ddlToMonth.SelectedValue);
                int AjaxModelOD = Convert.ToInt32(ddlAjaxModelSrch.SelectedValue);
                int CompID = Convert.ToInt32(ddlCompetitorSrch.SelectedValue);
                int CompModelID = Convert.ToInt32(ddlCompetitorMdlSrch.SelectedValue);
                DataSet ds = oCustSale.GetCustomerSaleData(StateID, DealeriD, FromYear, FromMonth, ToYear, ToMonth, AjaxModelOD, CompID, CompModelID, PSession.User.UserID);
                gvSearch.DataSource = ds;
                gvSearch.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                return;
            }

        }
        protected void Clear()
        {
            txtLoctaion.Text = ""; ddlYear.ClearSelection(); ddlMon.ClearSelection(); txtCustName.Text = "";
            txtConPerson.Text = ""; txtContDeatil.Text = ""; ddlAjax.ClearSelection(); txtAjaxPrice.Text = "";
            ddlCompititor.ClearSelection(); ddlCompititor_SelectedIndexChanged(null, null); txtComPrice.Text = "";
            txtQty.Text = ""; txtNOofVisit.Text = ""; ddlResType.ClearSelection(); txtremarks.Text = "";
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            divEntry.Visible = true;
            divSearch.Visible = false;
        }


        protected void btnSearchTab_Click(object sender, EventArgs e)
        {
            divEntry.Visible = false;
            divSearch.Visible = true;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            int CustomerID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            DataSet ds = oCustSale.GetCustomerSaleDataByID(CustomerID);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnNew_Click(btnNew, null);
                    DataRow dr = ds.Tables[0].Rows[0];
                    ddlState.SelectedValue = dr["StateID"].ToString();
                    ddlState_SelectedIndexChanged(null, null);
                    ddlDealer.SelectedValue = dr["CS_FkDealerID"].ToString();
                    ddlYear.SelectedValue = dr["CS_Year"].ToString();
                    ddlMon.SelectedValue = dr["CS_Month"].ToString();
                    txtCustName.Text = dr["CS_CustomerName"].ToString();
                    txtConPerson.Text = dr["CS_ContactPerson"].ToString();
                    txtContDeatil.Text = dr["CS_ContactNumber"].ToString();
                    ddlAjax.SelectedValue = dr["CS_FKAjaxModelID"].ToString();
                    ddlCompititor.SelectedValue = dr["CS_FkCompMakeID"].ToString();
                    ddlCompititor_SelectedIndexChanged(ddlCompititor, null);
                    ddlComModel.SelectedValue = dr["CS_FKcompModelID"].ToString();
                    ddlResType.SelectedValue = dr["CS_FKResonTypeID"].ToString();
                    txtAjaxPrice.Text = dr["CS_AjaxPrice"].ToString();
                    txtComPrice.Text = Convert.ToDecimal(dr["CS_CompPrice"]).ToString("0");
                    txtremarks.Text = dr["CS_ReasonRemarks"].ToString();
                    txtQty.Text = dr["CS_Qty"].ToString();
                    txtNOofVisit.Text = dr["CS_Noofvisit"].ToString();
                    ViewState["PkCustSaleID"] = CustomerID;
                }
            }

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                int StateID = Convert.ToInt32(ddlStateSrch.SelectedValue);
                int DealeriD = Convert.ToInt32(ddlDlrSrch.SelectedValue);
                int FromYear = Convert.ToInt32(ddlFromYear.SelectedValue);
                int ToYear = Convert.ToInt32(ddlToYear.SelectedValue);
                int FromMonth = Convert.ToInt32(ddlFromMonth.SelectedValue);
                int ToMonth = Convert.ToInt32(ddlToMonth.SelectedValue);
                int AjaxModelOD = Convert.ToInt32(ddlAjaxModelSrch.SelectedValue);
                int CompID = Convert.ToInt32(ddlCompetitorSrch.SelectedValue);
                int CompModelID = Convert.ToInt32(ddlCompetitorMdlSrch.SelectedValue);

                DataSet ds = oCustSale.GetCustomerSaleData(StateID, DealeriD, FromYear, FromMonth, ToYear, ToMonth, AjaxModelOD, CompID, CompModelID, PSession.User.UserID);
                DataTable dt = ds.Tables[0];
                dt.Columns.Remove("CS_PkCustSaleID");

                GridView gvExcel = new GridView();
                gvExcel.DataSource = dt;
                gvExcel.DataBind();

                gvExcel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvExcel.RenderControl(hw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "";
                Response.AppendHeader("content-disposition", "attachment;filename=\"Lost Customer Sale Data.xls\"");

                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                return;
            }
        }
    }
}
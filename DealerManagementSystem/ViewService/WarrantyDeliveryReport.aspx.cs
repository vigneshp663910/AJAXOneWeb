using Business;
using Properties; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyDeliveryReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_WarrantyDeliveryReport; } }
        protected void Page_PreInit(object sender, EventArgs e)
        { 
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            } 
        }
        public List<PDMS_DeliveryHeader> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_DeliveryReport"] == null)
                {
                    Session["DMS_DeliveryReport"] = new List<PDMS_DeliveryHeader>();
                }
                return (List<PDMS_DeliveryHeader>)Session["DMS_DeliveryReport"];
            }
            set
            {
                Session["DMS_DeliveryReport"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Warranty » Delivery Report');</script>");
             
            if (!IsPostBack)
            { 
                    fillDealer(); 
                txtDeliveryDateFrom.Text = DateTime.Now.ToShortDateString();
                txtDeliveryDateTo.Text = DateTime.Now.ToShortDateString();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillDelivery();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillDelivery()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                string DealerCode = ddlDealerCode.SelectedValue;
                string DeliveryNumber = txtDeliveryNumber.Text.Trim();
                //DateTime? DeliveryDateFrom = null;
                //DateTime? DeliveryDateTo = null;
                int? DeliveryTypeID = null;
                string DealerStateCode = "";

                //if (!string.IsNullOrEmpty(txtDeliveryDateFrom.Text.Trim()))
                //{
                //    DeliveryDateFrom = Convert.ToDateTime(txtDeliveryDateFrom.Text.Trim());
                //}
                //if (!string.IsNullOrEmpty(txtDeliveryDateTo.Text.Trim()))
                //{
                //    DeliveryDateTo = Convert.ToDateTime(txtDeliveryDateTo.Text.Trim());
                //}
                PDMS_Customer Dealer = new BDMS_Customer().getCustomerAddressFromSAP(DealerCode);

                DealerStateCode = Dealer.State.StateCode;
                List<PDMS_DeliveryHeader> SOIs = new BDMS_Delivery().getDelivery(DealerCode, DeliveryNumber, txtDeliveryDateFrom.Text.Trim(), txtDeliveryDateTo.Text.Trim(), DeliveryTypeID, DealerStateCode);

                SDMS_WarrantyClaimHeader = SOIs;

                gvDelivery.PageIndex = 0;
                gvDelivery.DataSource = SOIs;
                gvDelivery.DataBind();
                if (SOIs.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((gvDelivery.PageIndex) * gvDelivery.PageSize) + 1) + " - " + (((gvDelivery.PageIndex) * gvDelivery.PageSize) + gvDelivery.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_DeliveryReport", "fillDelivery", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDelivery.PageIndex > 0)
            {
                gvDelivery.PageIndex = gvDelivery.PageIndex - 1;
                gvDelivery.DataSource = SDMS_WarrantyClaimHeader;
                gvDelivery.DataBind();
                lblRowCount.Text = (((gvDelivery.PageIndex) * gvDelivery.PageSize) + 1) + " - " + (((gvDelivery.PageIndex) * gvDelivery.PageSize) + gvDelivery.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDelivery.PageCount > gvDelivery.PageIndex)
            {
                gvDelivery.PageIndex = gvDelivery.PageIndex + 1;
                gvDelivery.DataSource = SDMS_WarrantyClaimHeader;
                gvDelivery.DataBind();
                lblRowCount.Text = (((gvDelivery.PageIndex) * gvDelivery.PageSize) + 1) + " - " + (((gvDelivery.PageIndex) * gvDelivery.PageSize) + gvDelivery.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDelivery.PageIndex = e.NewPageIndex;
            gvDelivery.DataSource = SDMS_WarrantyClaimHeader;
            gvDelivery.DataBind();
            lblRowCount.Text = (((gvDelivery.PageIndex) * gvDelivery.PageSize) + 1) + " - " + (((gvDelivery.PageIndex) * gvDelivery.PageSize) + gvDelivery.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

        }
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
                DateTime traceStartTime = DateTime.Now;
                try
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        string DeliveryNumber = Convert.ToString(gvDelivery.DataKeys[e.Row.RowIndex].Value);
                        GridView gvClaimInvoiceItem = (GridView)e.Row.FindControl("gvClaimInvoiceItem");
                        List<PDMS_DeliveryItem> Lines = new List<PDMS_DeliveryItem>();
                        Lines = SDMS_WarrantyClaimHeader.Find(s => s.DeliveryNumber == DeliveryNumber).DeliveryItems;

                        gvClaimInvoiceItem.DataSource = Lines;

                        gvClaimInvoiceItem.DataBind();

                        TextBox txtTransportationThrough = (TextBox)e.Row.FindControl("txtTransportationThrough");
                        TextBox txtTransportationDate = (TextBox)e.Row.FindControl("txtTransportationDate");
                        TextBox txtVehicleNumber = (TextBox)e.Row.FindControl("txtVehicleNumber");
                        PDMS_DeliveryHeader Transportation = new BDMS_Delivery().GetDeliveryTransportationDetails(DeliveryNumber, null, null)[0];
                        txtTransportationThrough.Text = Transportation.TransportationThrough;
                        txtTransportationDate.Text = Transportation.TransportationDate == null ? "" : ((DateTime)Transportation.TransportationDate).ToShortDateString();
                        txtVehicleNumber.Text = Transportation.VehicleNumber;
                    }
                    TraceLogger.Log(traceStartTime);
                }
                catch (Exception ex)
                {

                }
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
        }
        protected void ibPDF_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            string DeliveryNumber = Convert.ToString(gvDelivery.DataKeys[gvRow.RowIndex].Value);
            string DealerCode = ((Label)gvDelivery.Rows[gvRow.RowIndex].FindControl("lblDealerCode")).Text;

            DateTime DeliveryDate = Convert.ToDateTime(((Label)gvDelivery.Rows[gvRow.RowIndex].FindControl("lblDeliveryDate")).Text);
            string TransportationThrough = ((TextBox)gvDelivery.Rows[gvRow.RowIndex].FindControl("txtTransportationThrough")).Text;
            string TransportationDate = ((TextBox)gvDelivery.Rows[gvRow.RowIndex].FindControl("txtTransportationDate")).Text;
            string VehicleNumber = ((TextBox)gvDelivery.Rows[gvRow.RowIndex].FindControl("txtVehicleNumber")).Text;
            new BDMS_Delivery().InsertOrUpdateDeliveryTransportationDetails(DeliveryNumber, DeliveryDate, TransportationThrough, string.IsNullOrEmpty(TransportationDate) ? (DateTime?)null : Convert.ToDateTime(TransportationDate), VehicleNumber);
            PAttachedFile UploadedFile = new BDMS_Delivery().DeliveryChellanBelow50K_Print(DeliveryNumber, DealerCode);

            string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            Response.AddHeader("Content-type", UploadedFile.FileType);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "." + UploadedFile.FileType);
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            Response.BinaryWrite(UploadedFile.AttachedFile);
            Response.Flush();
            Response.End();
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Delivery Number");
            dt.Columns.Add("Delivery Date");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Grand Total");
            dt.Columns.Add("Material");
            dt.Columns.Add("Material Desc");
            dt.Columns.Add("HSN Code");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Rate");
            dt.Columns.Add("Value");
            dt.Columns.Add("Discount");
            dt.Columns.Add("Taxable Value");
            dt.Columns.Add("CGST");
            dt.Columns.Add("CGST Value");
            dt.Columns.Add("SGST");
            dt.Columns.Add("SGST Value");
            dt.Columns.Add("IGST");
            dt.Columns.Add("IGST Value");
            foreach (PDMS_DeliveryHeader IC in SDMS_WarrantyClaimHeader)
            {
                foreach (PDMS_DeliveryItem Item in IC.DeliveryItems)
                {
                    dt.Rows.Add(
                          IC.DeliveryNumber
                        , IC.DeliveryDate.ToShortDateString()
                        , IC.Dealer.DealerCode
                        , IC.Dealer.DealerName
                        , IC.GrandTotal
                        , Item.Material
                        , Item.MaterialDesc
                        , Item.HSNCode
                        , Item.Qty
                        , Item.Rate
                        , Item.Value
                        , Item.Discount
                        , Item.TaxableValue
                        , Item.CGST
                        , Item.CGSTValue
                        , Item.SGST
                        , Item.SGSTValue
                        , Item.IGST
                        , Item.IGSTValue
                        );
                }
            }
            new BXcel().ExporttoExcel(dt, "Delivery Report");
        }
    }
}
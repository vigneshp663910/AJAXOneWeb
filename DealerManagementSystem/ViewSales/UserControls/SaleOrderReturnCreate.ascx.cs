using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSales.UserControls
{
    public partial class SaleOrderReturnCreate : System.Web.UI.UserControl
    {
        private List<PSaleOrderDelivery> SoDeliveryList
        {
            get
            {
                if (ViewState["SoDeliveryList"] == null)
                {
                    ViewState["SoDeliveryList"] = new List<PSaleOrderDelivery>();
                }
                return (List<PSaleOrderDelivery>)ViewState["SoDeliveryList"];
            }
            set
            {
                ViewState["SoDeliveryList"] = value;
            }
        }
        //private List<string> gvSoDeliverySelected
        //{
        //    get
        //    {
        //        if (ViewState["gvSoDeliverySelected"] == null)
        //        {
        //            ViewState["gvSoDeliverySelected"] = new List<string>();
        //        }
        //        return (List<string>)ViewState["gvSoDeliverySelected"];
        //    }
        //    set
        //    {
        //        ViewState["gvSoDeliverySelected"] = value;
        //    }
        //}
        private List<long> gvSoDeliverySelected
        {
            get
            {
                if (ViewState["gvSoDeliverySelected"] == null)
                {
                    ViewState["gvSoDeliverySelected"] = new List<long>();
                }
                return (List<long>)ViewState["gvSoDeliverySelected"];
            }
            set
            {
                ViewState["gvSoDeliverySelected"] = value;
            }
        }
        //private List<PSaleOrderDelivery> SoDeliveryItemList
        //{
        //    get
        //    {
        //        if (ViewState["SoDeliveryItemList"] == null)
        //        {
        //            ViewState["SoDeliveryItemList"] = new List<PSaleOrderDelivery>();
        //        }
        //        return (List<PSaleOrderDelivery>)ViewState["SoDeliveryItemList"];
        //    }
        //    set
        //    {
        //        ViewState["SoDeliveryItemList"] = value;
        //    }
        //}
        private DataTable SoDeliveryItemList
        {
            get
            {
                if (ViewState["SoDeliveryItemList"] == null)
                {
                    ViewState["SoDeliveryItemList"] = new List<PSaleOrderDelivery>();
                }
                return (DataTable)ViewState["SoDeliveryItemList"];
            }
            set
            {
                ViewState["SoDeliveryItemList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void Clear()
        {
            gvSoDelivery.DataSource = null;
            gvSoDelivery.DataBind();
            gvSoDeliveryItem.DataSource = null;
            gvSoDeliveryItem.DataBind();
            gvSoDeliverySelected.Clear();
            divSoDelivery.Visible = false;
            divSoDeliveryItem.Visible = false;
            txtInvoiceNo.Text = "";
            lblMessageSoReturnCreate.Visible = false;
            lblMessageSoReturnCreate.Text = "";
            divInvoiceDetails.Visible = false;
            divRemarks.Visible = false;
        }
        public void FillMaster()
        {
            Clear();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessageSoReturnCreate.Visible = true;
            gvSoDelivery.DataSource = null;
            gvSoDelivery.DataBind();
            string message = Validation();
            if (!string.IsNullOrEmpty(message))
            {
                lblMessageSoReturnCreate.Text = message;
                lblMessageSoReturnCreate.ForeColor = Color.Red;
                return;
            }
            ViewState["InvoiceNumber"] = txtInvoiceNo.Text.Trim();
            PApiResult Result = new BSalesOrderReturn().GetSaleOrderDeliveryForSoReturnCreation(Convert.ToString(ViewState["InvoiceNumber"]));
            
            if (Result.Status == PApplication.Failure)
            {
                lblMessageSoReturnCreate.Text = Result.Message;
                lblMessageSoReturnCreate.ForeColor = Color.Red;
                return;
            }
            SoDeliveryList = JsonConvert.DeserializeObject<List<PSaleOrderDelivery>>(JsonConvert.SerializeObject(Result.Data));

            lblDealer.Text = SoDeliveryList[0].SaleOrder.Dealer.DealerCode + " " + SoDeliveryList[0].SaleOrder.Dealer.DealerName;
            lblDealerOffice.Text = SoDeliveryList[0].SaleOrder.Dealer.DealerOffice.OfficeName_OfficeCode;
            lblCustomer.Text = SoDeliveryList[0].SaleOrder.Customer.CustomerCode + " " + SoDeliveryList[0].SaleOrder.Customer.CustomerName;
            lblInvoiceNumberDate.Text = SoDeliveryList[0].InvoiceNumber + " " + SoDeliveryList[0].InvoiceDate.ToString();

            divInvoiceDetails.Visible = true;
            divRemarks.Visible = true;
            gvSoDelivery.DataSource = SoDeliveryList;
            gvSoDelivery.DataBind();
            fillSoDelivery();
        }
        public string Validation()
        {
           if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                txtInvoiceNo.BorderColor = Color.Red;
                return "Please enter Invoice Number.";
            }
            return "";
        }
        public void fillSoDelivery()
        {
            //Clear();
            gvSoDelivery.DataSource = SoDeliveryList;
            gvSoDelivery.DataBind();
            foreach (GridViewRow row in gvSoDelivery.Rows)
            {
                CheckBox cbInvoice = (CheckBox)row.FindControl("cbSeleccbInvoicetChild");
                Label lblSaleOrderDeliveryItemID = (Label)row.FindControl("lblSaleOrderDeliveryItemID");
                if (gvSoDeliverySelected.Contains(Convert.ToInt64(lblSaleOrderDeliveryItemID.Text)))
                {
                    cbInvoice.Checked = true;
                }
            }
            
            if (SoDeliveryList.Count == 0)
            {
                lblRowCountSoDelivery.Visible = false;
                divSoDelivery.Visible = false;
            }
            else
            {
                lblRowCountSoDelivery.Visible = true;
                lblRowCountSoDelivery.Text = (((gvSoDelivery.PageIndex) * gvSoDelivery.PageSize) + 1) + " - " + (((gvSoDelivery.PageIndex) * gvSoDelivery.PageSize) + gvSoDelivery.Rows.Count) + " of " + SoDeliveryList.Count;
                divSoDelivery.Visible = true;
            }
        }
        public List<PSaleOrderDelivery> ReadSoDelivery()
        {
            List<PSaleOrderDelivery> pSoDeliveryItem = new List<PSaleOrderDelivery>();
            selectedGv();
            if (gvSoDeliverySelected.Count == 0)
            {
                lblMessageSoReturnCreate.Visible = true;
                lblMessageSoReturnCreate.Text = "Please select the Invoice.";
                lblMessageSoReturnCreate.ForeColor = Color.Red;
            }
            foreach (int Item in gvSoDeliverySelected)
            {
                foreach (PSaleOrderDelivery SoDelivery in SoDeliveryList)
                {
                    if (Item == SoDelivery.SaleOrderDeliveryItem.SaleOrderDeliveryItemID)
                    {
                        SoDeliveryItemList = new BSalesOrderReturn().GetSaleOrderDeliveryItemForSoReturnCreation(SoDelivery.SaleOrderDeliveryItem.SaleOrderDeliveryItemID);

                        for (int i = 0; i < SoDeliveryItemList.Rows.Count; i++)
                        {
                            pSoDeliveryItem.Add(new PSaleOrderDelivery()
                            {
                                SaleOrderDeliveryID = Convert.ToInt64(SoDeliveryItemList.Rows[i]["SaleOrderDeliveryID"]),
                                InvoiceNumber = SoDeliveryItemList.Rows[i]["InvoiceNumber"].ToString().Trim(),
                                InvoiceDate = Convert.ToDateTime(SoDeliveryItemList.Rows[i]["InvoiceDate"]),
                                SaleOrderDeliveryItem = new PSaleOrderDeliveryItem()
                                {
                                    SaleOrderDeliveryItemID = Convert.ToInt64(SoDeliveryItemList.Rows[i]["SaleOrderDeliveryItemID"]),
                                    SaleOrderItem = new PSaleOrderItem()
                                    {
                                        Material = new PDMS_Material()
                                        {
                                            MaterialID = Convert.ToInt32(SoDeliveryItemList.Rows[i]["MaterialID"]),
                                            MaterialCode = SoDeliveryItemList.Rows[i]["MaterialCode"].ToString().Trim(),
                                            MaterialDescription = SoDeliveryItemList.Rows[i]["MaterialDescription"].ToString().Trim(),
                                            BaseUnit = SoDeliveryItemList.Rows[i]["BaseUnit"].ToString().Trim(),
                                        },
                                    },
                                    Qty = Convert.ToDecimal(SoDeliveryItemList.Rows[i]["Qty"]),
                                }
                            });
                        }
                    }
                }
            }

            if (pSoDeliveryItem != null)
            {
                if (pSoDeliveryItem.Count > 0)
                {
                    gvSoDeliveryItem.DataSource = pSoDeliveryItem;
                    gvSoDeliveryItem.DataBind();
                    divSoDelivery.Visible = false;
                    divSoDeliveryItem.Visible = true;
                }
            }
            return pSoDeliveryItem;
        }
        void selectedGv()
        {
            foreach (GridViewRow row in gvSoDelivery.Rows)
            {
                CheckBox cbInvoice = (CheckBox)row.FindControl("cbInvoice");
                Label lblSaleOrderDeliveryItemID = (Label)row.FindControl("lblSaleOrderDeliveryItemID");

                if (cbInvoice.Checked)
                {
                    if (!gvSoDeliverySelected.Contains(Convert.ToInt64(lblSaleOrderDeliveryItemID.Text)))
                    {
                        gvSoDeliverySelected.Add(Convert.ToInt64(lblSaleOrderDeliveryItemID.Text));
                    }
                }
                else
                {
                    if (gvSoDeliverySelected.Contains(Convert.ToInt64(lblSaleOrderDeliveryItemID.Text)))
                    {
                        gvSoDeliverySelected.Remove(Convert.ToInt64(lblSaleOrderDeliveryItemID.Text));
                    }
                }
            }
        }
        protected void gvSoDelivery_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSoDelivery.PageIndex = e.NewPageIndex;
            selectedGv();
            fillSoDelivery();
        }
        public string RValidateReturnDelivery()
        {
            lblMessageSoReturnCreate.Visible = true;
            lblMessageSoReturnCreate.ForeColor = Color.Red;

            foreach (GridViewRow row in gvSoDeliveryItem.Rows)
            {
                TextBox txtReturnQty = (TextBox)row.FindControl("txtReturnQty");
                if (!string.IsNullOrEmpty(txtReturnQty.Text))
                {
                    decimal ReturnQty = Convert.ToDecimal(((TextBox)row.FindControl("txtReturnQty")).Text);
                    if (ReturnQty == 0)
                    {
                        return "Please enter Return Quantity.";
                    }
                    else if (ReturnQty > (Convert.ToDecimal(((Label)row.FindControl("lblQty")).Text)))
                    {
                        return "Please enter valid Return Quantity.";
                    }
                }
            }
            return "";
        }
        public List<PSaleOrderReturnItem_Insert> ReadSoDeliveryItem()
        {
            lblMessageSoReturnCreate.Visible = true;
            lblMessageSoReturnCreate.ForeColor = Color.Red;
            List<PSaleOrderReturnItem_Insert> soReturn = new List<PSaleOrderReturnItem_Insert>();

            foreach (GridViewRow row in gvSoDeliveryItem.Rows)
            {
                TextBox txtReturnQty = (TextBox)row.FindControl("txtReturnQty");
                if (!string.IsNullOrEmpty(txtReturnQty.Text))
                {
                    decimal ReturnQty = Convert.ToDecimal(((TextBox)row.FindControl("txtReturnQty")).Text);
                    if (ReturnQty == 0)
                    {
                        lblMessageSoReturnCreate.Text = "Please enter Return Quantity.";
                        return soReturn;
                    }
                    else if (ReturnQty > (Convert.ToDecimal(((Label)row.FindControl("lblQty")).Text)))
                    {
                        lblMessageSoReturnCreate.Text = "Please enter valid Return Quantity.";
                        return soReturn;
                    }

                    long SaleOrderDeliveryID = Convert.ToInt64(((Label)row.FindControl("lblSaleOrderDeliveryID")).Text);
                    long SaleOrderDeliveryItemID = Convert.ToInt64(((Label)row.FindControl("lblSaleOrderDeliveryItemID")).Text);
                    decimal Qty = Convert.ToDecimal(((TextBox)row.FindControl("txtReturnQty")).Text);
                    
                    soReturn.Add(new PSaleOrderReturnItem_Insert()
                    {
                        SaleOrderDeliveryID = SaleOrderDeliveryID,
                        SaleOrderDeliveryItemID = SaleOrderDeliveryItemID,
                        Qty = Qty,
                        Remarks = txtRemarks.Text.Trim(),
                    });
                }
            }
            return soReturn;
        }
        protected void cbInvoiceH_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbInvoiceH = (CheckBox)sender;
            foreach (GridViewRow row in gvSoDelivery.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cbInvoice = row.FindControl("cbInvoice") as CheckBox;
                    cbInvoice.Checked = (cbInvoiceH.Checked) ? true : false;
                }
            }
        }
        protected void cbInvoice_CheckedChanged(object sender, EventArgs e)
        {
            bool ChkHeader = true;
            CheckBox cbInvoiceH = (CheckBox)gvSoDelivery.HeaderRow.FindControl("cbInvoiceH");
            foreach (GridViewRow row in gvSoDelivery.Rows)
            {
                CheckBox cbInvoice = row.FindControl("cbInvoice") as CheckBox;
                if (cbInvoice.Checked == false)
                {
                    ChkHeader = false;
                }
            }
            cbInvoiceH.Checked = ChkHeader;
        }
    }
}
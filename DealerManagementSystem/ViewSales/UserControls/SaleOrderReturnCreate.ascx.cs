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
            lblMessageSoReturnCreate.Text = "";
        }
        public void Clear()
        {
            divInvoiceSearch.Visible = true;
            gvSoDelivery.DataSource = null;
            gvSoDelivery.DataBind();
            gvSoDeliveryItem.DataSource = null;
            gvSoDeliveryItem.DataBind();
            gvSoDeliverySelected.Clear();
            divSoDelivery.Visible = false;
            divSoDeliveryItem.Visible = false;
            txtInvoiceNo.Text = "";
            lblMessageSoReturnCreate.Text = "";
            divInvoiceDetails.Visible = false;
        }
        public void FillMaster()
        {  
            Clear();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessageSoReturnCreate.ForeColor = Color.Red;
            gvSoDelivery.DataSource = null;
            gvSoDelivery.DataBind(); 
            if (string.IsNullOrEmpty(txtInvoiceNo.Text) && string.IsNullOrEmpty(txtDeliveryNo.Text.Trim()))
            {
                txtInvoiceNo.BorderColor = Color.Red;
                txtInvoiceNo.BorderColor = Color.Red;
                lblMessageSoReturnCreate.Text = "Please enter Invoice Number or Delivery Number.";
                return;
            } 
            PApiResult Result = new BSalesOrderReturn().GetSaleOrderDeliveryForSoReturnCreation(txtInvoiceNo.Text.Trim(), txtDeliveryNo.Text.Trim()); 
            if (Result.Status == PApplication.Failure)
            {
                lblMessageSoReturnCreate.Text = Result.Message; 
                return;
            }
            SoDeliveryList = JsonConvert.DeserializeObject<List<PSaleOrderDelivery>>(JsonConvert.SerializeObject(Result.Data));
            if (SoDeliveryList.Count ==0)
            {
                lblMessageSoReturnCreate.Text = "Please verify Invoice."; 
                return;
            }
            lblDealer.Text = SoDeliveryList[0].SaleOrder.Dealer.DealerCode + " " + SoDeliveryList[0].SaleOrder.Dealer.DealerName;
            lblDealerOffice.Text = SoDeliveryList[0].SaleOrder.Dealer.DealerOffice.OfficeName;
            lblCustomer.Text = SoDeliveryList[0].SaleOrder.Customer.CustomerCode + " " + SoDeliveryList[0].SaleOrder.Customer.CustomerName;
            lblDivision.Text = SoDeliveryList[0].SaleOrder.Division.DivisionCode;
            lblSaleOrderType.Text = SoDeliveryList[0].SaleOrder.SaleOrderType.SaleOrderType.ToString();
            lblDeliveryNumber.Text = SoDeliveryList[0].DeliveryNumber;
            lblDeliveryDate.Text = ((DateTime)SoDeliveryList[0].DeliveryDate).ToShortDateString();
            lblInvoiceNumber.Text = SoDeliveryList[0].InvoiceNumber;
            lblInvoiceDate.Text = SoDeliveryList[0].InvoiceDate == null ? null : ((DateTime)SoDeliveryList[0].InvoiceDate).ToShortDateString();
            fillSoDelivery();
        } 
        public void fillSoDelivery()
        {
            //Clear();
            gvSoDelivery.DataSource = SoDeliveryList;
            gvSoDelivery.DataBind();

            CheckBox cbInvoiceH = (CheckBox)gvSoDelivery.HeaderRow.FindControl("cbInvoiceH");
            cbInvoiceH.Checked = true;

            foreach (GridViewRow row in gvSoDelivery.Rows)
            {
                CheckBox cbInvoice = (CheckBox)row.FindControl("cbInvoice");
                Label lblSaleOrderDeliveryItemID = (Label)row.FindControl("lblSaleOrderDeliveryItemID");
                if (gvSoDeliverySelected.Contains(Convert.ToInt64(lblSaleOrderDeliveryItemID.Text)))
                {
                    cbInvoice.Checked = true;
                }
                else
                {
                    cbInvoiceH.Checked = false;
                    cbInvoice.Checked = false;
                }
            } 
            if (SoDeliveryList.Count == 0)
            {
                lblRowCountSoDelivery.Visible = false;
                divSoDelivery.Visible = false;
                divInvoiceSearch.Visible = true;
                divInvoiceDetails.Visible = false;
            }
            else
            {
                lblRowCountSoDelivery.Visible = true;
                lblRowCountSoDelivery.Text = (((gvSoDelivery.PageIndex) * gvSoDelivery.PageSize) + 1) + " - " + (((gvSoDelivery.PageIndex) * gvSoDelivery.PageSize) + gvSoDelivery.Rows.Count) + " of " + SoDeliveryList.Count;
                divSoDelivery.Visible = true;
                divInvoiceSearch.Visible = false;
                divInvoiceDetails.Visible = true;
            }
        }                
        protected void gvSoDelivery_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSoDelivery.PageIndex = e.NewPageIndex; 
            fillSoDelivery();
        }
        public string RValidateReturnDelivery()
        {
            lblMessageSoReturnCreate.ForeColor = Color.Red;

            foreach (GridViewRow row in gvSoDeliveryItem.Rows)
            {
                TextBox txtReturnQty = (TextBox)row.FindControl("txtReturnQty");
                if (string.IsNullOrEmpty(txtReturnQty.Text))
                {
                    return "Please enter the Return Quantity for all the selected Items.";
                }
                //decimal ReturnQty = Convert.ToDecimal(((TextBox)row.FindControl("txtReturnQty")).Text);

                Decimal.TryParse("0" + ((TextBox)row.FindControl("txtReturnQty")).Text, out decimal ReturnQty);

                if (ReturnQty <= 0)
                {
                    return "Please enter Valid Return Quantity.";
                }
                else if (ReturnQty > (Convert.ToDecimal(((Label)row.FindControl("lblQty")).Text)))
                {
                    return "Please enter valid Return Quantity.";
                }
            }
            return "";
        }
        public List<PSaleOrderReturnItem_Insert> ReadSoDeliveryItem()
        {
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
                CheckBox cbInvoice = row.FindControl("cbInvoice") as CheckBox;
                cbInvoice.Checked = cbInvoiceH.Checked;
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
        protected void cbInvoice_CheckedChanged(object sender, EventArgs e)
        {
            bool ChkHeader = true;
           
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent; 
            Label lblSaleOrderDeliveryItemID = (Label)gvRow.FindControl("lblSaleOrderDeliveryItemID");
            CheckBox cbInvoice = (CheckBox)gvRow.FindControl("cbInvoice");
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

            foreach (GridViewRow row in gvSoDelivery.Rows)
            {
                cbInvoice = row.FindControl("cbInvoice") as CheckBox;
                if (cbInvoice.Checked == false)
                {
                    ChkHeader = false;
                }
            }
            CheckBox cbInvoiceH = (CheckBox)gvSoDelivery.HeaderRow.FindControl("cbInvoiceH");
            cbInvoiceH.Checked = ChkHeader;
        }
        protected void btnCreateSalesReturn_Click(object sender, EventArgs e)
        {
            List<PSaleOrderDelivery> pSoDeliveryItem = new List<PSaleOrderDelivery>(); 
            if (gvSoDeliverySelected.Count == 0)
            {
                lblMessageSoReturnCreate.Text = "Please select the Invoice.";
                lblMessageSoReturnCreate.ForeColor = Color.Red;
            }
            foreach (long Item in gvSoDeliverySelected)
            {
                foreach (PSaleOrderDelivery SoDelivery in SoDeliveryList)
                {
                    if (Item == SoDelivery.SaleOrderDeliveryItem.SaleOrderDeliveryItemID)
                    {
                        pSoDeliveryItem.Add(new PSaleOrderDelivery()
                        {
                            SaleOrderDeliveryID = SoDelivery.SaleOrderDeliveryID,
                            InvoiceNumber = SoDelivery.InvoiceNumber,
                            InvoiceDate = SoDelivery.InvoiceDate,
                            SaleOrderDeliveryItem = new PSaleOrderDeliveryItem()
                            {
                                SaleOrderDeliveryItemID = SoDelivery.SaleOrderDeliveryItem.SaleOrderDeliveryItemID,
                                Material = new PDMS_Material()
                                {
                                    MaterialID = SoDelivery.SaleOrderDeliveryItem.Material.MaterialID,
                                    MaterialCode = SoDelivery.SaleOrderDeliveryItem.Material.MaterialCode,
                                    MaterialDescription = SoDelivery.SaleOrderDeliveryItem.Material.MaterialDescription,
                                    BaseUnit = SoDelivery.SaleOrderDeliveryItem.Material.BaseUnit,
                                }, 
                                Qty = SoDelivery.SaleOrderDeliveryItem.Qty,
                            }
                        });
                    }
                }
            } 
          
            if (pSoDeliveryItem.Count > 0)
            {
                gvSoDeliveryItem.DataSource = pSoDeliveryItem;
                gvSoDeliveryItem.DataBind();
                divSoDelivery.Visible = false;
                divSoDeliveryItem.Visible = true;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessageSoReturnCreate.ForeColor = Color.Red;
            string message = RValidateReturnDelivery();
            if (!string.IsNullOrEmpty(message))
            {
                lblMessageSoReturnCreate.Text = message;
                return;
            } 
            List<PSaleOrderReturnItem_Insert> pSoReturnItem = ReadSoDeliveryItem();
            string result = new BAPI().ApiPut("SaleOrderReturn/SaleOrderReturnCreate", pSoReturnItem);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessageSoReturnCreate.Text = Result.Message;
                return;
            }
            //lblMessageSoReturnCreate.Text = Result.Message;
            //lblMessageSoReturnCreate.ForeColor = Color.Green;
            //fillViewSoReturn(Convert.ToInt64(Result.Data));
            Session["SaleOrderReturnID"] = Result.Data;
            Response.Redirect("SaleOrderReturn.aspx");
        }
    }
}
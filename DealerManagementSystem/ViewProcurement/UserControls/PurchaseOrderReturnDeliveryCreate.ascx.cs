using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement.UserControls
{
    public partial class PurchaseOrderReturnDeliveryCreate : System.Web.UI.UserControl
    {
        private List<PPurchaseOrderReturn> PoReturnItemList
        {
            get
            {
                if (ViewState["PoReturnItemList"] == null)
                {
                    ViewState["PoReturnItemList"] = new List<PPurchaseOrderReturn>();
                }
                return (List<PPurchaseOrderReturn>)ViewState["PoReturnItemList"];
            }
            set
            {
                ViewState["PoReturnItemList"] = value;
            }
        }
        private List<long> gvPoReturnItemSelected
        {
            get
            {
                if (ViewState["gvPoReturnItemSelected"] == null)
                {
                    ViewState["gvPoReturnItemSelected"] = new List<long>();
                }
                return (List<long>)ViewState["gvPoReturnItemSelected"];
            }
            set
            {
                ViewState["gvPoReturnItemSelected"] = value;
            }
        }
        public List<PPurchaseOrderReturnDeliveryItem_Insert> PurchaseOrderReturnDeliveryItem_Insert
        {
            get
            {
                if (ViewState["PurchaseOrderReturnDeliveryItem_Insert"] == null)
                {
                    ViewState["PurchaseOrderReturnDeliveryItem_Insert"] = new List<PPurchaseOrderReturnDeliveryItem_Insert>();
                }
                return (List<PPurchaseOrderReturnDeliveryItem_Insert>)ViewState["PurchaseOrderReturnDeliveryItem_Insert"];
            }
            set
            {
                ViewState["PurchaseOrderReturnDeliveryItem_Insert"] = value;
            }
        }

        public PPurchaseOrderReturnDelivery PoRDelivery
        {
            get
            {
                if (ViewState["PPurchaseOrderReturnDelivery"] == null)
                {
                    ViewState["PPurchaseOrderReturnDelivery"] = new PPurchaseOrderReturnDelivery();
                }
                return (PPurchaseOrderReturnDelivery)ViewState["PPurchaseOrderReturnDelivery"];
            }
            set
            {
                ViewState["PPurchaseOrderReturnDelivery"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void fillPOReturnItem(long PurchaseOrderReturnID)
        {
            Clear();
            PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderReturnItemForDeliveryCreation(PurchaseOrderReturnID);
            PoReturnItemList = JsonConvert.DeserializeObject<List<PPurchaseOrderReturn>>(JsonConvert.SerializeObject(Result.Data));
            gvPoReturnItem.DataSource = PoReturnItemList;
            gvPoReturnItem.DataBind();
            fillPendingDelivery();
        }
        void Clear()
        {
            gvPoReturnItem.DataSource = null;
            gvPoReturnItem.DataBind();
            gvPoReturnItemSelected.Clear();
            lblMessagePoReturnDeliveryCreate.Visible = false;
            lblMessagePoReturnDeliveryCreate.Text = "";
            lblMessagePoReturnDeliveryCreate.Visible = false;
        }
        public void fillPendingDelivery()
        {
            gvPoReturnItem.DataSource = PoReturnItemList;
            gvPoReturnItem.DataBind();
            foreach (GridViewRow row in gvPoReturnItem.Rows)
            {
                CheckBox cbIsChecked = (CheckBox)row.FindControl("cbIsChecked");
                Label lblPurchaseOrderReturnItemID = (Label)row.FindControl("lblPurchaseOrderReturnItemID");
                if (gvPoReturnItemSelected.Contains(Convert.ToInt32( lblPurchaseOrderReturnItemID.Text)))
                {
                    cbIsChecked.Checked = true;
                }
            }
            if (PoReturnItemList.Count == 0)
            {
                lblRowCountPoReturnItem.Visible = false;
            }
            else
            {
                lblRowCountPoReturnItem.Visible = true;
                lblRowCountPoReturnItem.Text = (((gvPoReturnItem.PageIndex) * gvPoReturnItem.PageSize) + 1) + " - " + (((gvPoReturnItem.PageIndex) * gvPoReturnItem.PageSize) + gvPoReturnItem.Rows.Count) + " of " + PoReturnItemList.Count;
            }
        }
        protected void gvPoReturnItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPoReturnItem.PageIndex = e.NewPageIndex;
            selectedGv();
            fillPendingDelivery();
        }
        protected void btnProceedDelivery_Click(object sender, EventArgs e)
        {
            lblMessagePoReturnDeliveryCreate.Text = "";
            List<PPurchaseOrderReturnItem> poReturnItem = ReadPoReturnItem();
            if (poReturnItem != null)
            {
                if (poReturnItem.Count > 0)
                {
                    gvPoReturnItemForDelivery.DataSource = poReturnItem;
                    gvPoReturnItemForDelivery.DataBind();
                    divDeliveryEntry.Visible = true;
                }
            }
        }
        public List<PPurchaseOrderReturnItem> ReadPoReturnItem()
        {
            List<PPurchaseOrderReturnItem> PurchaseOrderReturnItem = new List<PPurchaseOrderReturnItem>();
            selectedGv();
            if (gvPoReturnItemSelected.Count == 0)
            {
                lblMessagePoReturnDeliveryCreate.Visible = true;
                lblMessagePoReturnDeliveryCreate.Text = "Please select the PO Item.";
                lblMessagePoReturnDeliveryCreate.ForeColor = Color.Red;
                return PurchaseOrderReturnItem;
            }

            foreach (int Item in gvPoReturnItemSelected)
            {
                foreach (PPurchaseOrderReturn PoReItem in PoReturnItemList)
                {
                    if (Item == PoReItem.PurchaseOrderReturnItem.PurchaseOrderReturnItemID)
                    {
                        PurchaseOrderReturnItem.Add(new PPurchaseOrderReturnItem()
                        {
                            PurchaseOrderReturnID = PoReItem.PurchaseOrderReturnID,
                            PurchaseOrderReturnItemID = PoReItem.PurchaseOrderReturnItem.PurchaseOrderReturnItemID,
                            Item = PoReItem.PurchaseOrderReturnItem.Item,
                            Material = new PMaterial()
                            {
                                MaterialID = PoReItem.PurchaseOrderReturnItem.Material.MaterialID,
                                MaterialCode = PoReItem.PurchaseOrderReturnItem.Material.MaterialCode,
                                MaterialDescription = PoReItem.PurchaseOrderReturnItem.Material.MaterialDescription,
                                BaseUnit = PoReItem.PurchaseOrderReturnItem.Material.BaseUnit,
                            },
                            Quantity = PoReItem.PurchaseOrderReturnItem.Quantity,
                        });
                    }
                }
            }
            return PurchaseOrderReturnItem;
        }
        void selectedGv()
        {
            foreach (GridViewRow row in gvPoReturnItem.Rows)
            {
                CheckBox cbIsChecked = (CheckBox)row.FindControl("cbIsChecked");
                Label lblPurchaseOrderReturnItemID = (Label)row.FindControl("lblPurchaseOrderReturnItemID");


                long PurchaseOrderReturnItemID = Convert.ToInt64(lblPurchaseOrderReturnItemID.Text);
                if (cbIsChecked.Checked)
                {
                    if (!gvPoReturnItemSelected.Contains(PurchaseOrderReturnItemID))
                    {
                        gvPoReturnItemSelected.Add(PurchaseOrderReturnItemID);
                    }
                }
                else
                {
                    if (gvPoReturnItemSelected.Contains(PurchaseOrderReturnItemID))
                    {
                        gvPoReturnItemSelected.Remove(PurchaseOrderReturnItemID);
                    }
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<PPurchaseOrderReturnDeliveryItem_Insert> poReturnDelivery = ReadPoReturnDeliveryItem();

            foreach (GridViewRow row in gvPoReturnItemForDelivery.Rows)
            {
                if (poReturnDelivery.Count != gvPoReturnItemForDelivery.Rows.Count)
                {
                    lblMessagePoReturnDeliveryCreate.Visible = true;
                    lblMessagePoReturnDeliveryCreate.Text = "Please enter the Delivery Quantity for all selected Items.";
                    lblMessagePoReturnDeliveryCreate.ForeColor = Color.Red;
                    return;
                }
                

                foreach (PPurchaseOrderReturnDeliveryItem_Insert PoReItemDelivery in poReturnDelivery)
                {
                    if (PoReItemDelivery.DeliveryQty == 0)
                        {
                            lblMessagePoReturnDeliveryCreate.Visible = true;
                            lblMessagePoReturnDeliveryCreate.Text = "Please enter Delivery Quantity.";
                            lblMessagePoReturnDeliveryCreate.ForeColor = Color.Red;
                            return;
                        }

                        else if (PoReItemDelivery.DeliveryQty > (Convert.ToDecimal(((Label)row.FindControl("lblQty")).Text)))
                        {
                            lblMessagePoReturnDeliveryCreate.Visible = true;
                            lblMessagePoReturnDeliveryCreate.Text = "Please enter valid Delivery Quantity.";
                            lblMessagePoReturnDeliveryCreate.ForeColor = Color.Red;
                            return;
                        }
                    }
            }

            string result = new BAPI().ApiPut("PurchaseOrder/PurchaseOrderReturnDeliveryCreate", poReturnDelivery);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);
            
            if (Result.Status == PApplication.Failure)
            {
                lblMessagePoReturnDeliveryCreate.Text = Result.Message;
                return;
            }
            
            PoRDelivery = new BDMS_PurchaseOrder().GetPurchaseOrderReturnDeliveryByID(Convert.ToInt64(Result.Data));
            
            gvPoReturnDeliveryItem.DataSource = PoRDelivery.PurchaseOrderReturnDeliveryItemS;
            gvPoReturnDeliveryItem.DataBind();

        }        
        public List<PPurchaseOrderReturnDeliveryItem_Insert> ReadPoReturnDeliveryItem()
        {
            List<PPurchaseOrderReturnDeliveryItem_Insert> poReturnDelivery = new List<PPurchaseOrderReturnDeliveryItem_Insert>();

            foreach (GridViewRow row in gvPoReturnItemForDelivery.Rows)
            {
                if (!string.IsNullOrEmpty(((TextBox)row.FindControl("txtDeliveredQty")).Text))
                {
                    poReturnDelivery.Add(new PPurchaseOrderReturnDeliveryItem_Insert()
                    {
                        PurchaseOrderReturnID = Convert.ToInt64(((Label)row.FindControl("lblPurchaseOrderReturnID")).Text),
                        PurchaseOrderReturnItemID = Convert.ToInt64(((Label)row.FindControl("lblPurchaseOrderReturnItemID")).Text),
                        DeliveryQty = Convert.ToInt64(((TextBox)row.FindControl("txtDeliveredQty")).Text),
                        Remarks = txtRemarks.Text.Trim(),
                    });
                }
            }
            return poReturnDelivery;
        }                        
    }
}
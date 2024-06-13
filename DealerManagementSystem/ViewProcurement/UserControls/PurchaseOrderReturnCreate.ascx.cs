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
    public partial class PurchaseOrderReturnCreate : System.Web.UI.UserControl
    {
        private List<string> gvSelected
        {
            get
            {
                if (ViewState["gvSelected"] == null)
                {
                    ViewState["gvSelected"] =  new List<string>();
                }
                return (List<string>)ViewState["gvSelected"];
            }
            set
            {
                ViewState["gvSelected"] = value;
            }
        }
        private List<PGr> GrList
        {
            get
            {
                if (ViewState["GrList"] == null)
                {
                    ViewState["GrList"] = new List<PGr>();
                }
                return (List<PGr>)ViewState["GrList"];
            }
            set
            {
                ViewState["GrList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessagePoReturnCreate.Text = "";
        }
        public void FillMaster()
        {
            fillDealer();
            //fillVendor();
            //FillGetDealerOffice();
            //new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select Division");
           // fillDivision();
            Clear();
        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind(); 
        }
        //void fillVendor()
        //{
        //    ddlVendor.DataTextField = "CodeWithDisplayName";
        //    ddlVendor.DataValueField = "DID";
        //    ddlVendor.DataSource = new BDealer().GetDealerList(null, null, null);
        //    ddlVendor.DataBind();
        //    ddlVendor.Items.Insert(0, new ListItem("Select", "0"));
        //}
        //void fillDivision()
        //{
        //    ddlDivision.Items.Clear();
        //    ddlDivision.Items.Add( new ListItem("Parts", "15"));
        //}
        void Clear()
        {
            gvGr.PageIndex = 0;
            gvGr.DataSource = null;
            gvGr.DataBind();
            txtRemarks.Text = "";
            gvSelected.Clear();
            lblRowCountGR.Visible = false;
            lblMessagePoReturnCreate.Text = "";
            lblMessagePoReturnCreate.Visible = false;
        }
        //protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillGetDealerOffice();
        //}
        //private void FillGetDealerOffice()
        //{
        //    ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
        //    ddlDealerOffice.DataValueField = "OfficeID";
        //    ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null);
        //    ddlDealerOffice.DataBind();
        //    ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        //}
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvGr.DataSource = null;
            gvGr.DataBind();
            string message = Validation();
            if (!string.IsNullOrEmpty(message))
            {
                lblMessagePoReturnCreate.Text = message;
                lblMessagePoReturnCreate.Visible = true;
                lblMessagePoReturnCreate.ForeColor = Color.Red;
                return;
            } 
            //ViewState["VendorID"] = ddlVendor.SelectedValue;
            //ViewState["LocationID"] = ddlDealerOffice.SelectedValue;
            //ViewState["DivisionID"] = ddlDivision.SelectedValue;
              
            PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderAsnGrForPoReturnCreation(Convert.ToInt32(ddlDealer.SelectedValue), null, null, null, txtGrNumber.Text.Trim());
            GrList = JsonConvert.DeserializeObject<List<PGr>>(JsonConvert.SerializeObject(Result.Data));
            gvGr.DataSource = GrList;
            gvGr.DataBind();
            fillPendingGr();
        }
        public string Validation()
        {
            ddlDealer.BorderColor = Color.Silver;
            txtGrNumber.BorderColor = Color.Silver;
            //ddlVendor.BorderColor = Color.Silver;
            //ddlDealerOffice.BorderColor = Color.Silver;
            if (ddlDealer.SelectedValue == "0")
            {
                ddlDealer.BorderColor = Color.Red;
                return "Please select the Dealer.";
            }
            if (string.IsNullOrEmpty(txtGrNumber.Text.Trim()))
            {
                txtGrNumber.BorderColor = Color.Red;
                return "Please select the GR Number.";
            }
            //if (ddlDealerOffice.SelectedValue == "0")
            //{
            //    ddlDealer.BorderColor = Color.Red;
            //    return "Please select the Receiving Location.";
            //}
            return "";
        }
        public void fillPendingGr()
        {
            gvGr.DataSource = GrList;
            gvGr.DataBind();
            foreach (GridViewRow row in gvGr.Rows)
            {
                CheckBox cbIsChecked = (CheckBox)row.FindControl("cbIsChecked");
                Label lblGrItemID = (Label)row.FindControl("lblGrItemID"); 
                if (gvSelected.Contains(lblGrItemID.Text))
                {
                    cbIsChecked.Checked = true;
                }
            }
            if (GrList.Count == 0)
            {
                lblRowCountGR.Visible = false;
                divPendingGR.Visible = false;
            }
            else
            {
                lblRowCountGR.Visible = true;
                lblRowCountGR.Text = (((gvGr.PageIndex) * gvGr.PageSize) + 1) + " - " + (((gvGr.PageIndex) * gvGr.PageSize) + gvGr.Rows.Count) + " of " + GrList.Count;
                divPendingGR.Visible = true;
            }
        }         
        public List<PPurchaseOrderReturnItem_Insert> Read()
        {
            List<PPurchaseOrderReturnItem_Insert> pGrItem = new List<PPurchaseOrderReturnItem_Insert>();
            selectedGv();
            if (gvSelected.Count == 0)
            {
                lblMessagePoReturnCreate.Visible = true; 
                lblMessagePoReturnCreate.Text = "Please select the GR.";
                lblMessagePoReturnCreate.ForeColor = Color.Red;
            }
            foreach (string Item in gvSelected)
            { 
                    pGrItem.Add(new PPurchaseOrderReturnItem_Insert()
                    {
                        GrID = GrList[0].GrID,
                        GrItemID = Convert.ToInt64(Item),
                        Remarks = txtRemarks.Text.Trim(),
                    });
              
            }
            return pGrItem;
        }    
        void selectedGv()
        {
            foreach (GridViewRow row in gvGr.Rows)
            {
                CheckBox cbIsChecked = (CheckBox)row.FindControl("cbIsChecked");
                Label lblGrItemID = (Label)row.FindControl("lblGrItemID");
                if (cbIsChecked.Checked)
                {
                    if (!gvSelected.Contains(lblGrItemID.Text))
                    {
                        gvSelected.Add(lblGrItemID.Text);
                    }
                }
                else
                {
                    if (gvSelected.Contains(lblGrItemID.Text))
                    {
                        gvSelected.Remove(lblGrItemID.Text);
                    }
                }
            }
        }
        protected void gvGr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGr.PageIndex = e.NewPageIndex;
            selectedGv();
            fillPendingGr();
        }
    }
}
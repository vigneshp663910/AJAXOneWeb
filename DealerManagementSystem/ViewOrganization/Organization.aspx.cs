using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewOrganization
{
    public partial class Organization : BasePage
    {
        //public int DealerID
        //{
        //    get
        //    {
        //        if (Session["DealerID"] == null)
        //        {
        //            Session["DealerID"] = 0;
        //        }
        //        return Convert.ToInt32(Session["DealerID"]);
        //    }
        //    set
        //    {
        //        Session["DealerID"] = value;
        //    }
        //}
        //public int? DealerDepartmentID
        //{
        //    get
        //    {
        //        if (Session["DealerDepartmentID"] == null)
        //        {
        //            return null;
        //        }
        //        return Convert.ToInt32(Session["DealerDepartmentID"]);
        //    }
        //    set
        //    {
        //        Session["DealerDepartmentID"] = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Organisation » Actual');</script>");

            if (!IsPostBack)
            {
                 
                fillDealer();
                
                AjaxTree();
                
                SalesTree();
                PartsTree();
                ServiceTree();
            }
        }

        private void AjaxTree()
        { 
            List<PDealerEmployee> Ajax = new BOrganization().GetOrganization(null, 53, null);
            foreach (PDealerEmployee c in Ajax)
            {
                TreeNode t = new TreeNode(c.EmployeeName, c.EmpId.ToString());
                t.PopulateOnDemand = true;
                tvAjax.Nodes.Add(t);
            }
        }
       
       
        private void SalesTree()
        { 
            List<PDealerEmployee> Sales =  new BOrganization().GetOrganization(null, 53, 1);
            foreach (PDealerEmployee c in Sales)
            {
                TreeNode t = new TreeNode(c.EmployeeName, c.EmpId.ToString());
                t.PopulateOnDemand = true;
                tvSales.Nodes.Add(t);
            }
        }
        private void PartsTree()
        { 
            List<PDealerEmployee> Sales =  new BOrganization().GetOrganization(null, 53, 3);
            foreach (PDealerEmployee c in Sales)
            {
                TreeNode t = new TreeNode(c.EmployeeName, c.EmpId.ToString());
                t.PopulateOnDemand = true;
                tvParts.Nodes.Add(t);
            }
        }
        private void ServiceTree()
        { 
            List<PDealerEmployee> Sales = new BOrganization().GetOrganization(null, 53, 2);
            foreach (PDealerEmployee c in Sales)
            {
                TreeNode t = new TreeNode(c.EmployeeName, c.EmpId.ToString());
                t.PopulateOnDemand = true;
                tvService.Nodes.Add(t);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Home.aspx");
        }
      
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void ddlDealerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvDealer.Nodes.Clear();  
            DealerTree();
            tvDealer.ExpandAll();
        }
        private void DealerTree()
        {
            List<PDealerEmployee> Employee = new BOrganization().GetOrganization(null, Convert.ToInt32(ddlDealerCode.SelectedValue), null);
            foreach (PDealerEmployee c in Employee)
            {
                TreeNode t = new TreeNode(c.EmployeeName, c.EmpId.ToString());
                t.PopulateOnDemand = true;
                tvDealer.Nodes.Add(t);
            }
        }

        protected void tvAjax_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        { 
            TreeNode main = e.Node;
            FillChildNodes(main, 53, null);
        }

        protected void tvDealer_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            TreeNode main = e.Node;
            FillChildNodes(main, Convert.ToInt32(ddlDealerCode.SelectedValue), null);
        }

        protected void tvSales_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            TreeNode main = e.Node;
            FillChildNodes(main, 53, 1);
        }

        protected void tvParts_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            TreeNode main = e.Node;
            FillChildNodes(main, 53, 3);
        }

        protected void tvService_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            TreeNode main = e.Node;
            FillChildNodes(main, 53, 2);
        }
        void FillChildNodes(TreeNode main, int? DealerID, int? DealerDepartmentID)
        {
            int DealerEmployeeID = Convert.ToInt32(main.Value);

            List<PDealerEmployee> Employee = new BOrganization().GetOrganization(DealerEmployeeID, DealerID, DealerDepartmentID);
            foreach (PDealerEmployee s in Employee)
            {
                if (DealerEmployeeID != s.EmpId)
                {
                    TreeNode sub = new TreeNode(s.EmployeeName, s.EmpId.ToString());
                    sub.PopulateOnDemand = true;
                    main.ChildNodes.Add(sub);
                }
            }
        }
    }
}
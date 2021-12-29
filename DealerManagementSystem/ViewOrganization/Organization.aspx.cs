using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewOrganization
{
    public partial class Organization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCountry();
            }
        }

        private void PopulateCountry()
        {
            List<PDealerEmployee> allCountry = new List<PDealerEmployee>();
            allCountry = new BOrganization().GetOrganization(1);
            foreach (PDealerEmployee c in allCountry)
            {
                TreeNode t = new TreeNode(c.EmployeeName, c.EmpId.ToString());
                t.PopulateOnDemand = true;
                TreeView1.Nodes.Add(t);
            }
        }

        protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {

            // This code is for populate Child nodes
            TreeNode main = e.Node;
            int countryID = Convert.ToInt32(main.Value);
            List<PDealerEmployee> allState = new List<PDealerEmployee>();

            allState = new BOrganization().GetOrganization(1);
           

            foreach (PDealerEmployee s in allState)
            {
                TreeNode sub = new TreeNode(s.EmployeeName, s.EmpId.ToString());
                sub.PopulateOnDemand = true;
                main.ChildNodes.Add(sub);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Home.aspx");
        }
    }
}
using Business;
using Properties;
using System;
using System.Collections.Generic;
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
            TreeNode t = new TreeNode(PSession.User.ContactName, Convert.ToString(PSession.User.UserID));
            t.PopulateOnDemand = true;
            TreeView1.Nodes.Add(t);
            List<PDealerEmployee> allCountry = new List<PDealerEmployee>();
            allCountry = new BOrganization().GetOrganization(PSession.User.UserID);
            foreach (PDealerEmployee c in allCountry)
            {
                TreeNode sub = new TreeNode(c.EmployeeName, c.EmpId.ToString());
                sub.PopulateOnDemand = true;
                t.ChildNodes.Add(sub);
            }

        }

        protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {

            // This code is for populate Child nodes
            TreeNode main = e.Node;
            int countryID = Convert.ToInt32(main.Value);
            List<PDealerEmployee> allState = new List<PDealerEmployee>();

            allState = new BOrganization().GetOrganization(countryID);


            foreach (PDealerEmployee s in allState)
            {
                if (countryID != s.EmpId)
                {
                    TreeNode sub = new TreeNode(s.EmployeeName, s.EmpId.ToString());
                    sub.PopulateOnDemand = true;
                    main.ChildNodes.Add(sub);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Home.aspx");
        }
    }
}
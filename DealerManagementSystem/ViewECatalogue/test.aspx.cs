using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewECatalogue
{
    public partial class test :BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewECatalogue_SpcAssembly; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = GetDivision();
            this.PopulateTreeView(dt, "0", null);
        }
    }
    private void PopulateTreeView(DataTable dtParent, string parentId, TreeNode treeNode)
    {
        foreach (DataRow row in dtParent.Rows)
        {
            TreeNode child = new TreeNode
            {
                Text = row["Name"].ToString(),
                Value = row["Id"].ToString()
            };
            TreeNode child1 = new TreeNode();
            if (parentId == "0")
            {
                TreeView1.Nodes.Add(child);
                DataTable dtChild = GetModel(Convert.ToInt32(child.Value));
                PopulateTreeView(dtChild, child.Value, child);
            }
            else
            {
                DataTable dtChild1 = GetAssembly(Convert.ToInt32(child.Value));
                foreach (DataRow row1 in dtChild1.Rows)
                {
                    child1 = new TreeNode()
                    {
                        Text = row1["Name"].ToString(),
                        Value = row1["Id"].ToString()
                    };
                    child.ChildNodes.Add(child1);
                }
                treeNode.ChildNodes.Add(child);
            }
        }
    }
    DataTable GetDivision()
    {
        List<PDMS_Division> Division = new BDMS_Master().GetDivision(null, null);
        DataTable dt = new DataTable();
        dt.Columns.Add("Id");
        dt.Columns.Add("Name");
        foreach (PDMS_Division div in Division)
        {
            dt.Rows.Add(div.DivisionID, div.DivisionCode);
        }
        return dt;
    }

    DataTable GetModel(int DivisionID)
    {
        // DataTable dtChild = this.GetData("SELECT Id=PMCode, Name=PMDescription FROM sfTmProductModel WHERE PGCode = '" + child.Value + "' AND PMCode like '" + ls_model + "' AND Active = 'Y' AND Publish = 'Y' AND Purpose in ( 'C', 'B' ) ORDER BY SlNo");
        List<PSpcModel> Model = new BECatalogue().GetSpcModel(null, DivisionID, null, true, null, null, null);
        DataTable dt = new DataTable();
        dt.Columns.Add("Id");
        dt.Columns.Add("Name");
        foreach (PSpcModel M in Model)
        {
            if (M.IsPublish == true && (M.Purpose == "C" || M.Purpose == "B"))
            {
                dt.Rows.Add(M.SpcModelID, M.SpcModel);
            }
        }
        return dt;
    }

    DataTable GetAssembly(int ModelID)
    {
        PApiResult Result = new BECatalogue().GetSpcAssembly(null, ModelID, null, null, true, 0, null, null);
        List<PSpcAssembly> Assembly = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data));
        DataTable dt = new DataTable();
        dt.Columns.Add("Id");
        dt.Columns.Add("Name");
        foreach (PSpcAssembly M in Assembly)
        {
            if (M.SlNo >= 0)
            {
                dt.Rows.Add(M.SpcAssemblyID, M.AssemblyDescription);
            }
        }
        return dt;
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {

        //if (TreeView1.SelectedNode.Depth == 2)
        //{
        //    UC_SpcAssemblyView.Clear();
        //    UC_SpcAssemblyView.fillParts(Convert.ToInt32(TreeView1.SelectedNode.Value));
        //}
    }
}
}
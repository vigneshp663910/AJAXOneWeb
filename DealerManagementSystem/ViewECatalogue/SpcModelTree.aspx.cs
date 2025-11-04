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
    public partial class SpcModelTree : System.Web.UI.Page
    {
        List<PSpcModel> Model = new List<PSpcModel>();
        List<PSpcAssembly> Assembly = new List<PSpcAssembly>();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //ls_pg = Request.QueryString["ProductGroup"];
            //ls_model = Request.QueryString["Model"];
            //ls_mcno = Request.QueryString["McNo"];

            //Session["SelectedeCatProductGroup"] = ls_pg;

            //if (ls_mcno != "XXXXXXXXXXXX")
            //{
            //    GetProductGroupModel(ls_mcno);
            //    Session["SelectedeCatProductGroup"] = ls_pg;
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Model = new BECatalogue().GetSpcModel(null, null, null, true, null, null, null);
                PApiResult Result = new BECatalogue().GetSpcAssembly(null, null, null, null, true, 0, null, null);
                Assembly = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data));
                  
                DataTable dt1 = GetProductGroup(null, null);

                this.PopulateTreeView(dt1, "0", null);
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
                    // DataTable dtChild = this.GetData("SELECT Id=PMCode, Name=PMDescription FROM sfTmProductModel WHERE PGCode = '" + child.Value + "' AND PMCode like '" + ls_model + "' AND Active = 'Y' AND Publish = 'Y' AND Purpose in ( 'C', 'B' ) ORDER BY SlNo");
                    DataTable dtChild = GedModel(Convert.ToInt32(child.Value));

                    PopulateTreeView(dtChild, child.Value, child);
                }
                else
                {
                    //  DataTable dtChild1 = this.GetData("SELECT Id = AssyCode, Name = AssyDescription from enTmSPContents where PMCode = '" + child.Value + "' AND SlNo >= 0 AND Active = 'Y' ORDER BY SlNo");
                    DataTable dtChild1 = GedAssembly(Convert.ToInt32(child.Value));
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
 

        //private DataTable GetData(string query)
        //{
        //    DataTable dt = new DataTable();
        //    string constr = AppVariable.gsConnection_MRPINVENT;

        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            using (SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                cmd.CommandType = CommandType.Text;
        //                cmd.Connection = con;
        //                sda.SelectCommand = cmd;
        //                sda.Fill(dt);
        //            }
        //        }
        //        return dt;
        //    }
        //} 

        void GetProductGroupModel(string ls_mcno)
        {
            //string ls_sql;
            //ls_sql = "SELECT FGCode, FGDescription, MachineGroup, ProductOrderNo, OrderDate,BasicStartDate,BasicEndDate FROM sfTiProductionOrderHeader WHERE MachineSerialNo = '" + ls_mcno + "' ORDER BY MachineSerialNo DESC";
            //DataTable dtProductGroup = this.GetData(ls_sql); 
            //ls_model = dtProductGroup.Rows[0]["MachineGroup"].ToString();

            //ls_sql = "SELECT PGCode FROM sfTmProductModel WHERE PMCode = '" + ls_model + "'";
            //DataTable dtModel = this.GetData(ls_sql);

            //ls_pg = dtModel.Rows[0]["PGCode"].ToString();

        }

        DataTable GetProductGroup(int? SpcProductGroupID, string PGCode)
        {

            List<PSpcProductGroup> ProductGroup = new BECatalogue().GetSpcProductGroup(SpcProductGroupID, PGCode, true);           
            
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            foreach (PSpcProductGroup div in ProductGroup)
            {
                dt.Rows.Add(div.SpcProductGroupID, div.PGSCodePGDescription);
            }
            return dt;
        }

        DataTable GedModel(int SpcProductGroupID)
        { 
            List<PSpcModel> Model_ = Model.Where(e => e.ProductGroup.SpcProductGroupID == SpcProductGroupID).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            foreach (PSpcModel M in Model_)
            {
                if (M.IsPublish == true && (M.Purpose == "C" || M.Purpose == "B"))
                {
                    dt.Rows.Add(M.SpcModelID, M.SpcModel);
                }
            }
            return dt;
        }

        DataTable GedAssembly(int ModelID)
        {
            //PApiResult Result = new BECatalogue().GetSpcAssembly(null, ModelID, null, null, true, 0, null, null);
            //List<PSpcAssembly> Assembly = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data));

            List<PSpcAssembly> Assembly_ = Assembly.Where(e => e.SpcModel.SpcModelID == ModelID).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            foreach (PSpcAssembly M in Assembly_)
            {
                if (M.SlNo >= 0)
                {
                    dt.Rows.Add(M.SpcAssemblyID, M.AssemblyDescription);
                }
            }
            return dt;
        }

        protected void txtEquipment_TextChanged(object sender, EventArgs e)
        {
            TreeView1.Nodes.Clear();
            if (!string.IsNullOrEmpty(txtEquipment.Text.Trim()))
            {
                List<PDMS_EquipmentHeader> Equipment = new BDMS_Equipment().GetEquipment(null, txtEquipment.Text.Trim());
                if (Equipment.Count == 1)
                { 
                    Model = new BECatalogue().GetSpcModel(null, null, Equipment[0].EquipmentModel.ModelCode, true, null, null, null);
                    PApiResult Result = new BECatalogue().GetSpcAssembly(null, null, null, null, true, 0, null, null);
                    Assembly = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data));
                    DataTable dt1 = GetProductGroup(null,Equipment[0].EquipmentModel.Division.DivisionCode);
                    this.PopulateTreeView(dt1, "0", null);
                } 
            }
            else
            {                
                Model = new BECatalogue().GetSpcModel(null, null, null, true, null, null, null);
                PApiResult Result = new BECatalogue().GetSpcAssembly(null, null, null, null, true, 0, null, null);
                Assembly = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data));
                DataTable dt1 = GetProductGroup(null, null);
                this.PopulateTreeView(dt1, "0", null);
            }
        }
    }
}


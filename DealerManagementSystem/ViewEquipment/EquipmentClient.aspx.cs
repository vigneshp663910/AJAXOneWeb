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

namespace DealerManagementSystem.ViewEquipment
{
    public partial class EquipmentClient : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewEquipment_EquipmentClient; } }
        public List<PEquipmentClient> Client
        {
            get
            {
                if (ViewState["Client"] == null)
                {
                    ViewState["Client"] = new List<PEquipmentClient>();
                }
                return (List<PEquipmentClient>)ViewState["Client"];
            }
            set
            {
                ViewState["Client"] = value;
            }
        }
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "EquipmentClient.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Equipment » Equipment Client');</script>");
            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
                fillEquipmentClient();
            }
        }
        protected void btnSearchEquipmentClient_Click(object sender, EventArgs e)
        {
            try
            {
                fillEquipmentClient();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillEquipmentClient()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                PApiResult Result = new BDMS_Equipment().GetEquipmentClient(null, txtSClient.Text.Trim(), true, PageIndex, gvClient.PageSize);
                Client = JsonConvert.DeserializeObject<List<PEquipmentClient>>(JsonConvert.SerializeObject(Result.Data));
                gvClient.PageIndex = 0;
                gvClient.DataSource = Client;
                gvClient.DataBind();

                if (Result.RowCount == 0)
                {
                    PEquipmentClient pEquipmentClient = new PEquipmentClient();
                    Client.Add(pEquipmentClient);
                    gvClient.DataSource = Client;
                    gvClient.DataBind();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    gvClient.DataSource = Client;
                    gvClient.DataBind();
                    
                    PageCount = (Result.RowCount + gvClient.PageSize - 1) / gvClient.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvClient.PageSize) + 1) + " - " + (((PageIndex - 1) * gvClient.PageSize) + gvClient.Rows.Count) + " of " + Result.RowCount;

                    List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                    if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EquipmentClientAddEditDelete).Count() == 0)
                    {
                        for (int i = 0; i < gvClient.Columns.Count; i++)
                        {
                            if (gvClient.Columns[i].HeaderText == "Action")
                            {
                                gvClient.Columns[i].Visible = false;
                                gvClient.FooterRow.Visible = false;
                            }
                        }
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("EquipmentClient", "fillEquipmentClient", e1);
                throw e1;
            }
        }

        protected void lblClientEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lblClientEdit = (LinkButton)sender;
                TextBox txtClient = (TextBox)gvClient.FooterRow.FindControl("txtClient");
                Button BtnAddClient = (Button)gvClient.FooterRow.FindControl("BtnAddClient");
                GridViewRow row = (GridViewRow)(lblClientEdit.NamingContainer);
                string lblClient = ((Label)row.FindControl("lblClient")).Text.Trim();
                txtClient.Text = lblClient;
                HiddenID.Value = Convert.ToString(lblClientEdit.CommandArgument);
                BtnAddClient.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lblClientDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lblClientDelete = (LinkButton)sender;
                int EquipmentClientID = Convert.ToInt32(lblClientDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lblClientDelete.NamingContainer);
                string lblClient = ((Label)row.FindControl("lblClient")).Text.Trim();

                PEquipmentClient EquipmentClient = new PEquipmentClient();
                EquipmentClient.EquipmentClientID = EquipmentClientID;
                EquipmentClient.Client = lblClient;
                EquipmentClient.IsActive = false;

                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Equipment/InsertOrUpdateEquipmentClient", EquipmentClient));

                if (result.Status == PApplication.Failure)
                {
                    lblMessage.Text = result.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                lblMessage.Text = result.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                fillEquipmentClient();
                //Boolean Result = new BDMS_Equipment().InsertOrUpdateEquipmentClient(EquipmentClient, PSession.User.UserID);
                //if (Result)
                //{
                //    lblMessage.ForeColor = Color.Green;
                //    lblMessage.Text = "Client is Deleted Successfully...";
                //    lblMessage.Visible = true;
                //    fillEquipmentClient();
                //}
                //else
                //{
                //    lblMessage.ForeColor = Color.Red;
                //    lblMessage.Text = "Client is Not Deleted..!";
                //    lblMessage.Visible = true;
                //    return;
                //}
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void BtnAddClient_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Button BtnAddClient = (Button)gvClient.FooterRow.FindControl("BtnAddClient");

                string txtClient = ((TextBox)gvClient.FooterRow.FindControl("txtClient")).Text.Trim();
                if (string.IsNullOrEmpty(txtClient))
                {
                    lblMessage.Text = "Please Enter Client";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                PEquipmentClient EquipmentClient = new PEquipmentClient();
                if (BtnAddClient.Text == "Update")
                {
                    EquipmentClient.EquipmentClientID = Convert.ToInt32(HiddenID.Value);
                }
                EquipmentClient.Client = txtClient;
                EquipmentClient.IsActive = true;

                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Equipment/InsertOrUpdateEquipmentClient", EquipmentClient));

                if (result.Status == PApplication.Failure)
                {
                    lblMessage.Text = result.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                lblMessage.Text = result.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                fillEquipmentClient();
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                return;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillEquipmentClient();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillEquipmentClient();
            }
        }

        protected void gvClient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClient.PageIndex = e.NewPageIndex;
            fillEquipmentClient();
        }
    }
}
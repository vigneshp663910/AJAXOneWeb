<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.WebForm1" %> 

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            width: 170px;
            height: 50px;
            font: 20px;
        }

        .ajax__tab_xp .ajax__tab_header {
            background-position: bottom;
            background-repeat: repeat-x;
            font-family: verdana,tahoma,helvetica;
            font-size: 12px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp1:TabContainer ID="tbpOrgChart" runat="server">
        <asp1:TabPanel ID="tbpnlAjaxOrg" runat="server" HeaderText="Lead List" ToolTip="Lead List">
            <ContentTemplate>
                <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Country</legend>
                    <div class="col-md-12">

                        <div class="col-md-2 text-right">
                            <label>Lead Number</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtLeadNumber" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-2 text-right">
                            <label>Lead Date From</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtLeadDateFrom" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            <asp1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtLeadDateFrom" PopupButtonID="txtLeadDateFrom" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                        </div>

                        <div class="col-md-2 text-right">
                            <label>Lead Date To</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtLeadDateTo" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            <asp1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtLeadDateTo" PopupButtonID="txtLeadDateTo" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Progress Status</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlSProgressStatus" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-2 text-right">
                            <label>Status</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlSStatus" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-2 text-right">
                            <label>Category</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlSCategory" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-2 text-right">
                            <label>Qualification</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlSQualification" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Source</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlSSource" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-2 text-right">
                            <label>Lead Type</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlSType" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-2 text-right">
                            <label>Customer Code</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtSCustomerCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Country</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSCountry_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 text-right">
                            <label>State</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlSState" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-2">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                        </div>
                    </div>
                </fieldset>
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvLead" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lead Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeadNumber" Text='<%# DataBinder.Eval(Container.DataItem, "LeadNumber")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lead Date" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeadDate" Text='<%# DataBinder.Eval(Container.DataItem, "LeadDate")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category.Category")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Progress Status" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProgressStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ProgressStatus.ProgressStatus")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Progress Status" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQualification" Text='<%# DataBinder.Eval(Container.DataItem, "Qualification.Qualification")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSource" Text='<%# DataBinder.Eval(Container.DataItem, "Source.Source")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblType" Text='<%# DataBinder.Eval(Container.DataItem, "Type.Type")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Code" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Code" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control" Width="70px" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>Action</asp:ListItem>
                                            <asp:ListItem>Edit Lead</asp:ListItem>
                                            <asp:ListItem>Edit Financial Info</asp:ListItem>
                                            <asp:ListItem>Assign</asp:ListItem>
                                            <asp:ListItem>Customer Convocation</asp:ListItem>
                                            <asp:ListItem>Convert to Prospect</asp:ListItem>
                                            <asp:ListItem>Lost Lead</asp:ListItem>
                                            <asp:ListItem>Cancel Lead</asp:ListItem>
                                            <asp:ListItem>Add Effort</asp:ListItem>
                                            <asp:ListItem>Add Expense</asp:ListItem>
                                            <asp:ListItem>Add Follow-up</asp:ListItem>
                                            <asp:ListItem>View Lead</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#f2f2f2" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>
      
    </asp1:TabContainer>



    <asp:Panel ID="Panel111" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix"><span id="PopupDialogue">Log-In</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"><asp:Button ID="PopupClose" runat="server" Text="X" CssClass="PopupClose" /></a></div>
        <div class="col-md-12">
            <div class="col-md-3 text-right">
                <label>Sales Engineer</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-5 text-center">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn Back" />
                <asp:Button ID="btnAssignSalesEngineer" runat="server" Text="LogIn" CssClass="btn Save" OnClick="btnAssignSalesEngineer_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton ID="lnkLoginbtn" runat="server">Login</asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="MP_AssignSalesEngineer" runat="server"
        TargetControlID="lnkLoginbtn"
        PopupControlID="Panel111"
        BackgroundCssClass="modalBackground"
        DropShadow="true"
        OkControlID="btnAssignSalesEngineer"
        OnOkScript="ok()"
        CancelControlID="btnCancel" />


</asp:Content>
 



<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ColdVisits.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.ColdVisits" %>

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

        .Popup {
            display: block;
            z-index: 1002;
            outline: 0px;
            height: auto;
            width: 800px;
            top: 128px;
            left: 283px;
            position: absolute;
            padding: 0.2em;
            overflow: hidden;
            border-radius: 6px;
            border: 1px solid #CCC;
            background: #fefefe 50% bottom repeat-x;
            color: #666;
            font-family: Segoe UI,Arial,sans-serif;
            font-size: 1.1em;
            margin: 0 1% 0 1%;
        }

        .PopupHeader {
            border: 1px solid #333;
            background: #333 url(Ajax/Images/Feedbackheader.png) 50% 50% repeat-x;
            color: #fff;
            font-weight: bold;
            cursor: move;
            padding: 0.4em 1em;
            position: relative;
            border-radius: 6px;
            font-family: Segoe UI,Arial,sans-serif;
            font-size: 1.1em;
        }

        .clearfix:after {
            content: ".";
            display: block;
            height: 0;
            clear: both;
            visibility: hidden;
        }

        .PopupHeader a {
            color: #fff;
        }

        #PopupDialogue {
            float: left;
            font-size: 13px;
            font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
        }

        .PopupClose {
            float: right;
            color: black;
            font-size: 8px;
            width: 15px;
            height: 15px;
            padding: inherit;
        }

        .modal-backdrop {
            background-color: gray;
        }

        .modalBackground {
            background-color: #000000bd;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

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
                <asp:TextBox ID="txtLeadDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>

            </div>

            <div class="col-md-2 text-right">
                <label>Lead Date To</label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txtLeadDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>

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
                <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSCountry_SelectedIndexChanged" AutoPostBack="true"  />
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
                            <asp:Label ID="lblLeadID" Text='<%# DataBinder.Eval(Container.DataItem, "LeadID")%>' runat="server" Visible="false" />
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
                                <asp:ListItem>View Lead</asp:ListItem>
                                <asp:ListItem>Edit Lead</asp:ListItem>
                                <asp:ListItem>Convert to Prospect</asp:ListItem>
                                <asp:ListItem>Lost Lead</asp:ListItem>
                                <asp:ListItem>Cancel Lead</asp:ListItem>
                                <asp:ListItem>Assign</asp:ListItem>
                                <asp:ListItem>Add Follow-up</asp:ListItem>
                                <asp:ListItem>Customer Convocation</asp:ListItem>
                                <asp:ListItem>Edit Financial Info</asp:ListItem>
                                <asp:ListItem>Add Effort</asp:ListItem>
                                <asp:ListItem>Add Expense</asp:ListItem>
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

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

    <asp:Panel ID="pnlCustomer" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Sales Engineer Assign </span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="col-md-12">
            </div>
        </div>

    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="MPE_Customer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
 
</asp:Content>

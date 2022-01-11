<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ColdVisits.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.ColdVisits" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewPreSale/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Src="~/ViewPreSale/UserControls/Effort.ascx" TagPrefix="UC" TagName="UC_Effort" %>
<%@ Register Src="~/ViewPreSale/UserControls/Expense.ascx" TagPrefix="UC" TagName="UC_Expense" %>
<%@ Register Src="~/ViewPreSale/UserControls/CustomerView.ascx" TagPrefix="UC" TagName="UC_CustomerView" %>
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

        .portlet.box.green {
            border: 1px solid #5cd1db;
            border-top: 0;
        }

            .portlet.box.green > .portlet-title {
                background-color: #32c5d2;
            }

                .portlet.box.green > .portlet-title > .caption {
                    color: #fff;
                }

        .pull-right {
            float: right !important;
        }

        .btn:not(.md-skip):not(.bs-select-all):not(.bs-deselect-all).btn-sm {
            font-size: 11px;
            padding: 6px 18px 6px 18px;
        }

        .btn.yellow:not(.btn-outline) {
            color: #fff;
            background-color: #c49f47;
            border-color: #c49f47;
        }

        .form-group {
            margin-bottom: 5px;
        }

        b, optgroup, strong {
            font-weight: 700;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 text-right">
                            <label>Date From</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>

                        <div class="col-md-2 text-right">
                            <label>Date To</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>

                        <div class="col-md-2 text-right">
                            <label>Customer</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Mobile</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Country</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 text-right">
                            <label>State</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                            <asp:Button ID="btnAddColdVisit" runat="server" CssClass="btn Save" Text="Add Cold Visit" OnClick="btnAddColdVisit_Click" Width="150px"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12">
                <div class="col-md-12 Report">
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
                                    <asp:TemplateField HeaderText="Cold Visit No">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblColdVisitID" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblColdVisitNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitNumber")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cold Visit Date">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblColdVisitDate" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitDate")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action Type">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblActionType" Text='<%# DataBinder.Eval(Container.DataItem, "ActionType.ActionType")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbViewCustomer" runat="server" OnClick="lbViewCustomer_Click">
                                                <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server" />
                                            </asp:LinkButton><asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.ContactPerson")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.Mobile")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMail">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEMail" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.EMail")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control" Width="70px" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem>Action</asp:ListItem><asp:ListItem>Add Effort</asp:ListItem><asp:ListItem>Add Expense</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField></Columns><AlternatingRowStyle BackColor="#f2f2f2" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </fieldset>
                 </div></div></div><div class="col-md-12" id="divCustomerView" runat="server" visible="false">
            <div class="portlet box green" style="border: none; margin-bottom: 5px;">
                <div class="portlet-title">
                    <div class="caption" style="width: 100%">
                        <span>View Company Customer Details</span> <div class="pull-right">
                            <asp:Button ID="btnEditCustomer" runat="server" Text="Edit Customer" Style="margin-top: 5px;" class="btn btn-sm btn-primary" />
                            <button class="btn btn-sm btn-primary" href="#" onclick="ViewCustomerAudits('6750');" style="margin-top: 5px;">View Audit Trails</button>&nbsp;<button class="btn btn-sm yellow" onclick="history.go(-1)" style="margin-top: 5px;">Back</button></div></div></div><div class="col-md-12" runat="server" id="tblDashboard">

                    <UC:UC_CustomerView ID="UC_CustomerView" runat="server"></UC:UC_CustomerView>
                    <asp:PlaceHolder ID="ph_usercontrols_1" runat="server"></asp:PlaceHolder>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
                    </div>
                </div>
            </div>
        </div>



        <div class="col-md-12">
            <asp:Panel ID="pnlCustomer" runat="server" CssClass="Popup" Style="display: none">
                <div class="PopupHeader clearfix">
                    <span id="PopupDialogue">Add Cold Visit</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"> <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
                </div>
                <div class="col-md-12">

                    <UC:UC_CustomerCreate ID="UC_Customer" runat="server"></UC:UC_CustomerCreate>
                    <fieldset class="fieldset-border">
                        <div class="col-md-12">

                            <div class="col-md-2 text-right">
                                <label>Cold Visit Date</label> </div><div class="col-md-4">
                                <asp:TextBox ID="txtColdVisitDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox></div><div class="col-md-2 text-right">
                                <label>Action Type</label> </div><div class="col-md-4">
                                <asp:DropDownList ID="ddlActionType" runat="server" CssClass="form-control" />
                            </div>

                            <div class="col-md-2 text-right">
                                <label>Remark</label> </div><div class="col-md-10">
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox></div></div></fieldset> <div class="col-md-12 text-center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" />
                    </div>
                </div>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="MPE_Customer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

            <asp:Panel ID="pnlEffort" runat="server" CssClass="Popup" Style="display: none">
                <div class="PopupHeader clearfix">
                    <span id="PopupDialogue">Cold Visit Effort </span><a href="#" role="button"><asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
                </div>
                <div class="col-md-12">

                    <UC:UC_Effort ID="UC_Effort" runat="server"></UC:UC_Effort>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSaveEffort" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveEffort_Click" />
                    </div>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvEffort" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Sales Engineer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effort Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEffortType" Text='<%# DataBinder.Eval(Container.DataItem, "EffortType.EffortType")%>' runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effort Date" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEffortDate" Text='<%# DataBinder.Eval(Container.DataItem, "EffortDate")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effort Start Time" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEffortStartTime" Text='<%# DataBinder.Eval(Container.DataItem, "EffortStartTime")%>' runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effort End Time" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEffortEndTime" Text='<%# DataBinder.Eval(Container.DataItem, "EffortEndTime")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effort" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEffort" Text='<%# DataBinder.Eval(Container.DataItem, "Effort")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
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
                </div>
            </asp:Panel>

            <ajaxToolkit:ModalPopupExtender ID="MPE_Effort" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEffort" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

            <asp:Panel ID="pnlExpense" runat="server" CssClass="Popup" Style="display: none">
                <div class="PopupHeader clearfix">
                    <span id="PopupDialogue">Cold Visit Expense</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"> <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" />
                    </a>
                </div>
                <div class="col-md-12">
                    <UC:UC_Expense ID="UC_Expense" runat="server"></UC:UC_Expense>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSaveExpense" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveExpense_Click" />
                    </div>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
                            <Columns>
                                <%-- <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
            </ItemTemplate>
        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Sales Engineer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expense Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpenseType" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseType.ExpenseType")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expense Date" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpenseDate" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseDate")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
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
                </div>
            </asp:Panel>

            <ajaxToolkit:ModalPopupExtender ID="MPE_Expense" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlExpense" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
        </div>
    </div>
    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

</asp:Content>

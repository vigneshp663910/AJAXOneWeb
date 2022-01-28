<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ManageLeadFoloowUps.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.ManageLeadFoloowUps" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                            <label>State</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlFollow" runat="server" CssClass="form-control">
                                <asp:ListItem>Today's Follow-ups</asp:ListItem>
                                <asp:ListItem>Future Follow-ups</asp:ListItem>
                                <asp:ListItem>Completed Follow-ups</asp:ListItem>
                                <asp:ListItem>Cancelled Follow-ups</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>State</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                <asp:ListItem>My Own Follow-ups</asp:ListItem>
                                <asp:ListItem>Assigned Follow-ups</asp:ListItem>
                                <asp:ListItem>Team Follow-ups</asp:ListItem>
                            </asp:DropDownList>
                        </div>

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
                            <div id="divAuto" style="position: absolute; background-color: red; z-index: 1;">
                                <div id="div1" class="fieldset-borderAuto" style="display: none">
                                </div>
                                <div id="div2" class="fieldset-borderAuto" style="display: none">
                                </div>
                                <div id="div3" class="fieldset-borderAuto" style="display: none">
                                </div>
                                <div id="div4" class="fieldset-borderAuto" style="display: none">
                                </div>
                                <div id="div5" class="fieldset-borderAuto" style="display: none">
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Mobile</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>


                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvFollowUp" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Engineer">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Follow Up Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFollowUpDate" Text='<%# DataBinder.Eval(Container.DataItem, "FollowUpDate")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Follow Up Note">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "FollowUpNote")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Lead Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.LeadNumber")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lead Date" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadDate" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.LeadDate")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Category.Category")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Progress Status" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProgressStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.ProgressStatus.ProgressStatus")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qualification" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQualification" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Qualification.Qualification")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Source" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSource" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Source.Source")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Status.Status")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Type.Type")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer Code" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Dealer.DealerCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
        <div>
        </div>
    </div>


</asp:Content>


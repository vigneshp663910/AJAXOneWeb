<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Lead.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Lead" %>

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
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpDealerOrg" runat="server" HeaderText="Lead Create">
            <ContentTemplate>
                <div class="col-md-12">

                    <div class="col-md-12">
                        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Customer</legend>

                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>New Customer</label>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="cbNewCustomer" runat="server" OnCheckedChanged="cbNewCustomer_CheckedChanged" AutoPostBack="true" CssClass="form-control"></asp:CheckBox>
                                </div>
                                <div id="pnlOldCustomerName" class="col-md-12" runat="server" visible="true">
                                    <div class="col-md-2 text-right">
                                        <label>Customer Code</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtCCustomerCode" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 text-right">
                                        <label>Customer Name</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtCCustomerName" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 text-right">
                                        <label>Mobile</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtCMobile" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 text-right">
                                        <label>Country</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlCCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSCountry_SelectedIndexChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-md-2 text-right">
                                        <label>State</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlCState" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-2 text-right">
                                        <label>District</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlCDistrict" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnCustomerRetrieve" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="btnCustomerRetrieve_Click"></asp:Button>
                                    </div>
                                </div>
                                <div id="pnlNewCustomerName" runat="server" visible="false">
                                    <div class="col-md-3 text-right">
                                        <label>Customer Name</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 text-right">
                                        <label>GSTIN</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtGSTIN" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 text-right">
                                        <label>PAN</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 text-right">
                                        <label>Contact Person</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                    </div>


                                    <div class="col-md-3 text-right">
                                        <label>Mobile</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 text-right">
                                        <label>Alternative Mobile</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtAlternativeMobile" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone"></asp:TextBox>
                                    </div>


                                    <div class="col-md-3 text-right">
                                        <label>Email</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Email"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 text-right">
                                        <label>Address 1</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 text-right">
                                        <label>Address 2</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 text-right">
                                        <label>Address 3</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 text-right">
                                        <label>Country</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="Country" DataValueField="CountryID" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-md-3 text-right">
                                        <label>State</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-md-3 text-right">
                                        <label>District</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-md-3 text-right">
                                        <label>Tehsil</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlTehsil" runat="server" CssClass="form-control" DataTextField="Tehsil" DataValueField="TehsilID" />
                                    </div>
                                    <div class="col-md-3 text-right">
                                        <label>PinCode</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 text-right">
                                        <label>City</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnSaveCustomer" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSaveCustomer_Click" />
                                    </div>
                                    </asp:Panel>
                                </div>
                                <asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Code">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerID")%>' runat="server" Visible="false" />
                                                <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f2f2f2" />
                                    <FooterStyle ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                        </fieldset>


                        <fieldset class="fieldset-border" id="fldCountry" runat="server">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
                            <div class="col-md-12">
                                <div class="col-md-3 text-right">
                                    <label>Lead Date</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtLeadDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>

                                <div class="col-md-3 text-right">
                                    <label>Status</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" />
                                </div>

                                <div class="col-md-3 text-right">
                                    <label>Progress Status</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlProgressStatus" runat="server" CssClass="form-control" />
                                </div>


                                <div class="col-md-3 text-right">
                                    <label>Category</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" DataTextField="Category" DataValueField="CategoryID" />
                                </div>




                                <div class="col-md-3 text-right">
                                    <label>Qualification</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlQualification" runat="server" CssClass="form-control" DataTextField="Qualification" DataValueField="QualificationID" />
                                </div>
                                <div class="col-md-3 text-right">
                                    <label>Source</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control" DataTextField="Source" DataValueField="SourceID" />
                                </div>

                                <div class="col-md-3 text-right">
                                    <label>Lead Type</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlLeadType" runat="server" CssClass="form-control" DataTextField="Status" DataValueField="StatusID" />
                                </div>
                                <div class="col-md-3 text-right">
                                    <label>Remarks</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
                            </div>
                        </fieldset>



                    </div>

                </div>
                </div>
            </ContentTemplate>

        </asp1:TabPanel>
        <asp1:TabPanel ID="tplSalesOrg" runat="server" HeaderText="Lead Info">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Country</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 text-right">
                            <label>Lead Number</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblLeadNumber" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Lead Date</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblLeadDate" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Category</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblCategory" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Progress Status</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblProgressStatus" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Qualification</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblQualification" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Source</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblSource" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Status</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Type</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblType" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Dealer</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Remarks</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Customer</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Contact Person</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Mobile</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Email</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Location</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Financial Info</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblFinancialInfo" runat="server" CssClass="label"></asp:Label>
                        </div>
                    </div>
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>
    </asp1:TabContainer>
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

    <asp:Panel ID="pnlSEAssign" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Customer Create</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="PopupClose" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <asp:GridView ID="gvSalesEngineer" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
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
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assigned On" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedOn" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedOn")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lblSalesEngineerAdd" runat="server" OnClick="lblSalesEngineerAdd_Click">Add</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assigned By" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedBy.ContactName")%>' runat="server" />
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
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="MP_AssignSE" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlSEAssign" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


    <asp:Panel ID="pnlFollowUp" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix"><span id="PopupDialogue">Pre -Sales FollowUp</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"><asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a></div>
        <div class="col-md-12">
            <asp:GridView ID="gvFollowUp" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
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
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Follow Up Date">
                        <ItemTemplate>
                            <asp:Label ID="lblFollowUpDate" Text='<%# DataBinder.Eval(Container.DataItem, "FollowUpDate")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFollowUpDateF" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled" TextMode="Date"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Follow Up Note">
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "FollowUpNote")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFollowUpNoteF" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:LinkButton ID="lblFollowUpAdd" runat="server" OnClick="lblFollowUpAdd_Click">Add</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="MPE_FollowUp" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlFollowUp" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


    <asp:Panel ID="pnlConvocation" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix"><span id="PopupDialogue">Pre -Sales Convocation</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"><asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a></div>
        <div class="col-md-12">
            <asp:GridView ID="gvConvocation" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
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
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlSalesEngineerF" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Progress Status">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblProgressStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ProgressStatus.ProgressStatus")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlProgressStatusF" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Convocation" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblConvocation" Text='<%# DataBinder.Eval(Container.DataItem, "Convocation")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtConvocationF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Convocation Date" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblConvocationDate" Text='<%# DataBinder.Eval(Container.DataItem, "ConvocationDate")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtConvocationDateF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:LinkButton ID="lblConvocationAdd" runat="server" OnClick="lblConvocationAdd_Click">Add</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_Convocation" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlConvocation" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


    <asp:Panel ID="pnlFinancial" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix"><span id="PopupDialogue">Pre -Sales Financial Info</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"><asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a></div>
        <div class="col-md-12">
            <asp:GridView ID="gvFinancial" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank Name">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "BankName.BankName")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlBankNameF" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Finance Percentage" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblFinancePercentage" Text='<%# DataBinder.Eval(Container.DataItem, "FinancePercentage")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFinancePercentageF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>

                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRemarkF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:LinkButton ID="lblFinancialAdd" runat="server" OnClick="lblFinancialAdd_Click">Add</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>

    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_Financial" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlFinancial" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <asp:Panel ID="pnlEffort" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Pre -Sales Effort</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button4" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <asp:GridView ID="gvEffort" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
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
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Effort Type">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblEffortType" Text='<%# DataBinder.Eval(Container.DataItem, "EffortType.EffortType")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlEffortTypeF" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Effort Date" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblEffortDate" Text='<%# DataBinder.Eval(Container.DataItem, "EffortDate")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEffortDateF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>

                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Effort Start Time" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblEffortStartTime" Text='<%# DataBinder.Eval(Container.DataItem, "EffortStartTime")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEffortStartTimeF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Time"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Effort End Time" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblEffortEndTime" Text='<%# DataBinder.Eval(Container.DataItem, "EffortEndTime")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEffortEndTimeF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Time"></asp:TextBox>
                        </FooterTemplate>
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
                        <FooterTemplate>
                            <asp:TextBox ID="txtRemarkF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:LinkButton ID="lblEffortAdd" runat="server" OnClick="lblEffortAdd_Click">Add</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_Effort" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEffort" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <asp:Panel ID="pnlExpense" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Pre -Sales Expense</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button5" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
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
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlSalesEngineerF" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expense Type">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseType" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseType.ExpenseType")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlExpenseTypeF" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expense Date" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseDate" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseDate")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtExpenseDateF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAmountF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRemarkF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:LinkButton ID="lblExpenseAdd" runat="server" OnClick="lblExpenseAdd_Click">Add</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_Expense" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlExpense" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

</asp:Content>





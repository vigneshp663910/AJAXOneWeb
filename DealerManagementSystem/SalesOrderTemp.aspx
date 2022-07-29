<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SalesOrderTemp.aspx.cs" Inherits="DealerManagementSystem.SalesOrderTemp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        tr {
            background-color: initial;
        }

        th {
            background-color: initial;
            padding-right: 50px;
            padding-left: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message"/>
    <div class="col-md-12">        
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Sales Order</legend>
                <div class="col-md-12">
                    <asp:Panel ID="PnlCustomerView" runat="server" class="col-md-12">
                        <%--<div class="col-md-12">
                <div class="action-btn">
                    <div class="" id="boxHere"></div>
                    <div class="dropdown btnactions" id="customerAction">
                        <div class="btn Approval">Actions</div>
                        <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                            <asp:LinkButton ID="lbEditCustomer" runat="server">Edit Customer</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>--%>
                        <div class="col-md-12 field-margin-top">
                            <div class="col-md-12 View" style="margin-top: 25px">
                                <table>
                                    <tr><th><label>MODE OF BILLING</label></th><td><asp:Label ID="lblModeofbilling" runat="server" CssClass="label" Text="Modeofbilling"></asp:Label></td></tr>
                                    <tr><th><label>SAP TAX QTN NUMBER</label></th><td><asp:Label ID="lblSapQtnNumber" runat="server" CssClass="label" Text="SAP TAX QTN NUMBER"></asp:Label></td></tr>
                                    <tr><th><label>CUSTOMER CODE</label></th><td><asp:Label ID="lblCustomerCode" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>TITLE</label></th><td><asp:Label ID="lblTitle" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>INVOICE NAME AND ADDRESS</label><h6 style="color:red">(same will be shown in invoice & TR, kindly check before sending)</h6></th><td><asp:Label ID="lblInvoicenameandaddress" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>DELIVERY ADDRESS</label><h6 style="color:red">(only mention if delivery address is different than invoice addess)</h6></th><td><asp:Label ID="lblDeliveryAddress" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>GST NUMBER / UIN NUMBER</label></th><td><asp:Label ID="lblgstnumber" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>PAN NUMBER</label></th><td><asp:Label ID="lblpannumber" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>P.O. NUMBER AND DATE</label></th><td><asp:Label ID="lblponumberanddate" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>BASIC PRICE</label></th><td><asp:Label ID="lblbasicprice" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>HYPOTHECATION</label></th><td><asp:Label ID="lblhypothecation" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>BACK TO BACK - DO ENDORSED TO AJAX</label></th><td><asp:Label ID="lblbacktobackdoendorsedtoajax" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>DO NUMBER</label></th><td><asp:Label ID="lbldonumber" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>DO DATE</label></th><td><asp:Label ID="lbldodate" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>DO AMOUNT</label></th><td><asp:Label ID="lbldoamount" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>CREDIT DAYS</label></th><td><asp:Label ID="lblcreditdays" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>INVOICE VALUE</label></th><td><asp:Label ID="lblinvoicevalue" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>MODEL</label></th><td><asp:Label ID="lblmodel" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>SPECIAL REQUIREMENTS-1</label></th><td><asp:Label ID="lblspecialrequirement1" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>SPECIAL REQUIREMENTS-2</label></th><td><asp:Label ID="lblspecialrequirement2" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>SPECIAL REQUIREMENTS-3</label></th><td><asp:Label ID="lblspecialrequirement3" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>SPECIAL REQUIREMENTS-4</label></th><td><asp:Label ID="lblspecialrequirement4" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>SPECIAL REQUIREMENTS-5</label></th><td><asp:Label ID="lblspecialrequirement5" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>MACHINE QUANTITY</label></th><td><asp:Label ID="lblmachineqty" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>FOC: SERVICE KIT</label></th><td><asp:Label ID="lblfocservicekit" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>FOC: WHEEL ASSY.</label></th><td><asp:Label ID="lblfocwheelassy" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>FOC: EXTENSION CHUTES</label></th><asp:Label ID="lblfocextensionchutes" runat="server" CssClass="label"></asp:Label><td></td></tr>
                                    <tr><th><label>FOC: OTHERS (Please specify in text)</label></th><td><asp:Label ID="lblfocothers" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>SOURCE OF ENQURY</label></th><td><asp:Label ID="lblsourceofenquiry" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>REASON FOR ORDER CONVERSION</label></th><td><asp:Label ID="lblreasonfororderconversion" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>CUSTOMER TYPE</label></th><td><asp:Label ID="lblcustomertype" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>PROFILE</label></th><td><asp:Label ID="lblprofile" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>SIZE</label></th><td><asp:Label ID="lblsize" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>OWNERSHIP PATTERN</label></th><td><asp:Label ID="lblownershippattern" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>APPLICATION</label></th><td><asp:Label ID="lblapplication" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>NAME OF THE PROJECT</label></th><td><asp:Label ID="lblnameoftheproject" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>TRANSPORTATION AND INSUARANCE</label></th><td><asp:Label ID="lbltransportationandinsurance" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>SALES REGION</label></th><td><asp:Label ID="lblsalesregion" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>SALES OFFICE</label></th><td><asp:Label ID="lblsalesoffice" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>DEALER CODE</label></th><td><asp:Label ID="lbldealercode" runat="server" CssClass="label"></asp:Label></td></tr>
                                    <tr><th><label>DEALER NAME</label></th><td><asp:Label ID="lbldealername" runat="server" CssClass="label"></asp:Label></td></tr>
                                </table>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>

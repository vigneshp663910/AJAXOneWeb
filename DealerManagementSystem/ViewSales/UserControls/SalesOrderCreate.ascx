<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderCreate.ascx.cs" Inherits="DealerManagementSystem.ViewSales.UserControls.SalesOrderCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp:HiddenField ID="hdfCustomerId" runat="server" Visible="false"/>
<fieldset class="fieldset-border" runat="server">
    <legend style="background: none; color: #007bff; font-size: 17px;">Gr Details</legend>
    <div class="col-md-12">
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Customer</label>
            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"
                onKeyUp="GetCustomers()"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label>Remarks</label>
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label>Remarks</label>
            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
        </div>
    </div>
</fieldset>

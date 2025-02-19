<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketMarginWarrantyChange.aspx.cs" Inherits="DealerManagementSystem.ViewService.ICTicketMarginWarrantyChange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ICTicketBasicInformation.ascx" TagPrefix="UC" TagName="UC_BasicInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">IC Ticket</label>
                        <asp:TextBox ID="txtICTicket" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-10 text-left">
                        <label class="modal-label">-</label>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                    </div>
                </div>
            </fieldset>
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Ticket Margin Warranty Change</legend>
                <UC:UC_BasicInformation ID="UC_BasicInformation" runat="server"></UC:UC_BasicInformation>
            </fieldset>
            <fieldset class="fieldset-border">
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Is Margin Warranty</label>
                        <asp:CheckBox ID="cbIsMarginWarranty" runat="server" Enabled="false" CssClass="form-control"/>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Margin Remark</label>
                        <asp:TextBox ID="txtMarginRemark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="col-md-8 text-left">
                        <label class="modal-label">-</label>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" UseSubmitBehavior="true" OnClick="btnSave_Click" OnClientClick="return dateValidation();" />
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>

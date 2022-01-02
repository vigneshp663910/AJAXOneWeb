<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="tab_ASN.aspx.cs" Inherits="DealerManagementSystem.ViewSales.tab_ASN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divtop" style="width: 100%; height: 100%">
        <iframe id="iframe_ASN" name="iframe_ASN" src="tab_ASNPages"
            style="border-color: #FF0000; border-width: thin; height: 901px; width:100%; overflow: hidden;"></iframe>
    </div>
</asp:Content>

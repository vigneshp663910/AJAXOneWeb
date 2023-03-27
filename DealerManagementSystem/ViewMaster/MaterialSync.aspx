<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="MaterialSync.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.MaterialSync" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:TextBox ID="txtMaterialCode" runat="server"></asp:TextBox>
    <asp:Button ID="BtnMaterialSync" runat="server" Text="Material" OnClick="BtnMaterialSync_Click" />
</asp:Content>

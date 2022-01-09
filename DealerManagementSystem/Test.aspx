<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="DealerManagementSystem.Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <asp:Literal ID="ltScripts" runat="server"></asp:Literal>
    <div id="chart_div" style="width: 660px; height: 400px;">
    </div>
</asp:Content>

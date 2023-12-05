<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="BIService.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.BIService" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #iframeID
        {
            width:100%;
            height:91vh;
        }
    </style> 

   <iframe id="iframeID" title="Service"   src="https://app.powerbi.com/reportEmbed?reportId=ac4f2767-6262-4c3e-b9b1-1719de3cfcf7&autoAuth=true&ctid=c5850684-0690-45ce-b83b-66b28e82830b" frameborder="0" allowFullScreen="true"></iframe>

</asp:Content>

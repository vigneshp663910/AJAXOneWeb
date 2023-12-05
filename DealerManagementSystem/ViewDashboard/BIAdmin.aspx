<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="BIAdmin.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.BIAdmin" %>
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

   <iframe id="iframeID" title="Admin"   src="https://app.powerbi.com/reportEmbed?reportId=8d4cfe52-3bfd-4b39-ba5f-34ef45af1fdc&autoAuth=true&ctid=c5850684-0690-45ce-b83b-66b28e82830b" frameborder="0" allowFullScreen="true"></iframe>

                                       
</asp:Content>



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

    <iframe id="iframeID" title="Admin"   src="https://app.powerbi.com/reportEmbed?reportId=11a7b9f4-0a5a-4463-a9b6-8b08cac3510e&appId=36c844a1-66a3-4fe1-b88b-5f042941fe44&autoAuth=true&ctid=c5850684-0690-45ce-b83b-66b28e82830b" frameborder="0" allowFullScreen="true"></iframe>
                                       
 </asp:Content>


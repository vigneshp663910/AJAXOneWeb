<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="BIQuality.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.BIQuality" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #iframeID
        {
            width:100%;
            height:91vh;
        }
    </style> 

   <iframe id="iframeID" title="Quality"   src="https://app.powerbi.com/reportEmbed?reportId=7ba897d7-c3e7-4a93-8d59-2c42e26ecd99&appId=cc14a4ba-9e32-48d8-996c-7f67ffe772bd&autoAuth=true&ctid=c5850684-0690-45ce-b83b-66b28e82830b" frameborder="0" allowFullScreen="true"></iframe>




</asp:Content>

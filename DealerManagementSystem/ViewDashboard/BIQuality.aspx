<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="BIQuality.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.BIQuality" %> 


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #iframeID {
            width: 100%;
            height: 91vh;
        }
    </style>

    <iframe id="iframeID" title="Quality" src="https://app.powerbi.com/reportEmbed?reportId=cd1ae44c-4ace-42fb-92bd-c73caa7a6c9f&appId=cc14a4ba-9e32-48d8-996c-7f67ffe772bd&autoAuth=true&ctid=c5850684-0690-45ce-b83b-66b28e82830b" 
      frameborder="0" allowfullscreen="true"></iframe>


</asp:Content>

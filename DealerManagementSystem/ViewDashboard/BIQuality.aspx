<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="BIQuality.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.BIQuality" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #iframeID
        {
            width:100%;
            height:91vh;
        }
    </style> 

   <iframe id="iframeID" title="Quality"   src="https://app.powerbi.com/reportEmbed?reportId=b0e2bb43-7c89-4df9-b32e-3b65a7395236&appId=4c3aae15-afa4-4ec2-80dd-7eafbc640466&autoAuth=true&ctid=c5850684-0690-45ce-b83b-66b28e82830b" frameborder="0" allowFullScreen="true"></iframe>

</asp:Content>

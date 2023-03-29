<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="BiEnquiry.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.BiEnquiry" %>
 



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <style>
        #iframeID
        {
            width:100%;
            height:91vh;
        }
    </style> 

   <iframe id="iframeID" title="Enquiry"   src="https://app.powerbi.com/reportEmbed?reportId=59906eb9-829e-4639-9839-f8c2c472d035&autoAuth=true&ctid=c5850684-0690-45ce-b83b-66b28e82830b" frameborder="0" allowFullScreen="true"></iframe>
   
    
</asp:Content>

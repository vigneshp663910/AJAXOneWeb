<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="BiEnquiry.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.BiEnquiry" %>
 



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <style>
        #iframeID
        {
            width:100%;
            height:91vh;
        }
    </style> 
   <iframe id="iframeID" title="Enquiry"   src="https://app.powerbi.com/reportEmbed?reportId=1f84fe1c-3831-427d-a8e9-5a62bba244ce&autoAuth=true&ctid=c5850684-0690-45ce-b83b-66b28e82830b" frameborder="0" allowFullScreen="true"></iframe>
       

</asp:Content>

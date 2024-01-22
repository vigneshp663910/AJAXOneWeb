<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="eCatalogueN.aspx.cs" Inherits="DealerManagementSystem.RedirectTo.eCatalogueN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%-- <iframe id="iframeProfile" src="https://ajaxapps.ajax-engg.com:8095/eCatalogue/SP_Catalogue?ProductGroup=XX&Model=X&McNo=XXXXXXXXXXXX" width="100%" style="height: 98vh; margin-top:-70px" frameborder="0"></iframe>--%>

    <html>
    <head>
        <title>Test html page</title>
        <script type="text/javascript">
            function AlertVPNConnection() {
                alert("Please Connect VPN if not and Click OK to Continue...");
            }
        </script>
    </head>
    <body onload="AlertVPNConnection();">
         <iframe id="iframeProfile" src="https://ajaxapps.ajax-engg.com:8095/eCatalogue/SignIn.aspx" width="100%" style="height: 98vh; margin-top: -70px" frameborder="0"></iframe>
    </body>
    </html>

</asp:Content>

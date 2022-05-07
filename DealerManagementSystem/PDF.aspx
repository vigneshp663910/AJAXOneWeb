<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="PDF.aspx.cs" Inherits="DealerManagementSystem.PDF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #download{
            visibility:hidden;
        }
        #more{
            visibility:hidden;
        }
        #start{
            visibility:hidden;
        }
        #center{
            visibility:hidden;
        }
        iframe { overflow:hidden; }
    </style>
    <script type="text/javascript">
        jQuery('#ifrm_dcbform').load(function () {
            jQuery('#ifrm_dcbform').contents().find("#start").hide();
            jQuery('#ifrm_dcbform').contents().find("#center").hide();
            jQuery('#ifrm_dcbform').contents().find("#end").hide();
        });
    </script>
    <iframe id="ifrm_dcbform" runat="server" class="sdd" style="width: 100%" frameborder="0" border="0" allowtransparency="true" scrolling="no"></iframe>
</asp:Content>

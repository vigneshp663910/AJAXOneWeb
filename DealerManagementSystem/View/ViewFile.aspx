<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ViewFile.aspx.cs" Inherits="DealerManagementSystem.View.ViewFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <iframe id="ifrm_dcbform" src="https://storciaeajaxonedev.blob.core.windows.net/temp/903124SI00007 (2).pdf" runat="server" 
         class="sdd"  style="width: 100%; height:89.0vh"  frameborder="0" border="0" allowtransparency="true" scrolling="no"></iframe>

  <%--  <iframe id="Iframe1" src="https://storciaeajaxonedev.blob.core.windows.net/temp/903124SI00007 (2).pdf" runat="server" 
         class="sdd" style="width: 100%; height:89.0vh"  frameborder="0" border="0" allowtransparency="true" scrolling="no"></iframe>--%>
     <iframe id="Iframe2" src="C:/Users/john.peter/Downloads/File130032024143042%20(3).pdf" runat="server" 
         class="sdd" style="width: 100%; height:89.0vh"  frameborder="0" border="0" allowtransparency="true" scrolling="no"></iframe>


    
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SPTN.aspx.cs" Inherits="DealerManagementSystem.RedirectTo.SPTN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--  <iframe id="iframeProfile" src="https://ajaxapps.ajax-engg.com:8095/SAPBOK/Help" width="100%" style="height: 114vh; margin-top:-180px" frameborder="0"></iframe>--%>

    <iframe id="iframeProfile" src="https://ajaxapps.ajax-engg.com:8095/SAPBOK/HelpDocContents.aspx?Category=SS" width="100%" style="height: 100vh; margin-top: -65px" frameborder="0"></iframe>
</asp:Content>

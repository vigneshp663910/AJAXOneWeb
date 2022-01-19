<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="tab_MaterialMaster.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.tab_MaterialMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="divtop" style="width: 100%; height: 100%; margin-left:1px;">
        <iframe id="iframe_MM" name="iframe_MM" src="tab_MaterialMasterPages"
            style="border-color: #FF0000; border-width: thin; height: 901px; width:100%; overflow: hidden;"></iframe>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Lead.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Lead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .TabHeaderCSS {
            font-family: @FangSong, Arial, "Courier New";
            font-size: 50px;
            background-color: Silver;
            text-align: center;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <br />
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" CssClass="TabHeaderCSS">
        <asp:TabPanel runat="server" HeaderText="List" ID="TabPanel1" Width="150px">
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Create">
        </asp:TabPanel>
    </asp:TabContainer>


</asp:Content>

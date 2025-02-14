<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="UserAccessByModule.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.UserAccessByModule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewAdmin/UserControls/UserAccessByDealer.ascx" TagPrefix="UC" TagName="UC_Dealer" %>
<%@ Register Src="~/ViewAdmin/UserControls/UserAccessBySubModule.ascx" TagPrefix="UC" TagName="UC_SubModule" %>
<%@ Register Src="~/ViewAdmin/UserControls/UserAccessByChildModule.ascx" TagPrefix="UC" TagName="UC_ChildModule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp1:TabContainer ID="tbpCust" runat="server"   Font-Bold="True" Font-Size="Medium">
        <asp1:TabPanel ID="tpnlDealerAccess" runat="server" HeaderText="Dealer Access" Font-Bold="True">
            <ContentTemplate>
                <UC:UC_Dealer ID="UC_Dealer1" runat="server"></UC:UC_Dealer>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="TabSubModuleAccess" runat="server" HeaderText="Sub Module Access" Font-Bold="True">
            <ContentTemplate>
                 <UC:UC_SubModule ID="UC_SubModule1" runat="server"></UC:UC_SubModule>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpnlChildModuleAccess" runat="server" HeaderText="Child Module Access" Font-Bold="True">
            <ContentTemplate>
                 <UC:UC_ChildModule ID="UC_ChildModule1" runat="server"></UC:UC_ChildModule>
            </ContentTemplate>
        </asp1:TabPanel>
    </asp1:TabContainer>
</asp:Content>

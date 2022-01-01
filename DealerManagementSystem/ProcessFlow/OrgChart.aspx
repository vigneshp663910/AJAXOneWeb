<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="OrgChart.aspx.cs" Inherits="DealerManagementSystem.ProcessFlow.OrgChart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            width: 100px;
            height: 50px;
            font: 20px;
        }

        .ajax__tab_xp .ajax__tab_header {
            background-position: bottom;
            background-repeat: repeat-x;
            font-family: verdana,tahoma,helvetica;
            font-size: 12px;
            font-weight: bold;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--<style>
        .ajax__tab .ajax__tab_tab {
            height: 30px; /*if you change it here*/
            margin: 0;
            padding: 20px 4px;
            border: 0px solid;
            border-bottom: 0px;
        }

        .ajax__tab .ajax__tab_body {
            ;
            top: 30px; /*change it here also*/
            border: 1px solid;
            border-top: 0px;
        }
    </style>--%>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

    <%--<div class="col-md-12">--%>
    <%--<div class="col-md-12">--%>
        <asp1:TabContainer ID="tbpOrgChart" runat="server" ToolTip="DMS Organisation Chart" Font-Bold="True" Font-Size="Medium" VerticalStripWidth="240px">
            <asp1:TabPanel ID="tbpnlAjaxOrg" runat="server" HeaderText="OEM" Font-Bold="True" ToolTip="OEM  Organisation Chart...">
                <ContentTemplate>
                    <fieldset class="fieldset-border">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/ProcessFlow/AJAX_Org1.png" />
                    </fieldset>
                </ContentTemplate>
            </asp1:TabPanel>
            <asp1:TabPanel ID="tpDealerOrg" runat="server" HeaderText="DEALER" ToolTip="Dealer  Organisation Chart...">
                <ContentTemplate>
                    <fieldset class="fieldset-border">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/ProcessFlow/Dealer_Org1.png" />
                    </fieldset>
                </ContentTemplate>
            </asp1:TabPanel>
            <asp1:TabPanel ID="tplSalesOrg" runat="server" HeaderText="Sales" ToolTip="Sales Organisation Chart...">
                <ContentTemplate>
                    <fieldset class="fieldset-border">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/ProcessFlow/Sales_Org2.png" Width="1100" Height="900" />
                    </fieldset>
                </ContentTemplate>
            </asp1:TabPanel>
            <asp1:TabPanel ID="tpPartsOrg" runat="server" HeaderText="Parts" ToolTip="PARTS  Organisation Chart...">
                <ContentTemplate>
                    <fieldset class="fieldset-border">
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/ProcessFlow/Parts_Org2.png" Width="1100" Height="900" />
                    </fieldset>
                </ContentTemplate>
            </asp1:TabPanel>
            <asp1:TabPanel ID="tpServiceOrg" runat="server" HeaderText="Service" ToolTip="Service Organisation Chart...">
                <ContentTemplate>
                    <fieldset class="fieldset-border">
                        <asp:Image ID="Image5" runat="server" ImageUrl="~/ProcessFlow/Service_Org2.png" Width="1100" Height="900" />
                    </fieldset>
                </ContentTemplate>
            </asp1:TabPanel>

        </asp1:TabContainer>
   <%-- </div>--%>
    <%--</div>--%>
</asp:Content>

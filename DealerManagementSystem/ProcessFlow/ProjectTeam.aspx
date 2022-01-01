<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ProjectTeam.aspx.cs" Inherits="DealerManagementSystem.ProcessFlow.ProjectTeam" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            width: 120px;
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
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <%--<div class="col-md-12">
        <div class="col-md-12">--%>
            <asp1:TabContainer ID="tbpOrgChart" runat="server" ToolTip="Project Team" Font-Bold="True" Font-Size="Medium">
                <asp1:TabPanel ID="tbpnlAjaxOrg" runat="server" HeaderText="IT Team" Font-Bold="True">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/ProcessFlow/Project_Team_IT1.png" />
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tpDealerOrg" runat="server" HeaderText="Functional Team">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/ProcessFlow/Project_Team_Functional1.png" />
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>

                <asp1:TabPanel ID="tpProjectPlan" runat="server" HeaderText="Project Plan">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <asp:Image ID="Image6" runat="server" ImageUrl="~/ProcessFlow/Project_Plan1.png" />
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>

            </asp1:TabContainer>
       <%-- </div>
    </div>--%>
</asp:Content>

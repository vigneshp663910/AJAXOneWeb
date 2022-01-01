<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Process_Flow.aspx.cs" Inherits="DealerManagementSystem.ProcessFlow.Process_Flow" %>

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
    <%--<div class="col-md-12">--%>
      <%--  <div class="col-md-12">--%>
            <asp1:TabContainer ID="tbpProcess_Flow" runat="server" ToolTip="Process Flow" Font-Bold="True" Font-Size="Medium">
                <asp1:TabPanel ID="tbpnPre_Sales" runat="server" HeaderText="Pre-Sales" Font-Bold="True">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/ProcessFlow/Pre_Sales1.png" />
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tpService" runat="server" HeaderText="Procurement">
                    <ContentTemplate>
                        <%--<fieldset class="fieldset-border">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/ProcessFlow/Service1.png" />
                        </fieldset>--%>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tplMCSales" runat="server" HeaderText="M/C Sales">
                    <ContentTemplate>
                        <%--<fieldset class="fieldset-border">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/ProcessFlow/Sales_Org1.png" />
                        </fieldset>--%>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tpPartsSales" runat="server" HeaderText="Parts       Sales">
                    <ContentTemplate>
                        <%--<fieldset class="fieldset-border">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/ProcessFlow/Parts_Org1.png" />
                        </fieldset>--%>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tpServiceOrg" runat="server" HeaderText="Service">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <asp:Image ID="Image5" runat="server" ImageUrl="~/ProcessFlow/Service1.png" />
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>

                

            </asp1:TabContainer>
    <%--    </div>--%>
   <%-- </div>--%>
</asp:Content>

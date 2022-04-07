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
    <asp1:TabContainer ID="tcProcess_Flow" runat="server" ToolTip="Process Flow" Font-Bold="True" Font-Size="Medium">
        <asp1:TabPanel ID="tbPre_Sales" runat="server" HeaderText="Pre-Sales" Font-Bold="True">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/ProcessFlow/Pre_Sales1.png" />
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpProcurement" runat="server" HeaderText="Procurement">
            <ContentTemplate>
                <%--<fieldset class="fieldset-border">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/ProcessFlow/Service1.png" />
                        </fieldset>--%>
                <asp1:TabContainer ID="tcProcurement" runat="server" ToolTip="Procurement" Font-Bold="True" Font-Size="Medium">
                    <asp1:TabPanel ID="tpP1" runat="server" HeaderText="OE/Co-Dealer" Font-Bold="True" ToolTip="Procurement With OE/Co-Dealer">
                        <ContentTemplate>
                            <fieldset class="fieldset-border">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/ProcessFlow/V0_Procurment_Process_OE_CoDealer.png" />
                            </fieldset>
                        </ContentTemplate>
                    </asp1:TabPanel>
                    <asp1:TabPanel ID="tpP2" runat="server" HeaderText="PurchaseReturn" Font-Bold="True" ToolTip="Purchase Return">
                        <ContentTemplate>
                            <fieldset class="fieldset-border">
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/ProcessFlow/V0_Purchse_Return.png" />
                            </fieldset>
                        </ContentTemplate>
                    </asp1:TabPanel>
                    <asp1:TabPanel ID="tpP3" runat="server" HeaderText="StockTransfer" Font-Bold="True" ToolTip="Stock Transfer">
                        <ContentTemplate>
                            <fieldset class="fieldset-border">
                                <asp:Image ID="Image4" runat="server" ImageUrl="~/ProcessFlow/V0_Stock_Transfer.png" />
                            </fieldset>
                        </ContentTemplate>
                    </asp1:TabPanel>

                </asp1:TabContainer>
            </ContentTemplate>
        </asp1:TabPanel>

        <asp1:TabPanel ID="tpPartsSales" runat="server" HeaderText="Sales">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/ProcessFlow/V0_Secondary_Sales.png" />
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpService" runat="server" HeaderText="Service">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <asp:Image ID="Image6" runat="server" ImageUrl="~/ProcessFlow/Service1.png" />
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpInventory" runat="server" HeaderText="Inventory">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <asp:Image ID="Image7" runat="server" ImageUrl="~/ProcessFlow/V0_Physical_Inventory.png" />
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>

        <asp1:TabPanel ID="tpDealerManpower" runat="server" HeaderText="DealerManpower">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <asp:Image ID="Image8" runat="server" ImageUrl="~/ProcessFlow/V0_DMPM.png" />
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>

        <asp1:TabPanel ID="tpDMWebMobileUserId" runat="server" HeaderText="Web&Mobile Id ">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <asp:Image ID="Image9" runat="server" ImageUrl="~/ProcessFlow/V0_WebMobileUserID.png" />
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>

        <asp1:TabPanel ID="tpImplementtionProcess" runat="server" HeaderText="Implementation">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <asp:Image ID="Image10" runat="server" ImageUrl="~/ProcessFlow/V0_Implementation.png" />
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>


    </asp1:TabContainer>
    <%--    </div>--%>
    <%-- </div>--%>
</asp:Content>

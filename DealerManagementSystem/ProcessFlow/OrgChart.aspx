<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="OrgChart.aspx.cs" Inherits="DealerManagementSystem.ProcessFlow.OrgChart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
   
    <div class="col-md-12">
        <div class="col-md-12">
            <asp1:TabContainer ID="tbpOrgChart" runat="server" ToolTip="DMS Organisation Chart" Font-Bold="True" Font-Size="Medium">
                <asp1:TabPanel ID="tbpnlAjaxOrg" runat="server" HeaderText="OEM" Font-Bold="True">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/ProcessFlow/AJAX_Org1.png" />
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tpDealerOrg" runat="server" HeaderText="DEALER">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/ProcessFlow/Dealer_Org1.png" />
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tplSalesOrg" runat="server" HeaderText="Sales">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/ProcessFlow/Sales_Org1.png" />
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tpPartsOrg" runat="server" HeaderText="Parts">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/ProcessFlow/Parts_Org1.png" />
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tpServiceOrg" runat="server" HeaderText="Service">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <asp:Image ID="Image5" runat="server" ImageUrl="~/ProcessFlow/Service_Org1.png" />
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>

            </asp1:TabContainer>
        </div>
    </div>
</asp:Content>

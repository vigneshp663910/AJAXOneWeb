<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Organization.aspx.cs" Inherits="DealerManagementSystem.ViewOrganization.Organization" %>

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

    <script src="Scripts/jquery-1.7.1.js"></script>
    <script language="javascript" type="text/javascript">
        function placeUP() {
            var mouseX;
            var mouseY;
            // below line for get mouse position
            $(document).mousemove(function (e) {
                mouseX = e.pageX;
                mouseY = e.pageY;

            });
            // below line for show loading panel at proper place
            $('#<%= TreeView1.ClientID%> a').click(function () {
                $('#UP').css({ 'top': mouseY, 'left': mouseX });
            });
        }
    </script>


    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

   <%-- <div class="col-md-12">
        <div class="col-md-12">--%>
            <asp1:TabContainer ID="tbpOrgChart" runat="server" ToolTip="DMS Organisation Chart" Font-Bold="True" Font-Size="Medium" VerticalStripWidth="240px">
                <asp1:TabPanel ID="tbpnlAjaxOrg" runat="server" HeaderText="OEM" Font-Bold="True" ToolTip="OEM  Organisation Chart...">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <script type="text/javascript">
                                        Sys.Application.add_load(placeUP);
                                    </script>
                                    <%-- Here ExpandDepth="0" for eleminates the expansion of the added treenodes --%>

                                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="5" PopulateNodesFromClient="false" OnTreeNodePopulate="TreeView1_TreeNodePopulate">
                                    </asp:TreeView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
      <%--  </div>
    </div>--%>

    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="UP" style="position: absolute; background-image: url('ajax-loader.gif'); background-repeat: no-repeat; width: 20px;">
                &nbsp;
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

     
</asp:Content>

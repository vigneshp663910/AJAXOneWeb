<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Organization.aspx.cs" Inherits="DealerManagementSystem.ViewOrganization.Organization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

    <h3>Populate Treeview Nodes Dynamically (On Demand)</h3>
    <%-- Scripr manager is required as we are using AJAX --%>
    <%-- UpdateProgress is used for show loading panel while loading child nodes --%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="UP" style="position: absolute; background-image: url('ajax-loader.gif'); background-repeat: no-repeat; width: 20px;">
                &nbsp;
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                Sys.Application.add_load(placeUP);
            </script>
            <%-- Here ExpandDepth="0" for eleminates the expansion of the added treenodes --%>

       <%--   <asp:ListView ID="TreeView1" runat="server" ExpandDepth="0" PopulateNodesFromClient="false" OnTreeNodePopulate="TreeView1_TreeNodePopulate">--%>
                 <asp:TreeView  ID="TreeView1" runat="server" ExpandDepth="0" PopulateNodesFromClient="false"  OnTreeNodePopulate="TreeView1_TreeNodePopulate">
                     
               <%-- <alternatingitemtemplate>
                    <table>
                        <td>
                            <asp:Label ID="NovinkaLabel" runat="server" Text="John" />
                        </td>
                    </table>
                </alternatingitemtemplate>
                <itemtemplate>
                    <table>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Pe" />
                        </td>
                    </table>
                </itemtemplate>--%>
              
            </asp:TreeView>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--    <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15">
        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
        <ParentNodeStyle Font-Bold="False" />
        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
    </asp:TreeView>--%>

    <asp:Button ID="Button1" runat="server" Text="Button"  OnClick="Button1_Click"/>
</asp:Content>

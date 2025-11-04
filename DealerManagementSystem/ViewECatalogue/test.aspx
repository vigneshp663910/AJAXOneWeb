<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="test.aspx.cs" Inherits="DealerManagementSystem.ViewECatalogue.test"%> 
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
       
        /* horizontal panel*/
        .panel-container {
            display: flex;
            flex-direction: row;
            overflow: hidden;
            height: 96%;
        }

        .panel-left {
            flex: 0 0 auto; /* only manually resize */
            width: 20%;
            max-width: 40%;
            min-height: 200px;
            min-width: 15px;
            white-space: nowrap;
            background: #838383;
            color: white;
        }

        .splitter {
            flex: 0 0 auto;
            width: 10px;
            background: url('/Images/vsizegrip.png') center center no-repeat #838383;
            min-height: 200px;
            cursor: col-resize;
        }

        .panel-right {
            flex: 1 1 auto; /* resizable */
            width: 100%;
            min-height: 200px;
            min-width: 200px !important; /* NOTE: This won't be respected! Explicit size of left forces the size to be fixed */
            background: #eee;
        } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
                <asp:TreeView ID="TreeView1" runat="server" ImageSet="BulletedList4"
                    NodeIndent="15" ExpandDepth="0" Font-Names="Calibri" Font-Size="Large" Font-Bold="False">
                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                    <NodeStyle Font-Names="Tahoma" Font-Size="13pt" ForeColor="Black" HorizontalPadding="2px"
                        NodeSpacing="0px" VerticalPadding="0px"></NodeStyle>
                    <ParentNodeStyle Font-Bold="True" />
                    <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="2px"
                        VerticalPadding="2px" />
                </asp:TreeView>
      
     
</asp:Content>

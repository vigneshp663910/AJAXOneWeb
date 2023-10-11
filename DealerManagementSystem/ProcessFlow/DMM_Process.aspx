<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DMM_Process.aspx.cs" Inherits="DealerManagementSystem.ProcessFlow.DMM_Process" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    <body>
        <div class="col-md-12">
            <div class="col-md-11">
                <div id="ProcessFlowImage" runat="server">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/ProcessFlow/DMM_Process.png" />
                </div>
            </div>
            <div class="col-md-1" style="text-align: right; vertical-align: top">
                <asp:HyperLink ID="HyperLinkpdf" runat="server" CssClass="btn Save" NavigateUrl="../Help/HelpDoc.aspx?aFileName=../ProcessFlow/SOP/SOP_DealerEmployee.pdf" style="line-height:1.5 !important" ToolTip="SOP_DealerEmployee.pdf">
                    SOP
                </asp:HyperLink>
            </div>
        </div>
    </body>
    </html>
</asp:Content>

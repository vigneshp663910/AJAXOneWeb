<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Pre_Sales.aspx.cs" Inherits="DealerManagementSystem.ProcessFlow.Pre_Sales" %>

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
                <asp:Image ID="Image1" runat="server" ImageUrl="~/ProcessFlow/Pre_Sales1.png" />
            </div>
            <div class="col-md-1" style="text-align:right;vertical-align:top">
                <asp:Button ID="btnSOP" runat="server" CssClass="btn Save" Text="SOP" OnClick="btnSOP_Click" Width="80px"></asp:Button>
            </div>
        </div>
    </body>
    </html>
</asp:Content>

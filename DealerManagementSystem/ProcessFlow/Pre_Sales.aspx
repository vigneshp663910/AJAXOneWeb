<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Pre_Sales.aspx.cs" Inherits="DealerManagementSystem.ProcessFlow.Pre_Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>

    
    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server" /> </asp:ScriptManager>--%>
    <body onload="SetScreenTitle('Pre-Sales Process Flow')">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/ProcessFlow/Pre_Sales1.png" />
        <%--<form id="form1" runat="server">
            <div>
            </div>
        </form>--%>

        <%--<script>
            function SetScreenTitle() {
                alert('Hi');
            }
        </script>--%>
    </body>
    </html>
</asp:Content>

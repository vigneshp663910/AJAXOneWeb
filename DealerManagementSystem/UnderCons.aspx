<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="UnderCons.aspx.cs" Inherits="DealerManagementSystem.UnderCons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html>
    <head>
        <title>AJAX-eInfo</title>
        <link href="/Images/eInfo1.png" rel="shortcut icon" type="image/x-icon" />
        <style>
            .item1 {
                grid-area: header;
            }

            .item2 {
                grid-area: menu;
            }

            .item3 {
                grid-area: main;
            }

            .item4 {
                grid-area: right;
            }

            .item5 {
                grid-area: footer;
            }

            .grid-container {
                display: grid;
                grid-template-areas:
                    'header header header header header header'
                    'menu main main main main right'
                    'menu footer footer footer footer footer';
                grid-gap: 5px;
                background-color: #2196F3;
                padding: 10px;
            }

                .grid-container > div {
                    background-color: rgba(255, 255, 255, 0.8);
                    text-align: center;
                    padding: 20px 0;
                    font-size: 30px;
                }
        </style>
    </head>
    <body>

        <%--<h1>Learning Management System</h1>--%>

        <%--<p>This grid layout contains six columns and three rows:</p>--%>

        <div class="grid-container">
            <%--<div class="item1">DMS</div>
            <div class="item2"></div>--%>
            <div class="item3">
                <img src='/images/construction.gif' width="202" height="202"><br>

                <font face="verdana" size="4" color="blue" align="center">This part is under construction.<br>
                    Please visit some time later.<br />
                    Thank You</font>
            </div>
            <%--<div class="item4"></div>
            <div class="item5"></div>--%>
        </div>

    </body>
    </html>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="HelpDoc.aspx.cs" Inherits="DealerManagementSystem.Help.HelpDoc" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <head>
        <style type="text/css">
            .auto-style1 {
                width: 776px;
            }
        </style>
    </head>

    <%--<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" oncontextmenu="return false;">--%>
    <body onload="SetHeader()">
        <%--<table style="border-color: #CCCCCC; width: 1000px; font-family: Calibri; font-size: large; border-collapse: collapse;" border="2">

            <tr>
                <td rowspan="2" style="width: 150px">
                    <img src="../Ajax/Images/Ajax-New-Logo.png" border="0" width="150" height="45">
                </td>
                <td align="center" class="auto-style1"><b>AJAX ENGINEERING PRIVATE LTD</b></td>
                <td rowspan="2">
                    <img src="../Ajax/Images/UserGuide.png" height="45px" width="60px">
                </td>
            </tr>
            <tr>
                <td id="PageHeading" align="center" class="auto-style1"><b>DOCUMENTS
                </b></td>
            </tr>
        </table>--%>

        <%--<hr style="background-color: #0000FF" />--%>
        <asp:Literal ID="ltEmbed" runat="server" />
        <%--<hr/>--%>

        <script type="text/javascript">
            document.onmousedown = disableRightclick;
            var message = "Right click not allowed !!";
            function disableRightclick(evt) {
                if (evt.button == 2) {
                    alert(message);
                    return false;
                }
            }

            function SetHeader() {

                var catg = getParameterByName('Category');
                var phead = document.getElementById('PageHeading')

                if (catg == 'SS') {
                    phead.innerHTML = "<b>PARTS TOPICS</b>"
                }

                if (catg == 'IT') {
                    phead.innerHTML = "<b>IT/SYSTEM DOCUMENTS</b>"
                }

            }

            function getParameterByName(name, url) {
                if (!url) url = window.location.href;
                name = name.replace(/[\[\]]/g, '\\$&');
                var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, ' '));
            }

        </script>
        <%--</asp:Content>--%>
    </body>
</asp:Content>

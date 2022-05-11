<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="DealerManagementSystem.Help.Help" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    <body>

        <table style="border-color: #CCCCCC; width: 1000px; font-family: Calibri; font-size: large; border-collapse: collapse;" border="2">

            <%--<tr>
                <td rowspan="2" style="width: 150px">
                    <%--<img src="../Ajax/Images/Ajax-New-Logo.png" border="0" width="150" height="45">-
                </td>
                <td align="center" class="auto-style1"><b>AJAXOne HELP</b></td>
                <td rowspan="2">
                    <img src="../Ajax/Images/UserGuide.png" height="45px" width="60px">
                </td>
            </tr>--%>
            <tr>
                <td colspan="5" id="PageHeading" align="center" class="auto-style1"><b>INDEX
                </b></td>
            </tr>
            <tr align="center" style="color: #FFFFFF">
                <th width="30px">SN</th>
                <th width="600px">Module</th>
                <th width="150px">PDF</th>
                <th width="150px">Video</th>
                <th width="150px">PPS</th>

            </tr>
             <tr style="background-color: #FFFFFF">
                <td align="right">1</td>
                <td>Preface</td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne_Preface.pdf"><asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Preview.png" width="25px" Height="25px"/></asp:HyperLink>--%></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="UserGuide.aspx"><asp:Image ID="Image4" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink1c" runat="server" NavigateUrl="Files/V1_AJAXOne_Preface.pps"><asp:Image ID="Image1c" runat="server" ImageUrl="~/Images/save.png"  width="25px" Height="25px" /></asp:HyperLink></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">2</td>
                <td>Get Started</td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne_GetStarted.pdf"><asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Preview.png" width="25px" Height="25px"/></asp:HyperLink>--%></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="UserGuide.aspx"><asp:Image ID="Image5" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink2c" runat="server" NavigateUrl="Files/V1_AJAXOne_GetStarted.pps"><asp:Image ID="Image2c" runat="server" ImageUrl="~/Images/save.png"  width="25px" Height="25px" /></asp:HyperLink></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">3</td>
                <td>Pre-Sales</td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne.pdf"><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Preview.png" width="25px" Height="25px"/>--%></asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="UserGuide.aspx"><asp:Image ID="Image2" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="Files/V1_AJAXOne_PreSales.pps"><asp:Image ID="Image3" runat="server" ImageUrl="~/Images/save.png"  width="25px" Height="25px" /></asp:HyperLink></td>
            </tr>
             <tr style="background-color: #FFFFFF">
                <td align="right">4</td>
                <td>Parts</td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne.pdf">View</asp:HyperLink>--%></td>
                <td align="center">
                   <%-- <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="UserGuide.aspx">Play</asp:HyperLink>--%></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="Files/V1_AJAXOne.pps">Download</asp:HyperLink>--%></td>
            </tr>
             <tr style="background-color: #FFFFFF">
                <td align="right">5</td>
                <td>Service</td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne.pdf">View</asp:HyperLink>--%></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="UserGuide.aspx">Play</asp:HyperLink>--%></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="Files/V1_AJAXOne.pps">Download</asp:HyperLink>--%></td>
            </tr>
             <tr style="background-color: #FFFFFF">
                <td align="right">6</td>
                <td>Presentation</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne.pdf"><asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Preview.png" width="25px" Height="25px"/></asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="UserGuide.aspx"><asp:Image ID="Image5" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="Files/V1_AJAXOne.pps"><asp:Image ID="Image6" runat="server" ImageUrl="~/Images/save.png"  width="25px" Height="25px" /></asp:HyperLink></td>
            </tr>


        </table>


        <%--<div style="text-align: left; font-size: xx-large; font-family: Calibri; font-weight: bold;">
            Content<br />
            <div style="text-align: left; font-size: x-large; font-family: Calibri; font-weight: bold;">
                &nbsp;&nbsp;1)
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOneI.pdf">About DMS</asp:HyperLink><br />

                &nbsp;&nbsp;2)
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne.pdf">Document (PDF) </asp:HyperLink><br />

                &nbsp;&nbsp;3)
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="UserGuide.aspx">User Guide (Video) </asp:HyperLink><br />


                &nbsp;&nbsp;4)
            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="Files/V1_AJAxOne.pps">Presentation</asp:HyperLink>
            </div>
        </div>--%>
    </body>
    </html>

</asp:Content>

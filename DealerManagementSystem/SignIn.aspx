<%@ Page Title="AJAX-DMS SignIn" Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="DealerManagementSystem.SignIn" Async="true" %>

<!DOCTYPE html>
<head runat="server">
    <title>AJAX-DMS SignIn</title>

    <style>
        .loginFooter{
            font-size :14px;

        }
    </style>
</head>

<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
<link href="Ajax/Images/dms4.jpg" rel="shortcut icon" type="image/x-icon" />

<body onload="SetApp()">
    <!------LOG IN------LOG IN------LOG IN------LOG IN------LOG IN------LOG IN------LOG IN------LOG IN------LOG IN------LOG IN------LOG IN------->
    <div style="text-align: center; font-family: Calibri; /*background-color: #FFFFCC; */">
        <form id="form1" runat="server">
            <table border="1" style="border: thick solid #C0C0C0; text-align: right; margin-top: 10%; margin-right: 34%; margin-left: 34%; width: 30%;">
                <tr>
                    <td colspan="1" style="text-align: left; border-style: solid hidden hidden solid">
                        <img src="Ajax/Images/Ajax-New-Logo.png" border="0" width="150" height="45">
                    </td>
                    <td style="text-align: right; border-style: solid hidden hidden hidden"></td>
                    <td colspan="1" style="text-align: right; border-style: solid solid hidden solid">
                        <asp:Image ID="apppic" runat="server" ImageUrl="Ajax/Images/dms4.jpg" Height="50" Width="50" ImageAlign="AbsMiddle" />

                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="border-style: solid solid hidden solid; text-align: center;">
                        <%--<td colspan="3" style="border-style: solid solid hidden solid; background-color: #000099; color: #FFFFFF; text-align: center;"> Self Loader | Batching Plant | Pumps--%>
                        <%--<td colspan="3"> <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/ajax_ribbon.jpg" Height="20" Width="100%" ImageAlign="AbsMiddle" />--%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center; font-size: xx-large; border-style: solid solid hidden solid">Sign in 
                    </td>
                </tr>
                <tr style="background-color:#30526d; color: #FFFFFF; font-weight: bold">                 
                    <td id="apptitle" colspan="3" style="text-align: center; font-size: larger; border-style: solid solid hidden solid">
                        <asp:Image ID="ImageApp" Width="80px" Height="35px" ImageUrl="/Ajax/Images/dms6.jpg" AlternateText="rr1" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td colspan="3" style="border-style: solid solid hidden solid">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; border-style: solid hidden hidden solid">User Id:</td>
                    <td colspan="2" style="text-align: left; border-style: solid solid hidden solid">
                        <asp:TextBox ID="txtLoginID" runat="server" Width="300px" ForeColor="Blue" Font-Bold="false" Height="24px" PlaceHolder="Enter Your Login Account.." Required="true" AutoCompleteType="Email"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; border-style: solid hidden hidden solid">
                        <asp:Image ID="Image2" runat="server" Height="20px" ImageUrl="Ajax/Images/Login1.png" Width="25px" />
                        Password:</td>
                    <td colspan="2" style="text-align: left; border-style: solid solid hidden solid">
                        <asp:TextBox ID="txtPassword" runat="server" Width="300px" ForeColor="Blue" Font-Bold="false" Height="24px" TextMode="Password" Placeholder="Enter Password.."></asp:TextBox></td>
                </tr>
                <tr>
                    <%-- <td colspan="3" style="border-style: solid solid hidden solid">
                        <br />
                    </td>--%>
                    <td style="text-align: right; border-style: solid hidden hidden solid">
                        <%--<asp:Image ID="Image3" runat="server" Height="20px" ImageUrl="~/Images/Login1.png" Width="25px" />--%>
                        Module:</td>
                    <td colspan="2" style="text-align: left; border-style: solid solid hidden solid">
                        <%--<asp:TextBox ID="TextBox1" runat="server" Width="300px" ForeColor="Blue" Font-Bold="false" Height="24px" TextMode="Password" Placeholder="Enter Password.."></asp:TextBox>--%>
                        <asp:DropDownList ID="DropDownList1" runat="server" OnChange="dconfirm();" Width="300px" ForeColor="Blue">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem>Pre-Sales</asp:ListItem>
                            <asp:ListItem>Sales</asp:ListItem>
                            <asp:ListItem>Service</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td colspan="1" style="border-style: solid hidden hidden solid">
                        <%--I am Part of &nbsp; --%>
                    <td colspan="1" style="border-style: solid solid hidden solid; text-align: left;">
                        <%--<asp:DropDownList ID="DDL_UserType" runat="server">                           
                            <asp:ListItem Selected="True" Value="Dealer Parts Team">Dealer Parts Team</asp:ListItem>
                            <asp:ListItem>Dealer Service Team</asp:ListItem>  --%>
                        <%-- <asp:ListItem Selected="True" Value="Customer">Customer</asp:ListItem>--%>
                        <%--<asp:ListItem>AJAXcion</asp:ListItem>--%>
                        <%--</asp:DropDownList>  --%>                      
                    </td>
                    <td colspan="1" style="border-style: hidden solid hidden hidden">
                        <asp:LinkButton ID="lnkForgotPassword" runat="server" Text="Forgot Password" OnClick="lnkForgotPassword_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>

                <tr>
                    <td colspan="3" style="border-style: solid solid hidden solid">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="border-width: thick; border-color: #C0C0C0; text-align: left; border-style: solid hidden solid solid">&nbsp;&nbsp;                     
                        <%--<asp:LinkButton ID="lnkCreateAccount" runat="server" Text="New User Registration" PostBackUrl="~/eCatalogue/Account.aspx"></asp:LinkButton>--%></td>
                    <td style="border-width: thick; border-color: #C0C0C0; text-align: left; border-style: solid solid solid solid">
                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                    <td style="border-width: thick; border-color: #C0C0C0; text-align: right; border-style: solid solid solid hidden">
                        <asp:Button ID="btnLogin" runat="server" Text="Sign in" CssClass="btn btn-primary" OnClick="btnLogin_Click" BackColor="#3366FF" ForeColor="White" Height="35" Width="90" Font-Names="Calibri" Font-Size="Medium" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; border-style: solid hidden hidden hidden">Language: English </td>
                    <td style="text-align: center; border-style: solid solid hidden solid">
                        <%--<asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>--%>
                    </td>
                    <td style="text-align: right; border-style: solid hidden hidden hidden">
                        <asp:HyperLink ID="xHyperLink" runat="server" onclick="myFunction()" NavigateUrl='#' Text="Help"></asp:HyperLink></td>
                </tr>

            </table>
            <div id="help" style="display: none;">
                <table border="1" style="border: thin dotted #C0C0C0; text-align: right; margin-top: 0%; margin-right: 34%; margin-left: 34%; width: 30%; background-color: #FFFFE8;">
                    <tr style="text-align: left;">
                        <td>* If you are a New User, ask Admin for your Sign In Credentials.
                            <%--<asp:LinkButton ID="lnkCreateAccount1" runat="server" Text="Create Your Login Account." PostBackUrl="~/eCatalogue/Account.aspx"></asp:LinkButton>--%>
                            <br />
                            <%-- * While creating, please specify your active E-Mail id as your login account & Mobile No--%>
                            <br />
                            <%--&nbsp;&nbsp;&nbsp;for better management of your acccess to this portal.--%>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
        <br>
        <div class="loginFooter">AJAX Business System © 2021 | <a href="#" id="terms_and_conditions_link">Feedback</a> &nbsp;|&nbsp;  <a href="#" id="contact_us_link">Contact Us</a></div>
        <div style="text-align: center; margin: 0px 0;">
            <p style="margin: 0px; padding: inherit; "><font size="2px">Download Our Mobile App</font></p>
           
                <img src="/ajax/images/apple__v259.png" border="0" id="" alt="">
          
                <img src="/ajax/images/android__v259.png" border="0" id="" alt="">
        </div>

        
    </div>
    <script>


        function myFunction() {
            var helptab = document.getElementById("help");
            if (helptab.style.display == 'none') {
                helptab.style.display = 'block';
            }
            else {
                helptab.style.display = 'none';
            }

        }


        function dconfirm() {
            <%--var e = document.getElementById("<%=DropDownList1.ClientID%>");
            var selVal = e.options[e.selectedIndex].value;

            var e2 = document.getElementById("apptitle");
            var e3 = document.getElementById("apppic");

            if (selVal == 'All') {
                e2.innerHTML = 'Dealer Management System';
                e2.style.backgroundColor = "#30526d";
                apppic.src = "/Ajax/Images/dms4.jpg";
            }
            else if (selVal == 'Pre-Sales') {
                e2.innerHTML = 'Pre-Sales';
                apppic.src = "/Ajax/Images/dms4.jpg";
                e2.style.backgroundColor = "#669900";
            }
            else if (selVal == 'Sales') {
                e2.innerHTML = 'Sales';
                apppic.src = "/Ajax/Images/dms4.jpg";
                e2.style.backgroundColor = "#CC66FF";
            }
            else if (selVal == 'Service') {
                e2.innerHTML = 'Service';
                apppic.src = "/Ajax/Images/dms4.jpg";
                e2.style.backgroundColor = "#CC66FF";
            }--%>
            

        }


        function SetApp() {

            //var app = getParameterByName('App');
            var app = 'All';
            var e4 = document.getElementById("DropDownList1");
            e4.value = app;
            dconfirm();
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
</body>

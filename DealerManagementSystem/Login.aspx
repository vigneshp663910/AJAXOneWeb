<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DealerManagementSystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="icon" href="Ajax/AF2.jpg" type="image/x-icon">
    <title>AJAX-Portal Login</title>
    <link href="Styles/Login.css" rel="stylesheet" type="text/css" />

    <script>
        /* function geoFindMe() { */

        //const status = document.querySelector('#status');
        //const mapLink = document.querySelector('#map-link');

        //mapLink.href = '';
        //mapLink.textContent = '';

        function success(position) {
            const latitude = position.coords.latitude;
            const longitude = position.coords.longitude;
            document.getElementById('hfLatitude').value = latitude;
            document.getElementById('hfLongitude').value = longitude;
            status.textContent = '';
            //  mapLink.href = `https://www.openstreetmap.org/#map=18/${latitude}/${longitude}`;
            //  mapLink.textContent = `Latitude: ${latitude} °, Longitude: ${longitude} °`; 
        }
        function error() {
            status.textContent = 'Unable to retrieve your location';
        }

        if (!navigator.geolocation) {
            status.textContent = 'Geolocation is not supported by your browser';

        } else {
            status.textContent = 'Locating…';
            navigator.geolocation.getCurrentPosition(success, error);
        }

        /*  } */
     //   document.querySelector('#find-me').addEventListener('click', geoFindMe);
    </script>
</head>
<body>
    <body style="background-image: url(../Ajax/LoginBackground.png)">

        <form id="form1" runat="server">
            <asp:HiddenField ID="hfLatitude" runat="server" />
            <asp:HiddenField ID="hfLongitude" runat="server" />

            <link href='http://fonts.googleapis.com/css?family=Source+Sans+Pro:600' rel='stylesheet' type='text/css'>
            <div class="centered">
                <div style="background-image: url(../LoginImages/LoginBGButton.png); width: 303px; height: 320px">


                    <%--<asp:Label ID="lblMessage" runat="server"></asp:Label>--%>
                    <div style="padding-left: 30px; padding-top: 82px;">
                        <p>
                            <asp:Label ID="lblUsername" runat="server" CssClass="label" Text="<%$ Resources:Resource, lgnlblUsername %>"></asp:Label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="user_icon" Width="191px" Height="30px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" ToolTip="<%$ Resources:Resource,ttpUsername %>" ForeColor="Red"><img src="images/error_info.png" alt="info" /></asp:RequiredFieldValidator>
                        </p>
                    </div>
                    <div style="padding-left: 30px; padding-top: 15px;">
                        <p>
                            <asp:Label ID="lblPassword" runat="server" CssClass="label" Text="<%$ Resources:Resource, lgnlblpwd %>"></asp:Label>

                            <asp:TextBox ID="txtPassword" runat="server" CssClass="pass_icon" TextMode="Password" Width="191px" Height="30px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ToolTip="<%$ Resources:Resource,ttpPassword %>" ForeColor="Red"><img src="images/error_info.png" alt="info" /></asp:RequiredFieldValidator>
                        </p>
                    </div>
                    <div style="padding-left: 30px; padding-top: 29px">
                        <p>
                            <asp:Button ID="btnLogin" runat="server" Width="101px" Height="32px" Style="background-image: url(../LoginImages/Login-Button.png);" OnClick="btnLogin_Click" />
                            <asp:LinkButton ID="lForgetPassword" runat="server" OnClick="lForgetPassword_Click" ForeColor="Red">Forget Password</asp:LinkButton>
                        </p>
                    </div>
                    <%-- <h1><a href="ForgotPassword.aspx">Forgot password ?</a></h1>--%>
                    <div style="text-align: center">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>

                </div>

            </div>
        </form>
    </body>
</body>
</html>

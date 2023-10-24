<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DealerManagementSystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" href="../Ajax/Images/dms4.jpg" type="image/x-icon">
    <title>AJAX-DMS | Sign in</title>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <style>
        @import url('https://fonts.googleapis.com/css?family=Raleway:400,700');

        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
            font-family: Raleway, sans-serif;
        }

        body {
            background: linear-gradient(90deg, #C7C5F4, #776BCC);
        }

        .container {
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 100vh;
        }

        .screen {
            background: linear-gradient(90deg, #5D54A4, #7C78B8);
            position: relative;
            height: 600px;
            width: 360px;
            box-shadow: 0px 0px 24px #5C5696;
        }

        .screen__content {
            z-index: 1;
            position: relative;
            height: 100%;
        }

        .screen__background {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            z-index: 0;
            -webkit-clip-path: inset(0 0 0 0);
            clip-path: inset(0 0 0 0);
        }

        .screen__background__shape {
            transform: rotate(45deg);
            position: absolute;
        }

        .screen__background__shape1 {
            height: 520px;
            width: 520px;
            background: #FFF;
            top: -50px;
            right: 120px;
            border-radius: 0 72px 0 0;
        }

        .screen__background__shape2 {
            height: 220px;
            width: 220px;
            background: #6C63AC;
            top: -172px;
            right: 0;
            border-radius: 32px;
        }

        .screen__background__shape3 {
            height: 540px;
            width: 190px;
            background: linear-gradient(270deg, #5D54A4, #6A679E);
            top: -24px;
            right: 0;
            border-radius: 32px;
        }

        .screen__background__shape4 {
            height: 400px;
            width: 200px;
            background: #7E7BB9;
            top: 420px;
            right: 50px;
            border-radius: 60px;
        }

        .login {
            width: 320px;
            padding: 30px;
            padding-top: 156px;
        }

        .login__field {
            padding: 20px 0px;
            position: relative;
        }

        .login__icon {
            position: absolute;
            top: 30px;
            color: #7875B5;
        }

        .login__input {
            border: none;
            border-bottom: 2px solid #D1D1D4;
            background: none;
            padding: 10px;
            padding-left: 24px;
            font-weight: 700;
            width: 75%;
            transition: .2s;
        }

            .login__input:active,
            .login__input:focus,
            .login__input:hover {
                outline: none;
                border-bottom-color: #6A679E;
            }

        .login__submit {
            background: #fff;
            font-size: 14px;
            margin-top: 30px;
            padding: 16px 20px;
            border-radius: 26px;
            border: 1px solid #D4D3E8;
            text-transform: uppercase;
            font-weight: 700;
            display: flex;
            align-items: center;
            width: 100%;
            color: #4C489D;
            box-shadow: 0px 2px 2px #5C5696;
            cursor: pointer;
            transition: .2s;
        }

            .login__submit:active,
            .login__submit:focus,
            .login__submit:hover {
                border-color: #6A679E;
                outline: none;
            }

        .button__icon {
            font-size: 24px;
            margin-left: auto;
            color: #7875B5;
        }

        .social-login {
            position: absolute;
            height: 140px;
            width: 160px;
            text-align: center;
            bottom: 0px;
            right: 0px;
            color: #fff;
        }

        .social-icons {
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .social-login__icon {
            padding: 20px 10px;
            color: #fff;
            text-decoration: none;
            text-shadow: 0px 0px 8px #7875B5;
        }

            .social-login__icon:hover {
                transform: scale(1.5);
            }
    </style>

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
    <div class="container">
        <div class="screen">
            <div class="screen__content">
                <form class="login" id="form2" runat="server">
                    <asp:HiddenField ID="hfLatitude" runat="server" />
                    <asp:HiddenField ID="hfLongitude" runat="server" />
                    <div class="login__field">
                       <%-- <i class="login__icon fas fa-user"></i>--%>
                        <%--<input type="text" class="login__input" placeholder="User name / Email">--%>
                        <asp:Label ID="lblUsername" runat="server" CssClass="label" Text="<%$ Resources:Resource, lgnlblUsername %>"></asp:Label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="login__input" Width="191px" Height="30px" placeholder="User name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" ToolTip="<%$ Resources:Resource,ttpUsername %>" ForeColor="Red"><img src="images/error_info.png" alt="info" /></asp:RequiredFieldValidator>
                    </div>
                    <div class="login__field">
                        <%--<i class="login__icon fas fa-lock"></i>--%>
                       
                        <%--<input type="password" class="login__input" placeholder="Password">--%>
                        <asp:Label ID="lblPassword" runat="server" CssClass="label" Text="<%$ Resources:Resource, lgnlblpwd %>"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="login__input" TextMode="Password" Width="191px" Height="30px" placeholder="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ToolTip="<%$ Resources:Resource,ttpPassword %>" ForeColor="Red"><img src="images/error_info.png" alt="info" /></asp:RequiredFieldValidator>

                    </div>
                    <button class="button login__submit" >
                        <%--<span class="button__text">Log In Now</span>--%>
                        <asp:Button ID="btnLogin" runat="server" Width="221px" Height="32px" class="button__text" Text="Log In Now" OnClick="btnLogin_Click" BorderStyle="None" BackColor="#64679E" ForeColor="White" />
                        <%--<i class="button__icon fas fa-chevron-right"></i>--%>
                         <i class="fa fa-sign-in" aria-hidden="true"></i>
                    </button>
                    <asp:LinkButton ID="lForgetPassword" runat="server" OnClick="lForgetPassword_Click" ForeColor="red" style="font-size:small;">Forget Password</asp:LinkButton>
                    <div style="text-align: center" >
                        <asp:Label ID="lblMessage" runat="server" style="font-size:small;"></asp:Label>
                    </div>
                </form>
                <div class="social-login">
                    <h3><i class="fa fa-bolt" aria-hidden="true"></i></h3>
                    <div class="social-icons">
                        <%--<a href="#" class="social-login__icon fab fa-instagram"></a>
                        <a href="#" class="social-login__icon fab fa-facebook"></a>
                        <a href="#" class="social-login__icon fab fa-twitter"></a>--%>
                    </div>
                </div>
            </div>
            <div class="screen__background">
                <span class="screen__background__shape screen__background__shape4"></span>
                <span class="screen__background__shape screen__background__shape3"></span>
                <span class="screen__background__shape screen__background__shape2"></span>
                <span class="screen__background__shape screen__background__shape1"></span>
            </div>
        </div>
    </div>
</body>
</html>
<%--#64679E, #5D54A4, #776BCC #C7C5F4 --%>
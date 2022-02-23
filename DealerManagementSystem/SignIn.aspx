<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="DealerManagementSystem.SignIn" %>


<!DOCTYPE html>

<html>
<head>
    <link rel="icon" href="../Ajax/Images/dms4.jpg" type="image/x-icon">
    <title>AJAX-DMS | Sign in</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="../CSS/bootstrap.min.4.5.2.css" />


    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
        }

        * {
            box-sizing: border-box;
        }

        .fieldset-border {
            
            border: solid 1px #cacaca;
            margin: 15px 0;
            border-radius: 5px;
            padding: 10px;
        }

            .fieldset-border legend {
                width: auto;
                background: #fff;
                padding: 0 4px;
                color: #313131;
                font-size: 14px;
                margin-bottom: 5px;
                border: none;
                font-weight: 500;
            }

        fieldset {
            padding: 5px;
        }

            fieldset legend {
                font-weight: bold;
            }
        /* style the container */
        .container {
            position: relative;
            border-radius: 5px;
            background-color: #f2f2f2;
            padding: 20px 0 30px 0;
        }

        /* style inputs and link buttons */
        input,
        .btn {
            width: 100%;
            padding: 12px;
            border: solid 1px #cacaca;
            background-color: rgb(245 248 255);
            border-radius: 4px;
            margin: 5px 0;
            opacity: 0.85;
            display: inline-block;
            font-size: 17px;
            line-height: 20px;
            text-decoration: none; /* remove underline from anchors */
        }

            input:hover,
            .btn:hover {
                opacity: 1;
            }

        /* add appropriate colors to fb, twitter and google buttons */
        .fb {
            background-color: #3B5998;
            color: white;
        }

        .twitter {
            background-color: #55ACEE;
            color: white;
        }

        .google {
            background-color: #dd4b39;
            color: white;
        }

        /* style the submit button */
        input[type=submit] {
            background-color: #04AA6D;
            color: white;
            cursor: pointer;
        }

            input[type=submit]:hover {
                background-color: #45a049;
            }

        /* Two-column layout */
        /*.col {
            float: left;
            width: 50%;
            margin: auto;
            padding: 0 50px;
            margin-top: 6px;
        }*/

        /* Clear floats after the columns */
        /*.row:after {
            content: "";
            display: table;
            clear: both;
        }*/

        /* vertical line */
        /*.vl {
            position: absolute;
            left: 50%;
            transform: translate(-50%);
            border: 2px solid #ddd;
            height: 175px;
        }*/

        /* text inside the vertical line */
        /*.vl-innertext {
            position: absolute;
            top: 50%;
            transform: translate(-50%, -50%);
            background-color: #f1f1f1;
            border: 1px solid #ccc;
            border-radius: 50%;
            padding: 8px 10px;
        }*/

        /* hide some text on medium and large screens */
        /*.hide-md-lg {
            display: none;
        }*/

        /* bottom container */
        /*.bottom-container {
            text-align: center;
            background-color: #666;
            border-radius: 0px 0px 4px 4px;
        }*/

        /* Responsive layout - when the screen is less than 650px wide, make the two columns stack on top of each other instead of next to each other */
        /*@media screen and (max-width: 650px) {
            .col {
                width: 100%;
                margin-top: 0;
            }*/
        /* hide the vertical line */
        /*.vl {
                display: none;
            }*/
        /* show the hidden text on small screens */
        /*.hide-md-lg {
                display: block;
                text-align: center;
            }
        }*/

        .modal-lc {
            max-width: 20%;
        }

        /* Split the screen in half */
        .split {
            height: 100%;
            position: fixed;
            z-index: 1;
            top: 0;
            overflow-x: hidden;
            padding-top: 20px;
        }

        /* Control the left side */
        .left {
            left: 0;
            width: 50%;
            padding: initial;
            background-color: #fff;
        }

        /* Control the right side */
        .right {
            right: 0;
            width: 50%;
            padding: initial;
        }
        /* Control the top side */
        .top {
            top: 0;
            height: 50%;
            width: 50%;
            padding: initial;
            background-color: #fff;
        }

        /* Control the bottom side */
        .bottom {
            top: 50%;
            bottom: 0;
            height: 50%;
            width: 50%;
            padding: initial;
        }

        /* If you want the content centered horizontally and vertically */
        .centered {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            text-align: center;
        }

            /* Style the image inside the centered container, if needed */
            .centered img {
                width: 150px;
                border-radius: 50%;
            }

        .vertical-center {
            margin: 0;
            padding: 100px;
            position: absolute;
            top: 50%;
            left: 10%;
            -ms-transform: translateY(-50%);
            transform: translateY(-50%);
        }

        #ImageCompanyLogoRight {
            display: none;
        }

        @media screen and (min-device-width: 320px) and (max-device-width: 768px) {
            #LoginLeft {
                display: none;
            }

            #Footer {
                display: none;
            }

            #ImageCompanyLogoRight {
                display: block;
            }

            #LoginRight {
                width: 100%;
            }


            .vertical-center {
                margin: 0;
                padding: initial;
                position: absolute;
                top: 50%;
                left: 10%;
                -ms-transform: translateY(-50%);
                transform: translateY(-50%);
            }
        }
    </style>


</head>
<body>
    <div>
        <form id="form1" runat="server">
            <div class="row">
                <div id="LoginLeft" class="left split">
                    <div class="split left">
                        <asp:Image ID="Image1" runat="server" Width="100%" Height="100%" ImageUrl="~/Ajax/Images/bg01.jpg" />
                        <div class="vertical-center" style="padding: 198px; text-align: center">
                            <asp:Image ID="ImageCompanyLogo" runat="server" ImageUrl="~/Ajax/Images/ajax_logow.png" Height="70" Width="150" />
                            <h3 style="font-family: Calibri; color: white;">DELAER MANAGEMENT SYSTEM</h3>
                        </div>
                    </div>
                    <%--<div class="split bottom" style="padding: 128px; background: linear-gradient(180deg, #b7babf, #f0f4fd,#b7babf);">
                        <h2>Ajax-XXXXXXXXXXXX</h2>
                    </div>--%>
                </div>
                <div id="LoginRight" class="right split" style="margin: 0 auto">
                    <div class="col-md-12 vertical-center" style="width: 80%">
                        <div id="ImageCompanyLogoRight" style="text-align: center;">
                            <asp:Image ID="ImageCompanyLogo2" runat="server" ImageUrl="~/Ajax/Images/Ajax-New-Logo.png" Height="40" Width="150" />
                        </div>
                        <br />
                        <div style="text-align: center;">
                            <asp:Image ID="ImageAppLogo" runat="server" ImageUrl="~/Ajax/Images/dms4.jpg" Width="50" Height="50" />
                        </div>

                        <%--<div style="text-align: center;background-color:#2e516d">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Ajax/Images/dms6.jpg" Width="130" Height="60" />
                        </div>--%>

                        <fieldset class="fieldset-border">

                            <legend style="background: none; color: #007bff; font-size: 20px;">Sign in</legend>
                            <div class="col-md-12">
                                <div>
                                    <br />
                                    <label><b>UserID / Email / Mobile</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtUsername" runat="server" ToolTip="Enter User Id / Email Id / Mobile No..." PlaceHolder="UserID / Email / Mobile"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtUsername" ToolTip="<%$ Resources:Resource,ttpUsername %>" ForeColor="Red"><img src="images/error_info.png" alt="info" />

                                    </asp:RequiredFieldValidator>
                                    <%-- <input type="text" name="username" id="txtusername" runat="server" placeholder="Username" required>--%>
                                </div>
                                <div>
                                    <%--<br />--%>
                                    <label><b>Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtPassword" runat="server" ToolTip="Enter Password..." PlaceHolder="Password" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ToolTip="<%$ Resources:Resource,ttpPassword %>" ForeColor="Red"><img src="images/error_info.png" alt="info" /></asp:RequiredFieldValidator>

                                    <%--<input type="password" name="password" id="txtpassword" runat="server" placeholder="Password" required>--%>
                                </div>
                                <%--  <br />--%>
                                <%--  <input type="submit" value="Login">--%>
                                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                                <div style="text-align: center">
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <br />
                                <asp:LinkButton ID="LinkButton1" runat="server">Forgot password?</asp:LinkButton>
                                <%--<br />
                                <br />
                                <label>Don't have ajax account?</label>
                                <input type="submit" value="Create an account" class="btn-danger">--%>
                            </div>
                        </fieldset>
                        <%--  </div>--%>

                        <div id="Footer1">
                            <div style="text-align: center; margin: 10px 0;">
                                <p><font size="2px">Powered by AJAXOne&nbsp;&copy; <%: DateTime.Now.Year %> </font></p>

                                <div id="Footer">
                                    <p><font size="2px"><a href="#" data-toggle="modal" data-target="#myModal">Download Our Mobile App </a></font></p>
                                    <p>
                                        <span>
                                            <a href="#" data-toggle="modal" data-target="#myModal">
                                                <img src="../Images/apple.png" border="0" id="" alt="">
                                            </a>
                                        </span>
                                        <span>
                                            <%--<a href="https://play.google.com/store/apps/details?id=com.ajaxengg.hr_app">
                                                <img src="../Images/Playstore.png" border="0" id="" alt="">
                                            </a>--%>

                                            <a href="#" data-toggle="modal" data-target="#myModal">
                                                <img src="../Images/Playstore.png" border="0" id="" alt="">
                                            </a>


                                        </span>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>


    <div id="myModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header" style="color: #999999; height: 50px;">
                    <h5 class="modal-title" style="color: #0000FF"><b>AJAX Mobile Apps Library</b></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table id="tab_mobile" border="1" style="font-family: Calibri; font-size: medium">
                        <tr style="background-color: black; color: white">
                            <th>SN
                            </th>
                            <th style="width: 200px; text-align: center">App
                            </th>
                            <th style="width: 100px; text-align: center">Mode
                            </th>
                            <th style="width: 100px; text-align: center">Features
                            </th>
                            <th style="width: 100px">
                                <img src="../Images/Playstore.png" border="0" id="" alt="">
                            </th>
                            <th style="width: 100px">
                                <img src="../Images/apple.png" border="0" id="" alt="">
                            </th>
                            <th style="width: 600px; text-align: center">Remarks
                            </th>
                        </tr>
                        <tr>
                            <td style="text-align: right">1</td>
                            <td>AJAX One</td>
                            <td>Online</td>
                            <td>All</td>
                            <td style="text-align: center"><a href="https://play.google.com/store/apps/details?id=com.ajaxengg.hr_app">Install</a></td>
                            <td style="text-align: center">Install</td>
                            <td style="width: 100px; text-align: left">Includes both Pre-Sales & Service</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">2</td>
                            <td>Pre-Sales</td>
                            <td>Offline</td>
                            <td>Role Based</td>
                            <td style="text-align: center">Install</td>
                            <td style="text-align: center">Install</td>
                            <td style="width: 100px; text-align: left">Customer, Lead, Activity & Quotation</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">3</td>
                            <td>Service</td>
                            <td>Offline</td>
                            <td>Role Based</td>
                            <td style="text-align: center">Install</td>
                            <td style="text-align: center">Install</td>
                            <td style="width: 100px; text-align: left">IC Tickets, Customer Feedback</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">4</td>
                            <td>Customer</td>
                            <td>Online</td>
                            <td>Standard</td>
                            <td style="text-align: center">Install</td>
                            <td style="text-align: center">Install</td>
                            <td style="width: 100px; text-align: left">To Develop in Phase-2</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">5</td>
                            <td>Operator</td>
                            <td>Online</td>
                            <td>Standard</td>
                            <td style="text-align: center">Install</td>
                            <td style="text-align: center">Install</td>
                            <td style="width: 100px; text-align: left">For M/C Operators & Customers Only</td>
                        </tr>


                    </table>

                </div>

            </div>
        </div>
    </div>

    <%--<script>

        function openModal() {
            $('#myModal').modal('show')
        };
    </script>--%>
</body>
</html>

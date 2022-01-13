<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.SignIn" %>

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="../CSS/bootstrap.min.4.5.2.css" />
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
            width:50%;
            padding: initial;
            background-color: #fff;
        }

        /* Control the bottom side */
        .bottom {
            top:50%;
            bottom: 0;
            height: 50%;
            width:50%;
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

        @media screen and (min-device-width: 320px) and (max-device-width: 768px) {
            #LoginLeft {
                display: none;
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
                <div id="LoginLeft" class="split left">
                    <div class="split top">
                        <asp:Image ID="Image1" runat="server" Width="100%" Height="100%" ImageUrl="~/Ajax/Images/bg01.jpg" />
                    </div>
                    <div class="split bottom" style="padding: 128px;background: linear-gradient(180deg, #b7babf, #f0f4fd,#b7babf);">
                        <h2>Ajax-XXXXXXXXXXXX</h2>
                    </div>
                </div>
                <div id="LoginRight" class="split right" style="margin: 0 auto">
                    <div class="col-md-12 vertical-center" style="width: 80%">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 20px;">Sign in</legend>
                            <div class="col-md-12">
                                <div>
                                    <br />
                                    <label><b>UserID / Email / Mobile</b></label>
                                </div>
                                <div>
                                    <input type="text" name="username" id="txtusername" runat="server" placeholder="Username" required>
                                </div>
                                <div>
                                    <br />
                                    <label><b>Password</b></label>
                                </div>
                                <div>
                                    <input type="password" name="password" id="txtpassword" runat="server" placeholder="Password" required>
                                </div>
                                <br />
                                <input type="submit" value="Login">
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
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>

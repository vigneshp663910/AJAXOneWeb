<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="SpcAssemblyDrawingView.aspx.cs" Inherits="DealerManagementSystem.ViewECatalogue.SpcAssemblyDrawingView" %>

<%@ Register Src="~/ViewECatalogue/UserControls/SpcAssemblyDView.ascx" TagPrefix="UC" TagName="UC_SpcAssemblyDView" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        window.history.forward();
    </script>

    <link rel="icon" href="Ajax/Images/dms4.jpg" type="image/x-icon">
    <title>AJAX-Dealer Management</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
     <link rel="stylesheet" href="../CSS/w3.css"> 
    <link rel="stylesheet" href="../CSS/font.css">
    <link rel="stylesheet" href="../CSS/StyleSheet1.css" /> 

    <script src="/JS/jquery.min.js"></script> 
    <script src="/JS/bootstrap.min.js"></script>
    <link rel="stylesheet" href="../CSS/bootstrap.min.4.5.2.css" /> 
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">     
    <link href="https://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" rel="Stylesheet">
    <style>
        div#divChangeCustomer {
            text-align: right;
            margin-top: -40px;
            position: relative;
        }

        @media only screen and (max-width: 767px) {
            div#divChangeCustomer {
                margin-top: 0;
                text-align: center;
            }
        }

        #divChangeCustomer .btn {
            width: auto;
            height: auto;
            padding: 12px;
            cursor: pointer;
        }

        .ui-autocomplete {
            top: -38.875px;
            left: 756.225px;
            max-width: 388.5px;
            width: 390px;
            z-index: 10002;
            overflow: auto;
            height: 300px; 
        }

            .ui-autocomplete li {
                padding: 0px !important;
                background-image: linear-gradient(#fffcfc, #c0ebf2);
            }

                .ui-autocomplete li:hover {
                    padding: 0px !important;
                    background: #f3f2f2; 
                }

                .ui-autocomplete li a.ui-state-focus {
                    background: none;
                }

                .ui-autocomplete li a label {
                    font-size: 12px;
                    font-weight: normal;
                }

                .ui-autocomplete li a.customer .customer-info {
                    display: flex;
                    flex-wrap: nowrap;
                    justify-content: space-between;
                }

                    .ui-autocomplete li a.customer .customer-info label {
                        font-weight: bold;
                    }

                .ui-autocomplete li a.customer p.customer-name-info {
                    margin: 0;
                }

            .ui-autocomplete a.customer .customer-type {
                color: green;
            }

            .ui-autocomplete li a.customer p.customer-name-info label {
                color: darkblue;
                font-weight: bold;
            }

            .ui-autocomplete a.customer .contact-number {
                color: darkslateblue;
            }

            .ui-autocomplete a.customer .customer-address {
                color: #107cf1;
                font-weight: bold;
                margin: 0;
            }

        .customerAuto-View .col-6 {
            padding: 0;
        }
    </style>

    <style>
        .LabelValue {
            color: blue;
            font-weight: bold;
            padding-left: 5px;
            font-size: 14px;
            font-family: system-ui;
        }
    </style>

    <style>
        .col-md-12 {
            padding: 0px 12px 12px 12px;
        }

        .w3-btn, .w3-button {
            text-decoration: initial; 
        }

        .w3-btn, .w3-button {
            padding: 1px 1px !important; 
        } 
        .topnav a {
            float: left;
            color: #f2f2f2;
            text-align: center;
            padding: 2px 20px;
            text-decoration: none;
            font-size: 15px;
        }

        body, h1, h2, h3, h4, h5 {
            font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
        }

        body {
            font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
            font-size: 14px;
        }

        .form-check {
            display: initial;
        }

        .w3-half img {
            margin-bottom: -6px;
            margin-top: 16px;
            opacity: 0.8;
            cursor: pointer
        }

            .w3-half img:hover {
                opacity: 1
            }

        .w3-padding {
            padding: 0px 16px !important;
        }
                .ajaxheader {
            
            background-image: linear-gradient(to bottom,#2e506d,#325471);
            background-repeat: no-repeat;
            float: left;
            width: 100%;
            height: 50px;
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0; 
            z-index: 99;
            padding: 0px 15px 0px 23px; 
        }

        .ajaxheaderinner {
            width: 100%;
            float: left;
            min-height: 50px;
        }

        .ajaxmiddle_section_inner {
            float: left;
            width: 100%;
            border-radius: 10px;
            padding: 4%;
            background: url("../images/login-tp.png") repeat scroll 0 0 transparent;
            margin: 4% 0 4% 0;
            box-shadow: 0 0 10px #333;
        }

        .uniquelogin {
            padding: 10px 0 !important;
            text-align: center;
            width: 100% !important;
        }

        .logo img {
            float: left; 
        }
        .logo {
            float: left;
        }
        .appico {
            margin: 0 auto;
            padding-left: 100px;
        }

        .login {
            color: #fff;
            font-size: 17px;
            text-shadow: 1px 0px 1px #000;
            font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
        }

            .login:hover,
            .login:focus {
                color: #fff;
                text-decoration: initial;
            }

        .w3-bar-block .w3-bar-item:hover,
        .w3-bar-block .w3-bar-item:focus {
            text-decoration: initial;
        }

        .middle_section {
            width: 90%;
            margin: 0 auto; 
        }
         

        .loginfooter_new {
            background: #232425; 
            float: left;
            height: 45px;
            text-align: center;
            width: 100%;
        }

        .explogologinpg_new {
            background: url("../images/iti_white.png") no-repeat scroll 0 0;
            color: white;
            float: left;
            height: 30px;
            margin: 9px 0 0 43%;
            text-align: right;
        }

        legend {
            background: none;
            color: #007bff;
            font-size: 17px;
        }

        .btn {
            height: 23px;
            width: 85px;
            border-radius: 6px;
            margin: 0px 5px 0px 5px;
            font-size: 14px;
            line-height: 0.5;
        } 
        tr { 
            background-color: #d8d8d8;
            font-weight: bold;
            color: black;
        }

        .Grid th { 
            background-color: #039caf;
            color: white;
            font-weight: bold;
            font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
        }

        .Grid td span { 
            font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
        }

        .Grid td a {
            color: blue;
        }

        .Grid.table > tbody > tr > td {
            padding: 8px;
        }

        tr:hover {
            background-color: #d8d8d8;
        }

        .Grid td input {
            height: 15px;
            width: 20px;
        }
        th {
            background-color: #009AD7;
        }
        td {
            overflow-wrap: break-word;
        }
        .checkagree {
            margin-left: 10px;
        }

        .form-control {
            margin: 0;
            font-family: inherit;
            font-size: inherit;
            line-height: inherit;
            color: blue;
        }

        .w3-half img {
            margin-bottom: -6px;
            margin-top: 16px;
            opacity: 0.8;
            cursor: pointer
        }

            .w3-half img:hover {
                opacity: 1
            }

        .w3-large {
            font-size: 14px !important;
        }

        .w3-padding-32 {
            padding-top: 0px !important;
        }

        #icoDashBoard {
            padding-right: 1.5em;
        }

        .field-margin-top .View {
            background: rgb(241 241 241) !important;
            border-radius: 5px;
            margin: 0px;
        }

            .field-margin-top .View .aspNetDisabled {
                padding-top: 10px;
                vertical-align: middle;
                padding-left: 5px;
            }


        @media screen and (min-device-width: 320px) and (max-device-width: 720px) {
             
            .w3-main {
                margin-top: 30px;
            } 
        }

        @media screen and (min-device-width: 320px) and (max-device-width: 500px) {
            #ICLeadManagement {
                display: none;
            } 

            #icoDashBoard {
                display: none;
            }

            #ICFeedback {
                display: none;
            }

            #ICCallsupport {
                display: none;
            } 

            #icoDashBoard {
                padding-right: 2em;
            }

            .w3-padding-32 {
                padding-top: 32px !important;
            }

            .w3-sidebar.w3-collapse {
                width: 100% !important;
            }

            #userprofile {
                display: none;
            }

            label {
                padding-left: initial;
                float: left;
            }

            html,
            body {
                height: 100%;
            }

            .Popup {
                left: 0px;
                top: 0px;
            }
        }

        input[type=checkbox], input[type=radio] {
            margin: 0px;
        }

        label {
            font-size: 14px;
            padding-left: initial;
            font-weight: bold;
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

        .fieldname {
            color: #FFFFFF;
            float: left;
            font-size: 13px;
            font-weight: 500;
            margin: 3px 0 0;
            width: 105px;
        }

        .Report {
            padding-left: initial;
            padding-right: initial;
            overflow: auto;
            font-weight: 300; 
            line-height: 1.5;
        }

        .col-md-12 .Report {
            display: inline-table;
        }


        .w3-sidebar {
            height: 95.5%;
            margin-top: 50px;
        }

        .w3-blue, .w3-hover-blue:hover { 
            background-color: #3C4C5B !important; 
        } 
        .lRemove {
            background: #C22439 !important;
            color: white;
            height: 23px;
        }

            .lRemove:hover {
                background: #C22439 !important;
                color: white;
                height: 23px;
            }

        .lDownload {
            background: #009ad7 !important;
            color: white;
            height: 23px;
        }

            .lDownload:hover {
                background: #009ad7 !important;
                color: white;
                height: 23px;
            }

        .w3-bar-block .w3-bar-item {
            padding: 7px 16px 7px 37px !important;
            color: #ccc;
            border-top: 1px solid #546171;
        }

        .ProjectTitle {
            background-color: #336699 !important;
            color: white;
            font-size: large;
            font-weight: normal;
            text-align: left;
            margin-top: 30px;
            padding: 0px 0px 0px 8px;
            border-style: none none solid none;
            border-width: thin;
            border-color: red;
            margin-left: 1px;
        }

        .ProjectHeadingLine { 
            background-color: #569d14 !important
        }

        .Delete {
            background: #C22439 !important;
            color: white;
            height: 18px;
            width: 23px;
            border-radius: 3px;
        }

            .Delete:hover {
                background: #C22439 !important;
                color: white;
                height: 18px;
                width: 23px;
                border-radius: 3px;
            }

        .Reject {
            background: #C22439 !important;
            color: white;
            height: calc(1.5em + .75rem + 2px);
        }

            .Reject:hover {
                background: #C22439 !important;
                color: white;
                height: calc(1.5em + .75rem + 2px);
            }

        .Back {
            background: #673499 !important;
            color: white;
            height: calc(1.5em + .75rem + 2px);
        }

            .Back:hover {
                background: #673499 !important;
                color: white;
                height: calc(1.5em + .75rem + 2px);
            }

        .Save { 
            background-color: forestgreen;
            color: white;
            height: calc(1.5em + .75rem + 2px);
        }

            .Save:hover {
                background: #145c14;
            }

        .Search {
            background: #009ad7 !important;
            color: white;
            height: calc(1.5em + .75rem + 2px);
        }

            .Search:hover {
                background: #009ad7 !important;
                color: white;
                height: calc(1.5em + .75rem + 2px);
            }

        .Edit {
            background: #68AF27 !important;
            color: white;
            height: calc(1.5em + .75rem + 2px);
        }

            .Edit:hover {
                background: #68AF27 !important;
                color: white;
                height: calc(1.5em + .75rem + 2px);
            }

        .Approval {
            background: #68AF27 !important;
            color: white;
            height: calc(1.5em + .75rem + 2px);
        }

            .Approval:hover {
                background: #68AF27 !important;
                color: white;
                height: calc(1.5em + .75rem + 2px);
            }

        .Progress {
            background: #FFAA31 !important;
            color: white;
            height: calc(1.5em + .75rem + 2px);
        }

            .Progress:hover {
                background: #FFAA31 !important;
                color: white;
                height: calc(1.5em + .75rem + 2px);
            }

        .Mandatory {
            color: red;
            font-size: 15px;
            font-weight: bold;
        }

        .disable-select {
            user-select: none; /* supported by Chrome and Opera */
            -webkit-user-select: none; /* Safari */
            -khtml-user-select: none; /* Konqueror HTML */
            -moz-user-select: none; /* Firefox */
            -ms-user-select: none; /* Internet Explorer/Edge */
        }

        .ajax__calendar * tr {
            background-color: white;
            color: black;
        }

        .message {
            text-align: center;
            font-weight: bold;
            width: 100%;
        }

        .table > tbody > tr > td {
            line-height: 1.42857143;
            vertical-align: top;
            border-top: 1px solid #ddd;
            font-weight: initial;
            text-align: none;
        }

        b, strong {
            font-weight: initial;
        } 
        .w3-padding-large {
            padding: 0px 2px !important;
        }

        .w3-large {
            font-size: 15px !important;
        }

        .w3-medium {
            font-size: 14px !important;
            font-weight: initial;
        }

        .fa-2x {
            font-size: 2em;
            float: right;
        }

        .submenu {
            font-weight: bold;
            font-size: 15px;
        }

        ul#main-menu {
            padding-inline-start: -0px;
        }

        .col, .col-1, .col-10, .col-11, .col-12, .col-2, .col-3, .col-4, .col-5, .col-6, .col-7, .col-8, .col-9, .col-auto, .col-lg, .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-lg-auto, .col-md, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-md-auto, .col-sm, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-sm-auto, .col-xl, .col-xl-1, .col-xl-10, .col-xl-11, .col-xl-12, .col-xl-2, .col-xl-3, .col-xl-4, .col-xl-5, .col-xl-6, .col-xl-7, .col-xl-8, .col-xl-9, .col-xl-auto {
            display: inline-table;
            padding: 5px;
        }

        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            width: 70px;
        }

        .ajax__tab_xp .ajax__tab_header {
            font-size: 16px;
        }

        td .form-control {
            height: 23px;
            padding: inherit;
        }

        .Grid td .form-control {
            height: 35px;
            padding: inherit;
            width: 100%;
        }

        .table > tbody > tr > td {
            padding: inherit;
        }

        button, input {
            height: 23px;
        }

        @media (min-width: 768px) {
            .col-md-3 {
                -ms-flex: 0 0 22%;
                flex: 0 0 22%;
                max-width: 22%;
            }
        }

        .Popup {
            display: block;
            z-index: 1002;
            outline: 0px;
            height: auto;
            width: 800px;
            top: 128px;
            left: 283px;
            position: absolute;
            padding: 0.2em;
            overflow: hidden;
            border-radius: 6px;
            border: 1px solid #CCC;
            background: #fefefe 50% bottom repeat-x;
            color: #666;
            font-family: Segoe UI,Arial,sans-serif;
            font-size: 1.1em;
            margin: 0 1% 0 1%;
            transition: transform 0.9s, top 0.9s;
        }

        .PopupHeader {
            border: 1px solid #333;
            background: #003d99 url(Ajax/Images/Feedbackheader.png) 50% 50% repeat-x;
            color: #fff;
            font-weight: bold;
            cursor: move;
            padding: 0.4em 1em;
            position: relative;
            border-radius: 6px;
            font-family: Segoe UI,Arial,sans-serif;
            font-size: 1.1em;
        }

        .clearfix:after {
            content: ".";
            display: block;
            height: 0;
            clear: both;
            visibility: hidden;
        }

        .PopupHeader a {
            color: #fff;
        }

        #PopupDialogue {
            float: left;
            font-size: 13px;
            font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
        }

        .PopupClose {
            float: right;
            color: black;
            font-size: 8px;
            width: 15px;
            height: 15px;
            padding: inherit;
        }

        .modal-backdrop {
            background-color: gray;
        }

        .modalBackground {
            background-color: #000000bd;
        }

        .Popup label {
            font-size: 13px;
        } 
        .dropbtn {
            background-color: #30526f;
            color: white;
            padding: 16px;
            font-size: 16px;
            border: none;
        }

        .dropbtn1 { 
            background-color: #336699 !important;
            color: white;
            padding: 1px;
            font-size: 16px;
            border: none;
        }

        .dropdown {
            position: relative;
            display: inline-block;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #f1f1f1;
            min-width: 195px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            z-index: 1;
        }

            .dropdown-content a {
                color: black;
                padding: 8px 16px;
                text-decoration: none;
                display: block;
            }

                .dropdown-content a:hover {
                    background-color: #ddd;
                }

        .dropdown:hover .dropdown-content {
            display: block;
        }
         

        .View {
            background: rgba(191,191,191,.8) !important;
        }

            .View label {
                font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
                font-weight: initial;
                font-size: 17px;
            }

            .View .label {
                font-weight: bold;
                font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
              
                font: 16px;
            }
             
        .Popup {
            height: 600px;
        }

            .Popup .model-scroll {
                height: 500px;
                overflow: auto;
            }

            .Popup.flexible-popup {
                height: auto;
            }

        .modal-label {
            display: block;
        }

        #MainContent_UC_Customer_cbSendSMS, #MainContent_UC_Customer_cbSendEmail {
            margin-top: 0;
            vertical-align: middle;
        }

        .modal-check-label {
            padding-right: 10px;
            padding-top: 2px;
        }

        .PopupClose {
            font-size: 13px;
            width: 25px;
            height: 25px;
        }

            .PopupClose:hover {
                background: #fff;
            }

        .back-buttton.sticky {
            position: fixed;
            right: 0;
            z-index: 99;
            background: #fff;
            width: calc(100% - 251px);
            padding: 7px 0px;
            top: 78px;
        }

        .back-buttton #MainContent_btnBackToList {
            float: right;
            margin-right: 20px;
            margin-top: -6px;
        }

        .lead-back-btn .back-buttton #MainContent_btnBackToList {
            margin-right: 10px;
        }

        .back-buttton.coldvisit #MainContent_btnBackToList {
            margin-top: 0px;
        }

        .back-buttton.sticky #MainContent_btnBackToList {
            margin-right: 30px;
            margin-top: 0px;
        } 
        .ajax__tab_default .ajax__tab {
            height: 30px !important;
        }

        .ajax__tab_xp .ajax__tab_header .ajax__tab_inner {
            padding-left: 1px !important;
        }

        .ajax__tab_xp .ajax__tab_header .ajax__tab_outer, .ajax__tab_xp .ajax__tab_header .ajax__tab_inner {
            background: none !important;
            height: 30px !important;
        }

        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            background: #fff !important;
            background-image: linear-gradient(#fff, #e3e4ee) !important;
            height: 29px !important;
            border: 1px solid #ccc;
            border-bottom: none;
            border-radius: 3px 3px 0px 0px;
            padding: 7px 8px !important;
            width: auto;
        }

        .ajax__tab_xp .ajax__tab_header .ajax__tab_active .ajax__tab_tab {
            background: #fff !important;
        }

        .ajax__tab_xp .ajax__tab_body {
            padding: 0 8px !important;
        }

        .Popup label {
            margin: 0px;
        }

        .fieldset-border {
            margin: 0px;
            padding-top: 0px;
        }

            .fieldset-border legend {
                margin-bottom: 0;
            }

        .action-btn {
            margin-right: 98px;
            margin-top: -62px;
            display: block;
            float: right;
        }

            .action-btn .btn.Approval {
                padding: 11px;
                margin-top: 7px;
            }

        #MainContent_UC_CustomerView_PnlCustomerView .action-btn .btn.Approval {
            margin-top: 2px;
        }

        .action-btn .btnactions.sticky {
            position: fixed;
            right: 118px;
            z-index: 99;
            top: 78px;
        }

        .dropdown.btnactions {
            z-index: 1;
        }

        .field-margin-top {
            margin-top: -35px;
            padding: 5px 0px;
        }

        .col-md-12 .Report {
            display: grid;
        }

        .table {
            margin-bottom: 0px;
        }

        .w3-show {
            background: #505a65;
        }

        .menu-item .submenu {
            font-weight: normal;
            font-size: 14px;
        }

        .submenu + .w3-bar-block .w3-bar-item {
            padding-left: 51px !important;
        }

        .submenu + .w3-show {
            background: #475461;
        }

        .w3-button:hover {
            text-decoration: none;
            color: #fff !important;
            background-color: #3a444d !important;
        }

        #main-nav .fa-2x {
            font-size: 1.6em;
        }

        #main-nav #main-menu > li > a {
            padding-top: 5px !important;
            padding-bottom: 5px !important;
            color: #ccc !important;
            border-top: 1px solid #546171;
        }

            #main-nav #main-menu > li > a:hover {
                color: #fff !important;
            }

                #main-nav #main-menu > li > a:hover i.fa-cogs {
                    color: #2fb7c3 !important;
                }

        .w3-bar-block .w3-bar-item:hover i.fa-mercury {
            color: #ff6666 !important;
        }

        .w3-bar-block .w3-bar-item.active {
            background-color: #3a444d !important;
            color: #fff !important;
        }

        .project-title-outer {
            position: relative;
            z-index: 9;
        } 
        @media screen and (min-device-width: 250px) and (max-device-width: 992px) {
            .project-title-outer .ProjectTitle {
                position: fixed;
                width: 100%;
                margin: 0;
                top: 50px;
                border-top: 1px solid #4c7da7;
            }
        }

        @media screen and (min-device-width: 993px) and (max-device-width: 3000px) {
            .project-title-outer .ProjectTitle {
                position: fixed; 
                width: -webkit-fill-available;
                margin: 0;
                top: 50px;
                border-top: 1px solid #4c7da7;
            }
        } 

        .sub-menu-dashboard .dropdown-content, #icoDashBoard .dropdown-content {
            right: 0px;
        }

        .table.table-bordered.table-condensed.Grid th {
            padding: 0.3rem 0.75rem;
            font-size: 14px;
            text-align: center;
        }

        .table.table-bordered.table-condensed.Grid td {
            padding: 6px 8px;
            font-size: 14px;
        }

        .table.table-bordered.table-condensed.Grid tr:last-child td {
            padding: 3px 8px;
        } 

        .lead-static .portlet .btn-group.btn-group-devided {
            vertical-align: initial;
            padding: 0;
        }

            .lead-static .portlet .btn-group.btn-group-devided label {
                display: flex;
                justify-content: center;
                align-items: center;
            }

                .lead-static .portlet .btn-group.btn-group-devided label span {
                    padding-left: 5px;
                }

        .lead-static .portlet #divLeadStatistics {
            justify-content: center;
        }


        .lead-static .portlet #divEnquiryStatistics {
            justify-content: center;
        }

        .lead-static .portlet #divLeadStatistics .thumbnail.wide_thumbnail {
            width: 15%;
            padding-top: 20px;
            padding-bottom: 20px;
            padding-right: 25px !important;
            margin-right: 10px !important;
            margin-top: 10px;
        }


        #divEnquiryStatistics .thumbnail.wide_thumbnail {
            width: 20%;
            padding-top: 20px;
            padding-bottom: 20px;
            padding-right: 25px !important;
            margin-right: 10px !important;
            margin-top: 10px;
        }

        /*Report pages*/
        .report-container tr > td {
            padding: 15px;
        }

        .report-container .boxHead {
            overflow: hidden;
            position: relative;
        }

            .report-container .boxHead .logheading {
                float: left;
            }

            .report-container .boxHead .order-show {
                float: right;
            }

        .report-container .report-panel {
            margin-top: 15px
        }

            .report-container .report-panel .form-control {
                height: 35px;
                padding: 0px 7px;
            }

            .report-container .report-panel input.form-control {
                height: 35px;
                padding: 0px 10px;
            }

        .report-panel .col, .report-panel .col-1, .report-panel .col-10, .report-panel .col-11, .report-panel .col-12, .report-panel .col-2, .report-panel .col-3, .report-panel .col-4,
        .report-panel .col-5, .report-panel .col-6, .report-panel .col-7, .report-panel .col-8, .report-panel .col-9, .report-panel .col-auto, .report-panel .col-lg, .report-panel .col-lg-1,
        .report-panel .col-lg-10, .report-panel .col-lg-11, .report-panel .col-lg-12, .report-panel .col-lg-2, .report-panel .col-lg-3, .report-panel .col-lg-4, .report-panel .col-lg-5,
        .report-panel .col-lg-6, .report-panel .col-lg-7, .report-panel .col-lg-8, .report-panel .col-lg-9, .report-panel .col-lg-auto, .report-panel .col-md, .report-panel .col-md-1,
        .report-panel .col-md-10, .report-panel .col-md-11, .report-panel .col-md-12, .report-panel .col-md-2, .report-panel .col-md-3, .report-panel .col-md-4, .report-panel .col-md-5,
        .report-panel .col-md-6, .report-panel .col-md-7, .report-panel .col-md-8, .report-panel .col-md-9, .report-panel .col-md-auto, .report-panel .col-sm, .report-panel .col-sm-1,
        .report-panel .col-sm-10, .report-panel .col-sm-11, .report-panel .col-sm-12, .report-panel .col-sm-2, .report-panel .col-sm-3, .report-panel .col-sm-4, .report-panel .col-sm-5,
        .report-panel .col-sm-6, .report-panel .col-sm-7, .report-panel .col-sm-8, .report-panel .col-sm-9, .report-panel .col-sm-auto, .report-panel .col-xl, .report-panel .col-xl-1,
        .report-panel .col-xl-10, .report-panel .col-xl-11, .report-panel .col-xl-12, .report-panel .col-xl-2, .report-panel .col-xl-3, .report-panel .col-xl-4, .report-panel .col-xl-5,
        .report-panel .col-xl-6, .report-panel .col-xl-7, .report-panel .col-xl-8, .report-panel .col-xl-9, .report-panel .col-xl-auto {
            display: initial;
        }

        .report-container .report-panel button, .report-container .report-panel input {
            height: 30px;
        }

        .custom-auto-complete {
            position: absolute;
            background-color: #f7f7f7;
            z-index: 1;
            width: 97%;
            border-top: 0;
            border: 1px solid #ddd;
            box-shadow: 2px 2px 6px 0px #cecaca;
        }

            .custom-auto-complete .auto-item {
                border-bottom: 1px solid #ddd;
                padding: 10px;
            }

                .custom-auto-complete .auto-item:last-child {
                    border-bottom: none;
                }

                .custom-auto-complete .auto-item p {
                    color: #000;
                    display: flex;
                    flex-direction: row;
                    justify-content: space-between;
                    margin: 0px;
                }

                    .custom-auto-complete .auto-item p > span {
                        color: forestgreen;
                        font-weight: bold;
                    }

                    .custom-auto-complete .auto-item p label span {
                        font-weight: bold;
                    }

                    .custom-auto-complete .auto-item p label {
                        font-weight: normal;
                    }

                .custom-auto-complete .auto-item .customer-info {
                    display: flex;
                    flex-direction: row;
                    justify-content: space-between;
                }

                    .custom-auto-complete .auto-item .customer-info label:last-child {
                        color: #009AD7;
                    }

        .lead-static .portlet .btn-group.btn-group-devided label input {
            margin: 0;
        }

        .portlet-title .actions .btn-group {
            margin-top: 4px;
            display: flex;
            padding: 0;
        }

        @media only screen and (min-width: 992px) {
            .closed-sidebar {
                width: 50px;
                overflow: visible;
            }

                .closed-sidebar #main-menu > li > a span {
                    display: none;
                }

                .closed-sidebar #main-menu > li > a {
                    padding-top: 5px !important;
                    padding-bottom: 5px !important;
                }

                .closed-sidebar #main-menu > li:hover {
                    width: 256px !important;
                    position: relative !important;
                    z-index: 10000;
                    display: block !important;
                }

                    .closed-sidebar #main-menu > li:hover span {
                        display: inline !important;
                        padding-left: 30px;
                    }

                .closed-sidebar #main-menu > li > .w3-bar-block {
                    display: none !important;
                }

                .closed-sidebar #main-menu > li:hover > .w3-bar-block {
                    width: 210px;
                    position: absolute;
                    z-index: 2000;
                    left: 46px;
                    margin-top: 0;
                    top: 100%;
                    display: block !important;
                    -webkit-border-radius: 0 0 4px 4px;
                    -moz-border-radius: 0 0 4px 4px;
                    -ms-border-radius: 0 0 4px 4px;
                    -o-border-radius: 0 0 4px 4px;
                    border-radius: 0 0 4px 4px;
                    background: #505a65;
                    max-height: 400px;
                    overflow: auto;
                }

                    .closed-sidebar #main-menu > li:hover > .w3-bar-block a {
                        padding: 5px 10px 5px 21px !important
                    }

                        .closed-sidebar #main-menu > li:hover > .w3-bar-block a span {
                            padding-left: 0;
                        }

                        .closed-sidebar #main-menu > li:hover > .w3-bar-block a.submenu .sub-menu-icon {
                            display: inline !important;
                        }
        }

        @media only screen and (max-width: 992px) {
            .back-buttton.sticky, .project-title-outer .ProjectTitle {
                width: 100%;
            }

            .w3-sidebar.w3-collapse {
                width: 250px !important;
            }

            #icoDashBoard {
                padding-right: 1em;
            }
        }

        @media only screen and (max-width: 767px) {
            .Popup {
                height: auto;
                left: 0 !important;
                width: auto;
            } 
            .table-responsive {
                width: 100%;
                margin-bottom: 15px;
                overflow-y: hidden; 
            }

                .table-responsive > .table {
                    margin-bottom: 0;
                }

            .responsive-menu-click {
                z-index: 999;
            }

            .w3-sidebar.w3-collapse {
                z-index: 9999 !important;
            }

            .my-overlay {
                z-index: 999;
            }

            .lead-static .portlet .portlet-title .actions {
                float: none !important;
            }

            .lead-static .portlet #divLeadStatistics .thumbnail.wide_thumbnail #divEnquiryStatistics {
                width: 28%;
            }

            .lead-static .portlet #divLeadStatistics .thumbnail.wide_thumbnail:first-child #divEnquiryStatistics {
                margin-left: 0 !important;
            }

            .lead-static .portlet #divLeadStatistics .thumbnail.wide_thumbnail:nth-child(2n) #divEnquiryStatistics {
                margin-right: 0 !important;
            }

            .Popup .model-scroll {
                width: 440px;
                margin: 0 auto;
                max-height: calc(100vh - 175px);
                height: auto;
            }

            .flexible-popup .flexible-scroll {
                max-height: calc(100vh - 175px);
                overflow: auto;
                display: block;
            }

            .lead-static .portlet #divLeadStatistics {
                margin-right: 0;
                margin-left: 0;
                flex-direction: column;
            }

                .lead-static .portlet #divLeadStatistics .thumbnail.wide_thumbnail,
                #divEnquiryStatistics .thumbnail.wide_thumbnail {
                    width: 60%;
                    margin-right: auto !important;
                    margin-left: auto !important;
                }

            .portlet-title .caption {
                padding-bottom: 10px;
            }
        }

        @media only screen and (max-width: 480px) {
            .lead-static .portlet #divLeadStatistics .thumbnail.wide_thumbnail #divEnquiryStatistics {
                width: 42%;
            }

            .lead-static .portlet .btn-group.btn-group-devided label {
                width: 70px;
            }

            .Popup .model-scroll {
                width: 340px;
            }

            .lead-static .portlet #divLeadStatistics .thumbnail.wide_thumbnail,
            #divEnquiryStatistics .thumbnail.wide_thumbnail {
                width: 80%;
            }
        }

        @media only screen and (max-width: 576px) {
            .order-xs-last {
                -ms-flex-order: 13;
                order: 13
            }

            .order-xs-0 {
                -ms-flex-order: 0;
                order: 0
            }

            .order-xs-1 {
                -ms-flex-order: 1;
                order: 1
            }

            .order-xs-2 {
                -ms-flex-order: 2;
                order: 2
            }

            .order-xs-3 {
                -ms-flex-order: 3;
                order: 3
            }

            .order-xs-4 {
                -ms-flex-order: 4;
                order: 4
            }

            .order-xs-5 {
                -ms-flex-order: 5;
                order: 5
            }

            .order-xs-6 {
                -ms-flex-order: 6;
                order: 6
            }

            .order-xs-7 {
                -ms-flex-order: 7;
                order: 7
            }

            .order-xs-8 {
                -ms-flex-order: 8;
                order: 8
            }

            .order-xs-9 {
                -ms-flex-order: 9;
                order: 9
            }

            .order-xs-10 {
                -ms-flex-order: 10;
                order: 10
            }

            .order-xs-11 {
                -ms-flex-order: 11;
                order: 11
            }

            .order-xs-12 {
                -ms-flex-order: 12;
                order: 12
            }
        }
    </style> 
    <%--Below for these Rating Related--%>
    <style type="text/css">
        .rating-star-block .star.outline {
            background: url("../Ajax/Images/star-empty-lg.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
        }

        .rating-star-block .star.filled {
            background: url("../Ajax/Images/star-fill-lg.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
        }

        .rating-star-block .star {
            color: rgba(0,0,0,0);
            display: inline-block;
            height: 24px;
            overflow: hidden;
            text-indent: -999em;
            width: 24px;
        }

        a {
            color: #005782;
            text-decoration: none;
        }

        @media screen and (min-device-width: 320px) and (max-device-width: 500px) {
            .feed {
                width: 305px;
            }
        }

        .LnkGridNotification {
            overflow-y: scroll;
            height: 100px;
            background-color: white;
        }

            .LnkGridNotification:hover {
                background-color: white;
            }

        #gvMessageAnnouncement a:hover {
            background-color: white;
        }
    </style> 
    <script>
        window.onload = function () {
            if (screen.width < 450) {
                var mvp = document.getElementById('vp');
                mvp.setAttribute('content', 'user-scalable=no,width=450');
            }
        }
    </script>
    <script>
        $(document).ready(function () {
            var ParentMenu = document.getElementById(localStorage.getItem("ParentMenu"));
            if (ParentMenu != null) {
                ParentMenu.className = "w3-bar-block w3-hide w3-medium w3-show";
                var MainMenu = document.getElementById(localStorage.getItem("MainMenu"));
                if (MainMenu != null) {
                    MainMenu.className = "w3-bar-block w3-hide w3-medium w3-show";
                }
            }
            var SidebarID = document.getElementById(localStorage.getItem("SidebarID"));
            var w3mainID = document.getElementById(localStorage.getItem("w3mainID"));
            if (SidebarID != null) {
                SidebarID.style.width = "50px";
                document.getElementById("CompanyLogo").style.display = "none";
                document.getElementById("SideBarToggle").style.paddingLeft = "0px";
                document.getElementById("mySidebar").classList.add(localStorage.getItem("mySidebarClass"));
                w3_hide_angle_down();
            }
            if (w3mainID != null) {
                w3mainID.style.marginLeft = "50px";
                document.getElementById("w3mainclass").classList.add(localStorage.getItem("ClosedSidebarClass"));
            }
             
        });

        function ParentMenuClick(ParentMenu, MainMenu) {

            localStorage.setItem("ParentMenu", ParentMenu);
            localStorage.setItem("MainMenu", MainMenu);
            var y = document.getElementById(MainMenu);
            var x = document.getElementById(ParentMenu);
            x.className = "w3-bar-block w3-hide w3-medium w3-show";
            y.className = "w3-bar-block w3-hide w3-medium w3-show";
            document.getElementById("mySidebar").style.display = "none";
            document.getElementById("myOverlay").style.display = "none";
        }
        function Menu(id, Mainid, Itag) {
            var xModule = document.getElementById(id);
            var xMainModule = document.getElementById(Mainid);

            var ModuleClassName = xModule.className;
            var itagid = document.getElementById(Itag);
            var itagClassName = xModule.className;

            document.getElementById(Itag).className = 'sub-menu-icon fa fa-angle-up fa-2x';

            $('#Menu').find('div').each(function () {
                var x2 = document.getElementById($(this).attr('id'));
                x2.className = x2.className.replace(" w3-show", "");
            }); 
            if (ModuleClassName == 'w3-bar-block w3-hide w3-medium w3-show') {
                xModule.className = 'w3-bar-block w3-hide w3-medium';
                document.getElementById(Itag).className = 'sub-menu-icon fa fa-angle-down fa-2x';
            }
            else {
                xModule.className = 'w3-bar-block w3-hide w3-medium w3-show';
            }

            if (Mainid != null) {

                xMainModule.className = 'w3-bar-block w3-hide w3-medium w3-show'; 
            }
        }
    </script>
    <script>
        function SetScreenTitle(as_pagetitle) {
            var lbl = document.getElementById("lblProjectTitle")
            lbl.textContent = as_pagetitle; 
        }
    </script>
    <script>
        function ScreenControl(optn) {
            var fsCriteria = document.getElementById("fsCriteria");
            var fs = document.getElementById("fs");
            var rs = document.getElementById("rs"); 

            if (optn == 1) {
                fsCriteria.style.display = "none";
                fs.style.display = "block";
                rs.style.display = "none";
            }

            if (optn == 2) {
                fsCriteria.style.display = "block";
                fs.style.display = "none";
                rs.style.display = "block";
            }
        }
    </script>

    <script> 
        function w3_open() {
            document.getElementById("mySidebar").style.display = "block";
            document.getElementById("myOverlay").style.display = "block";
        }

        function w3_close() { 
            document.getElementById("mySidebar").style.display = "none";
            document.getElementById("myOverlay").style.display = "none";
        }


        function w3_closeSideBar() {
            if (document.getElementById("mySidebar").style.width == "250px") {
                localStorage.setItem("SidebarID", "mySidebar");
                localStorage.setItem("mySidebarClass", "closed-sidebar");
                document.getElementById("mySidebar").style.width = "50px";
                document.getElementById("mySidebar").classList.add("closed-sidebar");
                w3_hide_angle_down();
                document.getElementById("CompanyLogo").style.display = "none";
                document.getElementById("SideBarToggle").style.paddingLeft = "0px"; 

                $('#Menu').find('div').each(function () {
                    var x2 = document.getElementById($(this).attr('id'));
                    x2.className = x2.className.replace(" w3-show", "");
                });
            }
            else {
                localStorage.removeItem("SidebarID", "mySidebar");
                localStorage.removeItem("mySidebarClass", "closed-sidebar");
                document.getElementById("mySidebar").style.width = "250px";
                document.getElementById("mySidebar").classList.remove("closed-sidebar");
                w3_disp_angle_down()
                document.getElementById("CompanyLogo").style.display = "block";
                document.getElementById("SideBarToggle").style.paddingLeft = "235Px"
            }

            if (document.getElementById("w3mainclass").style.marginLeft == "250px") {
                localStorage.setItem("w3mainID", "w3mainclass");
                localStorage.setItem("ClosedSidebarClass", "closed-sidebar-main");
                document.getElementById("w3mainclass").style.marginLeft = "50px";
                document.getElementById("w3mainclass").classList.add("closed-sidebar-main"); 
            }
            else {
                localStorage.removeItem("w3mainID", "w3mainclass");
                localStorage.removeItem("ClosedSidebarClass", "closed-sidebar-main");
                document.getElementById("w3mainclass").style.marginLeft = "250px";
                document.getElementById("w3mainclass").classList.remove("closed-sidebar-main"); 
            }
        }

        function w3_closeHomeSearch() {
            if (document.getElementById("homeSearchMain").style.width == "300px") {
                document.getElementById("homeSearchMain").style.width = "0px"; 

            }
            else {
                document.getElementById("homeSearchMain").style.width = "300px"; 
            }
        }

        function w3_hide_angle_down() {
            $(document).ready(function () {
                $('i').each(function () {
                    var i = $(this).attr('id');
                    if (i != undefined && i.substring(0, 1) == 'i') {
                        //console.log(i);
                        document.getElementById(i).style.display = "none";
                        document.getElementById(i).className = 'sub-menu-icon fa fa-angle-down fa-2x';

                    }
                });
            });
        };

        function w3_disp_angle_down() {
            $(document).ready(function () {
                $('i').each(function () {
                    var i = $(this).attr('id');
                    if (i != undefined) {
                        //console.log(i);
                        document.getElementById(i).style.display = "block";

                    }

                });
            });
        };


        // Previous date hide
        $(function () {
            var dtToday = new Date();

            var month = dtToday.getMonth() + 1;
            console.log(month);
            var day = dtToday.getDate();
            console.log(day);
            var year = dtToday.getFullYear();
            if (month < 10)
                month = '0' + month.toString();

            if (day < 10)
                day = '0' + day.toString();

            var minDate = year + '-' + month + '-' + day;
            // Max
            var mmonth = dtToday.setMonth(1);

            var mday = dtToday.setDate();
            var myear = dtToday.setFullYear();
            if (mmonth < 10)
                mmonth = '0' + mmonth.toString();

            if (mday < 10)
                mday = '0' + mday.toString();
            var monthplus = parseInt(mmonth) + 01;
            if (month < 10)
                monthplus = '0' + monthplus.toString();

            var maxDate = year + '-' + monthplus + '-' + day;

            $('#MainContent_UC_Customer_txtDOB').attr('min', minDate);
            $('#MainContent_UC_Customer_txtDOB').attr('max', maxDate);
            $('#MainContent_UC_Customer_txtDOAnniversary').attr('min', minDate);
        });


        //sticky back button

        function boxtothetop() {
            var windowTop = ($(window).scrollTop()) + 78;
            var top = $('#boxHere').offset().top;
            if (windowTop > top) {

                $('#backBtn').addClass('sticky');
                $('#boxHere').height($('#backBtn').outerHeight());
                $('#customerAction').addClass('sticky');
                $('#boxHere').height($('#customerAction').outerHeight());
            } else {
                $('#backBtn').removeClass('sticky');
                $('#customerAction').removeClass('sticky');
                $('#boxHere').height(0);
            }
        }
        $(function () {
            $(window).scroll(boxtothetop);
            boxtothetop();
        });

    </script>


    <script>
        jQuery(function ($) {
            var path = window.location.href;
            // because the 'href' property of the DOM element is the absolute path
            $('#main-nav ul li a').each(function () {
                if (this.href === path) {
                    $(this).addClass('active');
                } 
            });
        });
    </script>


    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        /* Start Loader Graphic */
        .loading {
            display: none;
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: center no-repeat rgba(255, 255, 255, 0.9);
            transform: translateZ(0);
            -webkit-transform: translateZ(0);
            transform: translateZ(0);
            -moz-transform: translatez(0);
            -ms-transform: translatez(0);
            -o-transform: translatez(0);
            -webkit-transform: translateZ(0);
            -webkit-font-smoothing: antialiased;
            -webkit-transform: translate3d(0, 0, 0);
            transform: translate3d(0, 0, 0);
        }

        .outerspinner {
            position: absolute;
            left: 50%;
            top: 50%;
            margin: -1.875rem 0 0 -4.688rem;
        }

        body.lang-fr-ca .outerspinner {
            margin: -1.875rem 0 0 -6.25rem;
        }

        .submission-msg {
            font-family: 'Roboto-Bold';
            color: #004382;
            font-size: 1rem;
            font-weight: 700;
            line-height: 1.2em;
            margin-bottom: 1rem;
            text-align: center;
        }

        .spinner {
            margin: 0;
            text-align: center;
        }

            .spinner > div {
                width: 18px;
                height: 18px;
                background-color: #00539f;
                border-radius: 100%;
                display: inline-block;
                -webkit-animation: sk-bouncedelay 1.4s infinite ease-in-out both;
                animation: sk-bouncedelay 1.4s infinite ease-in-out both;
            }

            .spinner .bounce1 {
                -webkit-animation-delay: -0.32s;
                animation-delay: -0.32s;
            }

            .spinner .bounce2 {
                -webkit-animation-delay: -0.16s;
                animation-delay: -0.16s;
            }

        @-webkit-keyframes sk-bouncedelay {
            0%, 80%, 100% {
                -webkit-transform: scale(0);
            }

            40% {
                -webkit-transform: scale(1);
            }
        }

        @keyframes sk-bouncedelay {
            0%, 80%, 100% {
                -webkit-transform: scale(0);
                transform: scale(0);
            }

            40% {
                -webkit-transform: scale(1);
                transform: scale(1);
            }
        }

        .Notification-counter {
            position: absolute;
            transform: scale(.9);
            transform-origin: top right;
            right: -0.45rem;
            margin-top: -0.15rem;
            color: #fff;
            background-color: #e74a3b;
            display: inline-block;
            padding: 0.25em 0.4em;
            font-size: 64%;
            font-weight: 700;
            line-height: 1;
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            border-radius: 0.35rem;
            transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <UC:UC_SpcAssemblyDView ID="UC_SpcAssemblyDView" runat="server"></UC:UC_SpcAssemblyDView>
    </form>
</body>
</html>
